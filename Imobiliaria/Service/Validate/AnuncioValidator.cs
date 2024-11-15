using Service.Dto;
using Service.Exception;

namespace Service.Validate
{
    public static class AnuncioValidator
    {
        // Método para validar os dados de um DTO de Anuncio.
        public static void Validate(AnuncioDto dto)
        {
            // Verifica se o título do anúncio é nulo ou vazio.
            if (string.IsNullOrWhiteSpace(dto.Titulo))
                throw new ValidationException("O título do anúncio é obrigatório.");

            // Verifica se a descrição do anúncio é nula ou vazia.
            if (string.IsNullOrWhiteSpace(dto.Descricao))
                throw new ValidationException("A descrição do anúncio é obrigatória.");

            // Verifica se a data de publicação não foi fornecida (valor padrão).
            if (dto.DataPublicacao == default)
                throw new ValidationException("A data de publicação é obrigatória.");
        }
    }
}
