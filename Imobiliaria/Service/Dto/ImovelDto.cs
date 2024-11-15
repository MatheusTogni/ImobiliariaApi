namespace Service.Dto
{
    public class ImovelDto
    {
        public int Id { get; set; } // Identificador único do imóvel, necessário para criação e identificação manual.

        public string Tipo { get; set; } // Tipo do imóvel, como "Rural", "Apartamento", "Casa".

        public string Status { get; set; } // Status atual do imóvel, como "À venda", "Aluguel mensal", etc.

        public string Endereco { get; set; } // Endereço completo do imóvel.
    }
}
