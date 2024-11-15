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
    public class ReclamacaoService
    {
        private readonly ReclamacaoRepository _repository; // Dependência do repositório para acessar o banco de dados.

        // Construtor que recebe o repositório necessário para operações de banco de dados.
        public ReclamacaoService(ReclamacaoRepository repository)
        {
            _repository = repository;
        }

        // Método que retorna todas as reclamações em formato DTO.
        public List<ReclamacaoDto> GetAllReclamacoes()
        {
            var reclamacoes = _repository.GetAll();
            return reclamacoes.Select(ReclamacaoParser.ToDto).ToList(); // Converte as entidades para DTOs.
        }

        // Método que retorna uma reclamação específica pelo ID.
        public ReclamacaoDto GetReclamacaoById(int id)
        {
            var reclamacao = _repository.GetById(id);
            return reclamacao != null ? ReclamacaoParser.ToDto(reclamacao) : null; // Retorna null se não encontrada.
        }

        // Método que cria uma nova reclamação com validação de dados.
        public void CreateReclamacao(ReclamacaoDto dto)
        {
            ReclamacaoValidator.Validate(dto); // Valida os dados do DTO.

            // Verifica se já existe uma reclamação com o mesmo ID.
            var existingReclamacao = _repository.GetById(dto.Id);
            if (existingReclamacao != null)
                throw new ValidationException("Já existe uma reclamação com este Id.");

            var reclamacao = ReclamacaoParser.ToEntity(dto); // Converte o DTO para uma entidade.
            _repository.Add(reclamacao); // Adiciona a entidade ao banco de dados.
        }

        // Método que atualiza uma reclamação existente.
        public void UpdateReclamacao(int id, ReclamacaoDto dto)
        {
            var existingReclamacao = _repository.GetById(id);
            if (existingReclamacao == null)
                throw new KeyNotFoundException("Reclamação não encontrada."); // Retorna erro se não encontrada.

            _repository.Detach(existingReclamacao); // Desanexa a entidade para evitar conflitos de rastreamento.

            ReclamacaoValidator.Validate(dto); // Valida os dados do DTO.
            var updatedReclamacao = ReclamacaoParser.ToEntity(dto);
            updatedReclamacao.Id = id; // Mantém o ID original da entidade.
            _repository.Update(updatedReclamacao); // Atualiza a entidade no banco.
        }

        // Método que atualiza parcialmente uma reclamação.
        public void PartialUpdateReclamacao(int id, ReclamacaoDto dto)
        {
            var existingReclamacao = _repository.GetById(id);
            if (existingReclamacao == null)
                throw new KeyNotFoundException("Reclamação não encontrada.");

            _repository.Detach(existingReclamacao); // Desanexa a entidade.

            // Atualiza apenas os campos que foram informados no DTO.
            if (!string.IsNullOrWhiteSpace(dto.Titulo)) existingReclamacao.Titulo = dto.Titulo;
            if (!string.IsNullOrWhiteSpace(dto.Descricao)) existingReclamacao.Descricao = dto.Descricao;
            if (dto.DataReclamacao != default) existingReclamacao.DataReclamacao = dto.DataReclamacao;
            if (!string.IsNullOrWhiteSpace(dto.Status)) existingReclamacao.Status = dto.Status;
            if (!string.IsNullOrWhiteSpace(dto.Cliente)) existingReclamacao.Cliente = dto.Cliente;

            _repository.Update(existingReclamacao); // Atualiza a entidade no banco de dados.
        }

        // Método que deleta uma reclamação pelo ID.
        public void DeleteReclamacao(int id)
        {
            var reclamacao = _repository.GetById(id);
            if (reclamacao == null)
                throw new KeyNotFoundException("Reclamação não encontrada."); // Retorna erro se não encontrada.

            _repository.Delete(id); // Remove a entidade do banco de dados.
        }
    }
}
