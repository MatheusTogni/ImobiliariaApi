using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
    public class ManutencaoRepository
    {
        private readonly DbContext _context; // Contexto do banco de dados para interagir com a tabela de manutenções.

        public ManutencaoRepository(DbContext context)
        {
            _context = context; // Inicializa o contexto recebido por injeção de dependência.
        }

        // Retorna uma lista de todas as manutenções.
        public List<Manutencao> GetAll()
        {
            return _context.Set<Manutencao>().ToList(); // Usa o DbSet para buscar todos os registros de manutenções.
        }

        // Retorna uma manutenção específica pelo ID.
        public Manutencao GetById(int id)
        {
            return _context.Set<Manutencao>().Find(id); // Procura uma manutenção pelo ID fornecido.
        }

        // Adiciona uma nova manutenção à base de dados.
        public void Add(Manutencao manutencao)
        {
            _context.Set<Manutencao>().Add(manutencao); // Adiciona a entidade ao DbSet.
            _context.SaveChanges(); // Salva as alterações no banco de dados.
        }

        // Atualiza uma manutenção existente.
        public void Update(Manutencao manutencao)
        {
            _context.Set<Manutencao>().Update(manutencao); // Atualiza a entidade no DbSet.
            _context.SaveChanges(); // Salva as alterações no banco de dados.
        }

        // Remove uma manutenção pelo ID.
        public void Delete(int id)
        {
            var manutencao = _context.Set<Manutencao>().Find(id); // Busca a manutenção pelo ID.
            if (manutencao != null)
            {
                _context.Set<Manutencao>().Remove(manutencao); // Remove a entidade do DbSet se encontrada.
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
