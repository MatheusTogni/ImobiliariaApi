using Service.Dto;
using Service.Exception;

namespace Service.Validate
{
    public static class ContatoValidator
    {
        // Método para validar os dados de um DTO de Contato.
        public static void Validate(ContatoDto dto)
        {
            // Verifica se o nome do contato é nulo ou vazio.
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new ValidationException("O nome do contato é obrigatório.");

            // Verifica se o telefone do contato é nulo ou vazio.
            if (string.IsNullOrWhiteSpace(dto.Telefone))
                throw new ValidationException("O telefone do contato é obrigatório.");

            // Verifica se o e-mail do contato é nulo ou vazio.
            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ValidationException("O email do contato é obrigatório.");
        }
    }
}
