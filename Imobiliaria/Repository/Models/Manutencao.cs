namespace Repository.Models
{
    public class Manutencao
    {
        public int Id { get; set; } // Identificador único da manutenção.

        public string Descricao { get; set; } // Descrição detalhada da manutenção necessária.

        public DateTime DataSolicitacao { get; set; } // Data em que a manutenção foi solicitada.

        public DateTime? DataConclusao { get; set; } // Data de conclusão da manutenção; pode ser nulo se não estiver concluída.

        public bool Status { get; set; } // true para concluída, false para pendente, indicando o estado da manutenção.

        public string Responsavel { get; set; } // Nome da pessoa ou equipe responsável pela manutenção.

        public int ImovelId { get; set; } // Relaciona a manutenção a um imóvel específico, se necessário.
    }
}
