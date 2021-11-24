using System;

namespace SafeFutureWebApplication.Models
{
    public interface IAuditable
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
