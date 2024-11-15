using Microsoft.EntityFrameworkCore;
using Repository.Models;

public class MyDbContext : DbContext
{
    // Construtor que aceita opções para configurar o contexto, como a conexão ao banco de dados.
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

    // Definição dos DbSets, que representam as tabelas do banco de dados.
    public DbSet<Contato> Contatos { get; set; } // Representa a tabela de contatos no banco.
    public DbSet<Imovel> Imoveis { get; set; } // Representa a tabela de imóveis no banco.
    public DbSet<Anuncio> Anuncios { get; set; } // Representa a tabela de anúncios no banco.
    public DbSet<Manutencao> Manutencoes { get; set; } // Representa a tabela de manutenções no banco.
    public DbSet<Pagamento> Pagamentos { get; set; } // Representa a tabela de pagamentos no banco.
    public DbSet<Reclamacao> Reclamacoes { get; set; } // Representa a tabela de reclamações no banco.

    // Método para configurar o mapeamento e regras do modelo.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Chama a implementação base para garantir configurações padrão.

        // As configurações adicionais podem ser aplicadas aqui, como definições de chaves, índices e relacionamentos.
    }
}
