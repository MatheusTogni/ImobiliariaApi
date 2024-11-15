using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class PagamentoRepository
    {
        private readonly DbContext _context; // Contexto do banco de dados para gerenciar a tabela de pagamentos.

        public PagamentoRepository(DbContext context)
        {
            _context = context; // Inicializa o contexto recebido por injeção de dependência.
        }

        // Retorna uma lista de todos os pagamentos.
        public List<Pagamento> GetAll()
        {
            return _context.Set<Pagamento>().ToList(); // Usa o DbSet para buscar todos os registros de pagamentos.
        }

        // Retorna um pagamento específico pelo ID.
        public Pagamento GetById(int id)
        {
            return _context.Set<Pagamento>().Find(id); // Procura um pagamento pelo ID fornecido.
        }

        // Adiciona um novo pagamento à base de dados.
        public void Add(Pagamento pagamento)
        {
            _context.Set<Pagamento>().Add(pagamento); // Adiciona a entidade ao DbSet.
            _context.SaveChanges(); // Salva as alterações no banco de dados.
        }

        // Atualiza um pagamento existente.
        public void Update(Pagamento pagamento)
        {
            _context.Set<Pagamento>().Update(pagamento); // Atualiza a entidade no DbSet.
            _context.SaveChanges(); // Salva as alterações no banco de dados.
        }

        // Remove um pagamento pelo ID.
        public void Delete(int id)
        {
            var pagamento = _context.Set<Pagamento>().Find(id); // Busca o pagamento pelo ID.
            if (pagamento != null)
            {
                _context.Set<Pagamento>().Remove(pagamento); // Remove a entidade do DbSet se encontrada.
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
