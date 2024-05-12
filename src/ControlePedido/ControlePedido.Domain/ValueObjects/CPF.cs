using System.Collections.Generic;
using ControlePedido.Domain.Base;

namespace ControlePedido.Domain.ValueObjects
{
    public class CPF : ValueObject
    {
        public string Numero { get; private set; }

        public CPF(string numero)
        {
            Numero = numero;

            ValidateValueObject();
        }

        public CPF() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Numero;
        }

        private void ValidateValueObject()
        {
            var cpfValidation = IsValidCPF(Numero);
            AssertionConcern.AssertArgumentFalse(cpfValidation, "Cpf inválido!");
        }

        private bool IsValidCPF(string cpf)
        {
            int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];
            }

            int remainder = sum % 11;
            if (remainder < 2)
            {
                remainder = 0;
            }
            else
            {
                remainder = 11 - remainder;
            }

            string digit = remainder.ToString();
            tempCpf += digit;
            sum = 0;

            for (int i = 0; i < 10; i++)
            {
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];
            }

            remainder = sum % 11;
            if (remainder < 2)
            {
                remainder = 0;
            }
            else
            {
                remainder = 11 - remainder;
            }

            digit += remainder.ToString();
            return cpf.EndsWith(digit);
        }
    }
}

