namespace Repository.Models
{
    public class Imovel
    {
        public int Id { get; set; } // Identificador único do imóvel.

        public string Tipo { get; set; } // Tipo do imóvel, como "Rural", "Apartamento" ou "Casa".

        public string Status { get; set; } // Status atual do imóvel, como "À venda", "Aluguel mensal", etc.

        public string Endereco { get; set; } // Endereço completo onde o imóvel está localizado.
    }
}
