using Service.Dto;
using Service.Exception;

namespace Service.Validate
{
    public static class ReclamacaoValidator
    {
        // Método para validar os dados de um DTO de Reclamacao.
        public static void Validate(ReclamacaoDto dto)
        {
            // Verifica se o título da reclamação é nulo ou vazio.
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                throw new ValidationException("O título da reclamação é obrigatório.");

            // Verifica se a descrição da reclamação é nula ou vazia.
            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ValidationException("A descrição da reclamação é obrigatória.");

            // Verifica se a data da reclamação não foi fornecida (valor padrão).
            if (dto.DataReclamacao == default)
                throw new ValidationException("A data da reclamação é obrigatória.");
        }
    }
}
