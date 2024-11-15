namespace Repository.Models
{
    public class Contato
    {
        public int Id { get; set; } // Identificador único do contato.

        public string Nome { get; set; } // Nome completo da pessoa que fez o contato.

        public string Telefone { get; set; } // Número de telefone para contato.

        public string Email { get; set; } // Endereço de e-mail do contato.

        public string Interesse { get; set; } // Indica o motivo do contato, como "Comprar", "Alugar", etc.
    }
}
