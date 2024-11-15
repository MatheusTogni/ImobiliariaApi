using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class ReclamacaoRepository
    {
        private readonly DbContext _context; // Contexto do banco de dados para gerenciar a tabela de reclamações.

        public ReclamacaoRepository(DbContext context)
        {
            _context = context; // Inicializa o contexto recebido por injeção de dependência.
        }

        // Retorna uma lista de todas as reclamações.
        public List<Reclamacao> GetAll()
        {
            return _context.Set<Reclamacao>().ToList(); // Usa o DbSet para buscar todos os registros de reclamações.
        }

        // Retorna uma reclamação específica pelo ID.
        public Reclamacao GetById(int id)
        {
            return _context.Set<Reclamacao>().Find(id); // Procura uma reclamação pelo ID fornecido.
        }

        // Adiciona uma nova reclamação à base de dados.
        public void Add(Reclamacao reclamacao)
        {
            _context.Set<Reclamacao>().Add(reclamacao); // Adiciona a entidade ao DbSet.
            _context.SaveChanges(); // Salva as alterações no banco de dados.
        }

        // Atualiza uma reclamação existente.
        public void Update(Reclamacao reclamacao)
        {
            _context.Set<Reclamacao>().Update(reclamacao); // Atualiza a entidade no DbSet.
            _context.SaveChanges(); // Salva as alterações no banco de dados.
        }

        // Remove uma reclamação pelo ID.
        public void Delete(int id)
        {
            var reclamacao = _context.Set<Reclamacao>().Find(id); // Busca a reclamação pelo ID.
            if (reclamacao != null)
            {
                _context.Set<Reclamacao>().Remove(reclamacao); // Remove a entidade do DbSet se encontrada.
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
