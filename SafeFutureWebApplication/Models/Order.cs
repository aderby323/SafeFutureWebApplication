using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Models
{
    public class Order : IAuditable
    {
        public Guid OrderId { get; set; }
        public Guid RecipientId { get; set; }
        public bool RecievedItems { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
