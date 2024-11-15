using Repository.Models;
using Service.Dto;

namespace Service.Parser
{
    public static class ReclamacaoParser
    {
        // Método para converter um DTO de Reclamacao em uma entidade Reclamacao.
        public static Reclamacao ToEntity(ReclamacaoDto dto)
        {
            return new Reclamacao
            {
                Id = dto.Id, // Mapeia o ID do DTO para a entidade para manter a identificação.
                Titulo = dto.Titulo, // Mapeia o título da reclamação do DTO para a entidade.
                Descricao = dto.Descricao, // Mapeia a descrição da reclamação do DTO para a entidade.
                DataReclamacao = dto.DataReclamacao, // Mapeia a data de reclamação do DTO para a entidade.
                Status = dto.Status, // Mapeia o status da reclamação do DTO para a entidade.
                Cliente = dto.Cliente // Mapeia o cliente do DTO para a entidade.
            };
        }

        // Método para converter uma entidade Reclamacao em um DTO de Reclamacao.
        public static ReclamacaoDto ToDto(Reclamacao entity)
        {
            return new ReclamacaoDto
            {
                Id = entity.Id, // Mapeia o ID da entidade para o DTO para manter a identificação.
                Titulo = entity.Titulo, // Mapeia o título da entidade para o DTO.
                Descricao = entity.Descricao, // Mapeia a descrição da entidade para o DTO.
                DataReclamacao = entity.DataReclamacao, // Mapeia a data de reclamação da entidade para o DTO.
                Status = entity.Status, // Mapeia o status da entidade para o DTO.
                Cliente = entity.Cliente // Mapeia o cliente da entidade para o DTO.
            };
        }
    }
}
