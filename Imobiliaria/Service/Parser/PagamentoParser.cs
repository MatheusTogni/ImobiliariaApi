using Repository.Models;
using Service.Dto;

namespace Service.Parser
{
    public static class PagamentoParser
    {
        // Método para converter um DTO de Pagamento em uma entidade Pagamento.
        public static Pagamento ToEntity(PagamentoDto dto)
        {
            return new Pagamento
            {
                Id = dto.Id, // Mapeia o ID do DTO para a entidade para manter a identificação.
                Descricao = dto.Descricao, // Mapeia a descrição do pagamento do DTO para a entidade.
                Valor = dto.Valor, // Mapeia o valor do pagamento do DTO para a entidade.
                DataPagamento = dto.DataPagamento, // Mapeia a data do pagamento do DTO para a entidade.
                Status = dto.Status, // Mapeia o status do pagamento do DTO para a entidade.
                MetodoPagamento = dto.MetodoPagamento // Mapeia o método de pagamento do DTO para a entidade.
            };
        }

        // Método para converter uma entidade Pagamento em um DTO de Pagamento.
        public static PagamentoDto ToDto(Pagamento entity)
        {
            return new PagamentoDto
            {
                Id = entity.Id, // Mapeia o ID da entidade para o DTO para manter a identificação.
                Descricao = entity.Descricao, // Mapeia a descrição da entidade para o DTO.
                Valor = entity.Valor, // Mapeia o valor da entidade para o DTO.
                DataPagamento = entity.DataPagamento, // Mapeia a data de pagamento da entidade para o DTO.
                Status = entity.Status, // Mapeia o status da entidade para o DTO.
                MetodoPagamento = entity.MetodoPagamento // Mapeia o método de pagamento da entidade para o DTO.
            };
        }
    }
}
