using Repository.Models;
using Service.Dto;

namespace Service.Parser
{
    public static class AnuncioParser
    {
        // Método para converter um DTO de Anuncio em uma entidade Anuncio.
        public static Anuncio ToEntity(AnuncioDto dto)
        {
            return new Anuncio
            {
                Id = dto.Id, // Mapeia o ID do DTO para a entidade.
                Titulo = dto.Titulo, // Mapeia o título do DTO para a entidade.
                Descricao = dto.Descricao, // Mapeia a descrição do DTO para a entidade.
                DataPublicacao = dto.DataPublicacao, // Mapeia a data de publicação do DTO para a entidade.
                Status = dto.Status, // Mapeia o status do DTO para a entidade.
                Plataforma = dto.Plataforma // Mapeia a plataforma do DTO para a entidade.
            };
        }

        // Método para converter uma entidade Anuncio em um DTO de Anuncio.
        public static AnuncioDto ToDto(Anuncio entity)
        {
            return new AnuncioDto
            {
                Id = entity.Id, // Mapeia o ID da entidade para o DTO.
                Titulo = entity.Titulo, // Mapeia o título da entidade para o DTO.
                Descricao = entity.Descricao, // Mapeia a descrição da entidade para o DTO.
                DataPublicacao = entity.DataPublicacao, // Mapeia a data de publicação da entidade para o DTO.
                Status = entity.Status, // Mapeia o status da entidade para o DTO.
                Plataforma = entity.Plataforma // Mapeia a plataforma da entidade para o DTO.
            };
        }
    }
}
