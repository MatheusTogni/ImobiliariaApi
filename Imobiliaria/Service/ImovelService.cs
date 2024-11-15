using Repository;
using Repository.Models;
using Service.Dto;
using Service.Parser;
using Service.Validate;
using System.Collections.Generic;
using System.Linq;

namespace Service
{
    public class ImovelService
    {
        private readonly ImovelRepository _repository;

        // Construtor que inicializa o serviço com um repositório de imóveis.
        public ImovelService(ImovelRepository repository)
        {
            _repository = repository;
        }

        // Método para obter todos os imóveis, convertendo as entidades para DTOs.
        public List<ImovelDto> GetAllImoveis()
        {
            var imoveis = _repository.GetAll();
            return imoveis.Select(ImovelParser.ToDto).ToList();
        }

        // Método para buscar um imóvel por ID e convertê-lo em DTO, se encontrado.
        public ImovelDto GetImovelById(int id)
        {
            var imovel = _repository.GetById(id);
            return imovel != null ? ImovelParser.ToDto(imovel) : null;
        }

        // Método para criar um novo imóvel após validação dos dados.
        public void CreateImovel(ImovelDto dto)
        {
            // Valida os dados do DTO.
            ImovelValidator.Validate(dto);

            // Valida se o ID já existe no repositório.
            IdValidator.ValidateUniqueId(dto.Id, _repository);

            var imovel = ImovelParser.ToEntity(dto);
            _repository.Add(imovel);
        }

        // Método para atualizar completamente um imóvel pelo ID.
        public void UpdateImovel(int id, ImovelDto dto)
        {
            var existingImovel = _repository.GetById(id);
            if (existingImovel == null)
                throw new KeyNotFoundException("Imóvel não encontrado.");

            // Desanexa a instância existente para evitar conflitos de rastreamento no contexto.
            _repository.Detach(existingImovel);

            // Valida os novos dados.
            ImovelValidator.Validate(dto);

            var updatedImovel = ImovelParser.ToEntity(dto);
            updatedImovel.Id = id; // Mantém o ID original do imóvel.
            _repository.Update(updatedImovel);
        }

        // Método para atualização parcial de um imóvel.
        public void PartialUpdateImovel(int id, ImovelDto dto)
        {
            var existingImovel = _repository.GetById(id);
            if (existingImovel == null)
                throw new KeyNotFoundException("Imóvel não encontrado.");

            // Desanexa a instância existente para evitar conflitos de rastreamento.
            _repository.Detach(existingImovel);

            // Atualiza apenas os campos não nulos/fornecidos pelo DTO.
            if (!string.IsNullOrWhiteSpace(dto.Tipo)) existingImovel.Tipo = dto.Tipo;
            if (!string.IsNullOrWhiteSpace(dto.Status)) existingImovel.Status = dto.Status;
            if (!string.IsNullOrWhiteSpace(dto.Endereco)) existingImovel.Endereco = dto.Endereco;

            _repository.Update(existingImovel);
        }

        // Método para deletar um imóvel pelo ID.
        public void DeleteImovel(int id)
        {
            var imovel = _repository.GetById(id);
            if (imovel == null)
                throw new KeyNotFoundException("Imóvel não encontrado.");

            _repository.Delete(id);
        }
    }
}
