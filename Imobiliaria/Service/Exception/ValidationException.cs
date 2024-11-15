using System;

namespace Service.Exception
{
    public class ValidationException : System.Exception
    {
        // Construtor que recebe uma mensagem de erro e a passa para a classe base (System.Exception).
        public ValidationException(string message) : base(message) { }
    }
}
