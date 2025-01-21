using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueSystem.Domain.Entities
{
    public class Queue
    {
        public int Id { get; set; }

        public string Category { get; set; } = string.Empty;

        public ICollection<Client> Clients { get; } = new List<Client>();
    }
}