namespace Service.Dto
{
    public class ManutencaoDto
    {
        public int Id { get; set; } // Identificador único da manutenção, usado para criação e atualização.

        public string Descricao { get; set; } // Descrição detalhada da manutenção.

        public DateTime DataSolicitacao { get; set; } // Data em que a manutenção foi solicitada.

        public DateTime? DataConclusao { get; set; } // Data de conclusão da manutenção, pode ser nula se ainda não concluída.

        public bool Status { get; set; } // true para manutenção concluída, false para pendente.

        public string Responsavel { get; set; } // Nome da pessoa ou equipe responsável pela manutenção.

        public int ImovelId { get; set; } // Identificador do imóvel relacionado à manutenção.
    }
}
