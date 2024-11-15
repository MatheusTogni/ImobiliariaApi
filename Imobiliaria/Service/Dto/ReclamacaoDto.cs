namespace Service.Dto
{
    public class ReclamacaoDto
    {
        public int Id { get; set; } // Identificador único da reclamação, necessário para identificação e atualização manual.

        public string Titulo { get; set; } // Título da reclamação que resume o problema.

        public string Descricao { get; set; } // Descrição detalhada da reclamação, fornecendo mais contexto sobre o problema.

        public DateTime DataReclamacao { get; set; } // Data em que a reclamação foi registrada.

        public string Status { get; set; } // Status da reclamação, como "Pendente", "Em Andamento", "Resolvida".

        public string Cliente { get; set; } // Nome ou identificação do cliente que fez a reclamação.
    }
}
