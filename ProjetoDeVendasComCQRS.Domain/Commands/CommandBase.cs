using System;

namespace ProjetoDeVendasComCQRS.Domain.Commands
{
    public class CommandBase
    {
        public Guid MessageId { get; set; }
    }
}
