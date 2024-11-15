using Repository.Models;
using Service.Dto;

namespace Service.Parser
{
    public static class ManutencaoParser
    {
        // Método para converter um DTO de Manutencao em uma entidade Manutencao.
        public static Manutencao ToEntity(ManutencaoDto dto)
        {
            return new Manutencao
            {
                Id = dto.Id, // Mapeia o ID do DTO para a entidade para manter a identificação.
                Descricao = dto.Descricao, // Mapeia a descrição da manutenção do DTO para a entidade.
                DataSolicitacao = dto.DataSolicitacao, // Mapeia a data de solicitação do DTO para a entidade.
                DataConclusao = dto.DataConclusao, // Mapeia a data de conclusão do DTO para a entidade, se houver.
                Status = dto.Status, // Mapeia o status da manutenção do DTO para a entidade.
                Responsavel = dto.Responsavel, // Mapeia o responsável pela manutenção do DTO para a entidade.
                ImovelId = dto.ImovelId // Mapeia o ID do imóvel relacionado do DTO para a entidade.
            };
        }

        // Método para converter uma entidade Manutencao em um DTO de Manutencao.
        public static ManutencaoDto ToDto(Manutencao entity)
        {
            return new ManutencaoDto
            {
                Id = entity.Id, // Mapeia o ID da entidade para o DTO para manter a identificação.
                Descricao = entity.Descricao, // Mapeia a descrição da entidade para o DTO.
                DataSolicitacao = entity.DataSolicitacao, // Mapeia a data de solicitação da entidade para o DTO.
                DataConclusao = entity.DataConclusao, // Mapeia a data de conclusão da entidade para o DTO, se houver.
                Status = entity.Status, // Mapeia o status da entidade para o DTO.
                Responsavel = entity.Responsavel, // Mapeia o responsável da entidade para o DTO.
                ImovelId = entity.ImovelId // Mapeia o ID do imóvel relacionado da entidade para o DTO.
            };
        }
    }
}
