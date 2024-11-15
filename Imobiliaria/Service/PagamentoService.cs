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
    public class PagamentoService
    {
        private readonly PagamentoRepository _repository; // Dependência do repositório para operações com o banco de dados.

        // Construtor que recebe o repositório necessário.
        public PagamentoService(PagamentoRepository repository)
        {
            _repository = repository;
        }

        // Método para obter todos os pagamentos e convertê-los em DTOs.
        public List<PagamentoDto> GetAllPagamentos()
        {
            var pagamentos = _repository.GetAll();
            return pagamentos.Select(PagamentoParser.ToDto).ToList(); // Conversão para DTO para retorno.
        }

        // Método para obter um pagamento específico pelo ID.
        public PagamentoDto GetPagamentoById(int id)
        {
            var pagamento = _repository.GetById(id);
            return pagamento != null ? PagamentoParser.ToDto(pagamento) : null; // Retorna null se não for encontrado.
        }

        // Método para criar um novo pagamento, com validação.
        public void CreatePagamento(PagamentoDto dto)
        {
            PagamentoValidator.Validate(dto); // Validação dos dados antes de criar.

            // Verifica se o ID já existe para evitar duplicações.
            var existingPagamento = _repository.GetById(dto.Id);
            if (existingPagamento != null)
                throw new ValidationException("Já existe um pagamento com este Id.");

            var pagamento = PagamentoParser.ToEntity(dto); // Conversão de DTO para entidade.
            _repository.Add(pagamento);
        }

        // Método para atualizar um pagamento existente pelo ID.
        public void UpdatePagamento(int id, PagamentoDto dto)
        {
            var existingPagamento = _repository.GetById(id);
            if (existingPagamento == null)
                throw new KeyNotFoundException("Pagamento não encontrado.");

            _repository.Detach(existingPagamento); // Desanexa a entidade para evitar conflitos de rastreamento.

            PagamentoValidator.Validate(dto); // Validação dos dados antes da atualização.
            var updatedPagamento = PagamentoParser.ToEntity(dto);
            updatedPagamento.Id = id; // Mantém o ID original.
            _repository.Update(updatedPagamento);
        }

        // Método para atualização parcial de um pagamento.
        public void PartialUpdatePagamento(int id, PagamentoDto dto)
        {
            var existingPagamento = _repository.GetById(id);
            if (existingPagamento == null)
                throw new KeyNotFoundException("Pagamento não encontrado.");

            _repository.Detach(existingPagamento); // Desanexa a entidade para evitar conflitos.

            // Atualiza apenas os campos fornecidos.
            if (!string.IsNullOrWhiteSpace(dto.Descricao)) existingPagamento.Descricao = dto.Descricao;
            if (dto.Valor > 0) existingPagamento.Valor = dto.Valor;
            if (dto.DataPagamento != default) existingPagamento.DataPagamento = dto.DataPagamento;
            existingPagamento.Status = dto.Status;
            if (!string.IsNullOrWhiteSpace(dto.MetodoPagamento)) existingPagamento.MetodoPagamento = dto.MetodoPagamento;

            _repository.Update(existingPagamento);
        }

        // Método para deletar um pagamento pelo ID.
        public void DeletePagamento(int id)
        {
            var pagamento = _repository.GetById(id);
            if (pagamento == null)
                throw new KeyNotFoundException("Pagamento não encontrado.");

            _repository.Delete(id);
        }
    }
}
