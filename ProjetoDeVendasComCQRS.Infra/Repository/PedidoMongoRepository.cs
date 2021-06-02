using MongoDB.Bson;
using MongoDB.Driver;
using ProjetoDeVendasComCQRS.Domain.Document;
using ProjetoDeVendasComCQRS.Domain.Interfaces.Repository;
using ProjetoDeVendasComCQRS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoDeVendasComCQRS.Infra.Repository
{
    public class PedidoMongoRepository : IPedidoMongoRepository
    {
        private readonly IMongoCollection<PedidoDocument> _pedidos;
        public PedidoMongoRepository(IPedidosDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _pedidos = database.GetCollection<PedidoDocument>(settings.CollectionName);
        }

        public List<PedidoDocument> Get()
        {
            return _pedidos.FindAsync(p => true).Result.ToList();
        }

        public PedidoDocument GetById(Guid id)
        {
            return _pedidos.FindAsync(p => p.IdBanco == id).Result.FirstOrDefault();
        }

        public async Task<PedidoDocument> CreateAsync(PedidoDocument pedido)
        {
            await _pedidos.InsertOneAsync(pedido);
            return pedido;
        }

        public async Task UpdateAsync(Guid id, PedidoDocument pedido)
        {
            await _pedidos.ReplaceOneAsync(p => p.IdBanco == id, pedido);
        }

        public async Task RemoveAsync(Guid id)
        {
            await _pedidos.DeleteOneAsync(p => p.IdBanco == id);
        }
    }
}
