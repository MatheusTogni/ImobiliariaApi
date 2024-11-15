using Service.Dto;
using Service.Exception;

namespace Service.Validate
{
    public static class ImovelValidator
    {
        // Método para validar os dados de um DTO de Imovel.
        public static void Validate(ImovelDto dto)
        {
            // Verifica se o tipo do imóvel é nulo ou vazio.
            if (string.IsNullOrWhiteSpace(dto.Tipo))
                throw new ValidationException("O tipo do imóvel é obrigatório.");

            // Verifica se o status do imóvel é nulo ou vazio.
            if (string.IsNullOrWhiteSpace(dto.Status))
                throw new ValidationException("O status do imóvel é obrigatório.");
        }
    }
}
