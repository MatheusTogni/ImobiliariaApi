using Service.Dto;
using Service.Exception;

namespace Service.Validate
{
    public static class ManutencaoValidator
    {
        // Método para validar os dados de um DTO de Manutencao.
        public static void Validate(ManutencaoDto dto)
        {
            // Verifica se a descrição da manutenção é nula ou vazia.
            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ValidationException("A descrição da manutenção é obrigatória.");

            // Verifica se a data de solicitação é a data padrão (não fornecida).
            if (dto.DataSolicitacao == default)
                throw new ValidationException("A data de solicitação é obrigatória.");

            // Verifica se o ID do imóvel é inválido (menor ou igual a zero).
            if (dto.ImovelId <= 0)
                throw new ValidationException("O ID do imóvel é obrigatório.");
        }
    }
}
