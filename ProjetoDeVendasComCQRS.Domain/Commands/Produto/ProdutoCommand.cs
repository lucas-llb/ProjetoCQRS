using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoDeVendasComCQRS.Domain.Commands.Produto
{
    public class ProdutoCommand : CommandBase
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public ProdutoCommand()
        {
        }
    }
}
