using System.Collections.Generic;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.ValueObjects;

namespace ControlePedido.Domain.Entities
{
    public class Cliente : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public CPF Cpf { get; private set; }
        public Email Email { get; private set; }
        public virtual ICollection<Pedido> Pedidos { get; private set; }

        public Cliente(string nome, string cpf, string email)
        {
            Nome = nome;
            Cpf = new CPF(cpf);
            Email = new Email(email);

            ValidateEntity();
        }

        protected Cliente() { }

        private void ValidateEntity()
        {
            AssertionConcern.AssertArgumentNotEmpty(Nome, "O nome não pode estar vazio!");
            AssertionConcern.AssertArgumentLength(Nome, 100, "O nome não pode ultrapassar 100 caracters!");
        }
    }
}

