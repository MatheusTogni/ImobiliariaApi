namespace Service.Dto
{
    public class AnuncioDto
    {
        public int Id { get; set; } // Identificador único do anúncio.

        public string Titulo { get; set; } // Título do anúncio que descreve a oferta ou imóvel.

        public string Descricao { get; set; } // Descrição detalhada do anúncio.

        public DateTime DataPublicacao { get; set; } // Data em que o anúncio foi publicado.

        public bool Status { get; set; } // true para ativo, false para inativo, indicando se o anúncio está disponível.

        public string Plataforma { get; set; } // Plataforma onde o anúncio é exibido, como "Site" ou "Redes Sociais".
    }
}
