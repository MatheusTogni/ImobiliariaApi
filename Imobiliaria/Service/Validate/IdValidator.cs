using Repository;
using Service.Exception;

namespace Service.Validate
{
    public static class IdValidator
    {
        // Método para validar se um ID é único no repositório de contatos.
        public static void ValidateUniqueId(int id, ContatoRepository repository)
        {
            // Verifica se já existe uma entidade com o ID especificado no repositório de contatos.
            var existingEntity = repository.GetById(id);
            if (existingEntity != null)
            {
                // Lança uma exceção de validação se o ID já existir.
                throw new ValidationException($"Já existe um registro de contato com o Id {id}.");
            }
        }

        // Método para validar se um ID é único no repositório de imóveis.
        public static void ValidateUniqueId(int id, ImovelRepository repository)
        {
            // Verifica se já existe uma entidade com o ID especificado no repositório de imóveis.
            var existingEntity = repository.GetById(id);
            if (existingEntity != null)
            {
                // Lança uma exceção de validação se o ID já existir.
                throw new ValidationException($"Já existe um registro de imóvel com o Id {id}.");
            }
        }
    }
}
