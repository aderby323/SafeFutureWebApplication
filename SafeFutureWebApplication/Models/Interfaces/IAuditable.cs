using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Models
{
    public interface IAuditable
    {
        public DateTime CreatedOn { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
