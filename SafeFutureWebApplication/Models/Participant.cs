using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Models
{
    public class Participant
    {
        public Guid ParticipantId { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "First name must not exceed 150 characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Last name must not exceed 150 characters.")]
        public string LastName { get; set; }

        [StringLength(200, ErrorMessage = "Address must not exceed 200 characters.")]
        public string Address { get; set; }

        [StringLength(200, ErrorMessage = "Address 2 must not exceed 200 characters.")]
        public string Address2 { get; set; }

        [StringLength(200, ErrorMessage = "City must not exceed 200 characters.")]
        public string City { get; set; }

        [Required]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Required]
        public int HouseholdSize { get; set; }
    }
}
