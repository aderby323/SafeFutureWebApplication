using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Models
{
    public class Participant
    {
        public Guid ParticipantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public int HouseholdSize { get; set; }
    }
}
