using Repository.Models;
using Service.Dto;

namespace Service.Parser
{
    public static class ImovelParser
    {
        // Método para converter um DTO de Imovel em uma entidade Imovel.
        public static Imovel ToEntity(ImovelDto dto)
        {
            return new Imovel
            {
                Tipo = dto.Tipo, // Mapeia o tipo de imóvel do DTO para a entidade.
                Status = dto.Status, // Mapeia o status do imóvel do DTO para a entidade.
                Endereco = dto.Endereco // Mapeia o endereço do DTO para a entidade.
            };
        }

        // Método para converter uma entidade Imovel em um DTO de Imovel.
        public static ImovelDto ToDto(Imovel entity)
        {
            return new ImovelDto
            {
                Id = entity.Id, // Mapeia o ID da entidade para o DTO, garantindo a transferência de identificação.
                Tipo = entity.Tipo, // Mapeia o tipo da entidade para o DTO.
                Status = entity.Status, // Mapeia o status da entidade para o DTO.
                Endereco = entity.Endereco // Mapeia o endereço da entidade para o DTO.
            };
        }
    }
}
