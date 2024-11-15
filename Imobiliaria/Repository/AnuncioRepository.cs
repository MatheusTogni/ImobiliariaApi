using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class AnuncioRepository
    {
        private readonly DbContext _context; // Contexto do banco de dados utilizado para acessar as tabelas.

        public AnuncioRepository(DbContext context)
        {
            _context = context; // Inicializa o contexto recebido por injeção de dependência.
        }

        // Retorna uma lista de todos os anúncios.
        public List<Anuncio> GetAll()
        {
            return _context.Set<Anuncio>().ToList(); // Usa o DbSet para buscar todos os registros de anúncios.
        }

        // Retorna um anúncio específico pelo ID.
        public Anuncio GetById(int id)
        {
            return _context.Set<Anuncio>().Find(id); // Procura um anúncio pelo ID fornecido.
        }

        // Adiciona um novo anúncio à base de dados.
        public void Add(Anuncio anuncio)
        {
            _context.Set<Anuncio>().Add(anuncio); // Adiciona a entidade ao DbSet.
            _context.SaveChanges(); // Salva as alterações no banco.
        }

        // Atualiza um anúncio existente.
        public void Update(Anuncio anuncio)
        {
            _context.Set<Anuncio>().Update(anuncio); // Atualiza a entidade no DbSet.
            _context.SaveChanges(); // Salva as alterações no banco.
        }

        // Remove um anúncio pelo ID.
        public void Delete(int id)
        {
            var anuncio = _context.Set<Anuncio>().Find(id); // Busca o anúncio pelo ID.
            if (anuncio != null)
            {
                _context.Set<Anuncio>().Remove(anuncio); // Remove a entidade do DbSet se for encontrada.
                _context.SaveChanges(); // Salva as alterações no banco.
            }
        }

        // Desanexa uma entidade do contexto para evitar conflitos de rastreamento.
        public void Detach<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Detached; // Define o estado da entidade como 'Detached'.
        }
    }
}
