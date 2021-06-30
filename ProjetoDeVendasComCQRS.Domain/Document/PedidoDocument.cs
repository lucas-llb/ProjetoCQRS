using MongoDB.Bson;
using ProjetoDeVendasComCQRS.Domain.Entidades;
using System;

namespace ProjetoDeVendasComCQRS.Domain.Document
{
    public class PedidoDocument
    {
        public ObjectId _id { get; set; }
        public Guid IdBanco { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int Quantidade { get; set; }
        public double ValorTotal { get; set; }
        public DateTime Data { get; set; }
        public DateTime UltimaAlteracao { get; set; }

        public PedidoDocument()
        {
        }
    }
}
