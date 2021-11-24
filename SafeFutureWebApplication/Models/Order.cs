using System;
using System.ComponentModel.DataAnnotations;

namespace SafeFutureWebApplication.Models
{
    public class Order : IAuditable
    {

        public Guid OrderId { get; set; }

        public Guid RecipientId { get; set; }

        [Required]
        public bool RecievedItems { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedBy { get; set; }

    }
}
