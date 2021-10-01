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

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(150, ErrorMessage = "First name must not exceed 150 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(150, ErrorMessage = "Last name must not exceed 150 characters.")]
        public string LastName { get; set; }

        [StringLength(200, ErrorMessage = "Address must not exceed 200 characters.")]
        public string Address { get; set; }

        [StringLength(200, ErrorMessage = "Address 2 must not exceed 200 characters.")]
        public string Address2 { get; set; }

        [StringLength(200, ErrorMessage = "City must not exceed 200 characters.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Zip code is required.")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Household size is required.")]
        [Range(1, 15, ErrorMessage = "Householde size range is only from 1 to 15.")]
        public int HouseholdSize { get; set; }
    }
}
