using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QueueSystem.Domain.Entities
{
    public class Background
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public DateTime CalledAt { get; set; } = DateTime.Now;
    }
}