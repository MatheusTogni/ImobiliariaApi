using Repository;
using Repository.Models;
using Service.Dto;
using Service.Parser;
using Service.Validate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Service
{
    public class ManutencaoService
    {
        private readonly ManutencaoRepository _repository;
        private readonly ImovelRepository _imovelRepository; // Dependência adicional para verificar a existência do Imóvel

        // Construtor que recebe os repositórios necessários
        public ManutencaoService(ManutencaoRepository repository, ImovelRepository imovelRepository)
        {
            _repository = repository;
            _imovelRepository = imovelRepository;
        }

        // Retorna todas as manutenções, convertendo-as em DTOs
        public List<ManutencaoDto> GetAllManutencoes()
        {
            var manutencoes = _repository.GetAll();
            return manutencoes.Select(ManutencaoParser.ToDto).ToList();
        }

        // Retorna uma manutenção específica por ID, convertendo-a em DTO
        public ManutencaoDto GetManutencaoById(int id)
        {
            var manutencao = _repository.GetById(id);
            return manutencao != null ? ManutencaoParser.ToDto(manutencao) : null;
        }

        // Cria uma nova manutenção após validações
        public void CreateManutencao(ManutencaoDto dto)
        {
            // Valida os dados da manutenção
            ManutencaoValidator.Validate(dto);

            // Verifica se o Imóvel relacionado existe
            var imovel = _imovelRepository.GetById(dto.ImovelId);
            if (imovel == null)
                throw new ValidationException("Imóvel não encontrado.");

            // Verifica se o ID da manutenção já existe para evitar duplicações
            var existingManutencao = _repository.GetById(dto.Id);
            if (existingManutencao != null)
                throw new ValidationException("Já existe uma manutenção com este Id.");

            var manutencao = ManutencaoParser.ToEntity(dto);
            _repository.Add(manutencao);
        }

        // Atualiza uma manutenção existente pelo ID
        public void UpdateManutencao(int id, ManutencaoDto dto)
        {
            var existingManutencao = _repository.GetById(id);
            if (existingManutencao == null)
                throw new KeyNotFoundException("Manutenção não encontrada.");

            // Desanexa a entidade do contexto para evitar conflitos
            _repository.Detach(existingManutencao);

            // Valida os dados atualizados
            ManutencaoValidator.Validate(dto);

            var updatedManutencao = ManutencaoParser.ToEntity(dto);
            updatedManutencao.Id = id; // Mantém o ID original
            _repository.Update(updatedManutencao);
        }

        // Atualiza parcialmente uma manutenção existente pelo ID
        public void PartialUpdateManutencao(int id, ManutencaoDto dto)
        {
            var existingManutencao = _repository.GetById(id);
            if (existingManutencao == null)
                throw new KeyNotFoundException("Manutenção não encontrada.");

            // Desanexa a entidade para evitar conflitos de rastreamento
            _repository.Detach(existingManutencao);

            // Atualiza apenas os campos não nulos/fornecidos
            if (!string.IsNullOrWhiteSpace(dto.Descricao)) existingManutencao.Descricao = dto.Descricao;
            if (dto.DataSolicitacao != default) existingManutencao.DataSolicitacao = dto.DataSolicitacao;
            if (dto.DataConclusao.HasValue) existingManutencao.DataConclusao = dto.DataConclusao;
            existingManutencao.Status = dto.Status;
            if (!string.IsNullOrWhiteSpace(dto.Responsavel)) existingManutencao.Responsavel = dto.Responsavel;
            if (dto.ImovelId > 0) existingManutencao.ImovelId = dto.ImovelId;

            _repository.Update(existingManutencao);
        }

        // Deleta uma manutenção pelo ID
        public void DeleteManutencao(int id)
        {
            var manutencao = _repository.GetById(id);
            if (manutencao == null)
                throw new KeyNotFoundException("Manutenção não encontrada.");

            _repository.Delete(id);
        }
    }
}
