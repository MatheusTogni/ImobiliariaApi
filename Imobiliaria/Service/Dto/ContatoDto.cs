namespace Service.Dto
{
    public class ContatoDto
    {
        public int Id { get; set; } // Identificador único do contato, necessário para criação e identificação manual.

        public string Nome { get; set; } // Nome completo da pessoa que fez o contato.

        public string Telefone { get; set; } // Número de telefone para contato.

        public string Email { get; set; } // Endereço de e-mail do contato.

        public string Interesse { get; set; } // Indica o motivo do contato, como "Comprar", "Alugar", etc.
    }
}
