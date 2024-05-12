using System.Collections.Generic;
using ControlePedido.Domain.Base;

namespace ControlePedido.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Endereco { get; private set; }

        public Email(string email)
        {
            Endereco = email;
            ValidateValueObject();
        }

        public Email() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Endereco;
        }

        private void ValidateValueObject()
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            AssertionConcern.AssertArgumentMatches(pattern, Endereco, "E-mail inválido!");
        }
    }
}

