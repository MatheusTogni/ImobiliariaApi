using System.Collections.Generic;
using System.Linq;
using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ImovelRepository
    {
        private readonly DbContext _context; // Contexto do banco de dados para interagir com a tabela de imóveis.

        public ImovelRepository(DbContext context)
        {
            _context = context; // Inicializa o contexto recebido por injeção de dependência.
        }

        // Retorna uma lista de todos os imóveis.
        public List<Imovel> GetAll()
        {
            return _context.Set<Imovel>().ToList(); // Usa o DbSet para buscar todos os registros de imóveis.
        }

        // Retorna um imóvel específico pelo ID.
        public Imovel GetById(int id)
        {
            return _context.Set<Imovel>().Find(id); // Procura um imóvel pelo ID fornecido.
        }

        // Adiciona um novo imóvel à base de dados.
        public void Add(Imovel imovel)
        {
            _context.Set<Imovel>().Add(imovel); // Adiciona a entidade ao DbSet.
            _context.SaveChanges(); // Salva as alterações no banco de dados.
        }

        // Atualiza um imóvel existente.
        public void Update(Imovel imovel)
        {
            _context.Set<Imovel>().Update(imovel); // Atualiza a entidade no DbSet.
            _context.SaveChanges(); // Salva as alterações no banco de dados.
        }

        // Remove um imóvel pelo ID.
        public void Delete(int id)
        {
            var imovel = _context.Set<Imovel>().Find(id); // Busca o imóvel pelo ID.
            if (imovel != null)
            {
                _context.Set<Imovel>().Remove(imovel); // Remove a entidade do DbSet se encontrada.
                _context.SaveChanges(); // Salva as alterações no banco de dados.
            }
        }

        // Desanexa uma entidade do contexto para evitar conflitos de rastreamento.
        public void Detach<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Detached; // Define o estado da entidade como 'Detached' para evitar problemas de rastreamento.
        }
    }
}
