namespace Repository.Models
{
    public class Anuncio
    {
        public int Id { get; set; } // Identificador único do anúncio.

        public string Titulo { get; set; } // Título do anúncio para descrever o imóvel ou oferta.

        public string Descricao { get; set; } // Descrição detalhada do anúncio.

        public DateTime DataPublicacao { get; set; } // Data de publicação do anúncio.

        public bool Status { get; set; } // true para ativo, false para inativo, indicando a disponibilidade do anúncio.

        public string Plataforma { get; set; } // Indica onde o anúncio será exibido, como "Site" ou "Redes Sociais".
    }
}
