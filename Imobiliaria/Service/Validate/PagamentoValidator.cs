using Service.Dto;
using Service.Exception;

namespace Service.Validate
{
    public static class PagamentoValidator
    {
        // Método para validar os dados de um DTO de Pagamento.
        public static void Validate(PagamentoDto dto)
        {
            // Verifica se a descrição do pagamento é nula ou vazia.
            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ValidationException("A descrição do pagamento é obrigatória.");

            // Verifica se o valor do pagamento é menor ou igual a zero.
            if (dto.Valor <= 0)
                throw new ValidationException("O valor do pagamento deve ser maior que zero.");

            // Verifica se a data de pagamento não foi fornecida (valor padrão).
            if (dto.DataPagamento == default)
                throw new ValidationException("A data de pagamento é obrigatória.");

            // Verifica se o método de pagamento é nulo ou vazio.
            if (string.IsNullOrWhiteSpace(dto.MetodoPagamento))
                throw new ValidationException("O método de pagamento é obrigatório.");
        }
    }
}
