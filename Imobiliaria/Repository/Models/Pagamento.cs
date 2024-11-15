namespace Repository.Models
{
    public class Pagamento
    {
        public int Id { get; set; } // Identificador único do pagamento.

        public string Descricao { get; set; } // Descrição detalhada do pagamento, como motivo ou detalhes adicionais.

        public decimal Valor { get; set; } // Valor monetário do pagamento.

        public DateTime DataPagamento { get; set; } // Data em que o pagamento foi realizado ou está programado para ser realizado.

        public bool Status { get; set; } // true para indicar que o pagamento foi efetuado, false para pendente.

        public string MetodoPagamento { get; set; } // Método utilizado para o pagamento, como "Cartão de Crédito", "Boleto", "Transferência".
    }
}
