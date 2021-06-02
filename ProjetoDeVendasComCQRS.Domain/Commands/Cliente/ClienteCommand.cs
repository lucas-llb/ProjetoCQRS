using System;

namespace ProjetoDeVendasComCQRS.Domain.Commands.Cliente
{
    public class ClienteCommand : CommandBase
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public ClienteCommand()
        {
        }
    }
}
