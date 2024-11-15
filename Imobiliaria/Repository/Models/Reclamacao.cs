namespace Repository.Models
{
    public class Reclamacao
    {
        public int Id { get; set; } // Identificador único da reclamação.

        public string Titulo { get; set; } // Título da reclamação para indicar resumidamente o assunto.

        public string Descricao { get; set; } // Descrição detalhada da reclamação feita pelo cliente.

        public DateTime DataReclamacao { get; set; } // Data em que a reclamação foi registrada.

        public string Status { get; set; } // Status atual da reclamação, como "Pendente", "Em Andamento" ou "Resolvida".

        public string Cliente { get; set; } // Nome ou identificação do cliente que fez a reclamação.
    }
}
