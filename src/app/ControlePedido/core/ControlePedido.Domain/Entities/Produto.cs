using System;
using System.Collections.Generic;
using ControlePedido.Domain.Base;
using ControlePedido.Domain.Enums;
using ControlePedido.Domain.ValueObjects;

namespace ControlePedido.Domain.Entities
{
    public class Produto : Entity, IAggregateRoot
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public Categoria Categoria { get; private set; }
        public Imagem Imagem { get; private set; }
        public virtual ICollection<PedidoItem> ItensPedido { get; private set; }

        protected Produto(){}

        public Produto(string nome, decimal preco, Categoria categoria, string descricao, Imagem imagem)
        {
            Nome = nome;
            Preco = preco;
            Categoria = categoria;
            Descricao = descricao;
            Imagem = imagem;
            ValidateEntity();
        }

        private void ValidateEntity(){
            AssertionConcern.AssertArgumentNotEmpty(Nome, "O Nome não pode estar vazio!");
            AssertionConcern.AssertArgumentLength(Nome, 100, "O nome não pode ultrapassar 100 caracters!");
            AssertionConcern.AssertArgumentNotEmpty(Descricao, "A descrição não pode estar vazia!");
            AssertionConcern.AssertArgumentLength(Descricao, 500, "A descricao não pode ultrapassar 500 caracters!");
            AssertionConcern.AssertGratherThanValue(Preco, 0, "O preço deve ser maior do que 0.");
            AssertionConcern.AssertArgumentNotNull(Categoria, "A categoria não pode estar vazia!");
        }
    }
}
