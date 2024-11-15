using System.Collections.Generic;
using System.Linq;
using Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ContatoRepository
    {
        private readonly DbContext _context; // Contexto do banco de dados utilizado para interagir com a tabela de contatos.

        public ContatoRepository(DbContext context)
        {
            _context = context; // Inicializa o contexto recebido por injeção de dependência.
        }

        // Retorna uma lista de todos os contatos.
        public List<Contato> GetAll()
        {
            return _context.Set<Contato>().ToList(); // Usa o DbSet para buscar todos os registros de contatos.
        }

        // Retorna um contato específico pelo ID.
        public Contato GetById(int id)
        {
            return _context.Set<Contato>().Find(id); // Procura um contato pelo ID fornecido.
        }

        // Adiciona um novo contato à base de dados.
        public void Add(Contato contato)
        {
            _context.Set<Contato>().Add(contato); // Adiciona a entidade ao DbSet.
            _context.SaveChanges(); // Salva as alterações no banco de dados.
        }

        // Atualiza um contato existente.
        public void Update(Contato contato)
        {
            _context.Set<Contato>().Update(contato); // Atualiza a entidade no DbSet.
            _context.SaveChanges(); // Salva as alterações no banco de dados.
        }

        // Remove um contato pelo ID.
        public void Delete(int id)
        {
            var contato = _context.Set<Contato>().Find(id); // Busca o contato pelo ID.
            if (contato != null)
            {
                _context.Set<Contato>().Remove(contato); // Remove a entidade do DbSet se encontrada.
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
