using Repository.Models;
using Service.Dto;

namespace Service.Parser
{
    public static class ContatoParser
    {
        // Método para converter um DTO de Contato em uma entidade Contato.
        public static Contato ToEntity(ContatoDto dto)
        {
            return new Contato
            {
                Nome = dto.Nome, // Mapeia o nome do DTO para a entidade.
                Telefone = dto.Telefone, // Mapeia o telefone do DTO para a entidade.
                Email = dto.Email, // Mapeia o e-mail do DTO para a entidade.
                Interesse = dto.Interesse // Mapeia o interesse do DTO para a entidade.
            };
        }

        // Método para converter uma entidade Contato em um DTO de Contato.
        public static ContatoDto ToDto(Contato entity)
        {
            return new ContatoDto
            {
                Id = entity.Id, // Mapeia o ID da entidade para o DTO, garantindo a transferência de identificação.
                Nome = entity.Nome, // Mapeia o nome da entidade para o DTO.
                Telefone = entity.Telefone, // Mapeia o telefone da entidade para o DTO.
                Email = entity.Email, // Mapeia o e-mail da entidade para o DTO.
                Interesse = entity.Interesse // Mapeia o interesse da entidade para o DTO.
            };
        }
    }
}
