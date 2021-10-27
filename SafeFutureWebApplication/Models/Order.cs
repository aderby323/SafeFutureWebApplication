using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid RecipientId { get; set; }
        public bool RecievedItems { get; set; }
    }
}
