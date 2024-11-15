namespace Service.Dto
{
    public class PagamentoDto
    {
        public int Id { get; set; } // Identificador único do pagamento, necessário para identificação e atualização manual.

        public string Descricao { get; set; } // Descrição detalhada do pagamento, explicando o motivo ou referência do pagamento.

        public decimal Valor { get; set; } // Valor monetário do pagamento.

        public DateTime DataPagamento { get; set; } // Data em que o pagamento foi efetuado.

        public bool Status { get; set; } // true se o pagamento foi realizado, false se está pendente.

        public string MetodoPagamento { get; set; } // Método pelo qual o pagamento foi realizado, como "Cartão de Crédito", "Boleto", "Transferência".
    }
}
