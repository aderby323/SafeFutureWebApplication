using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SafeFutureWebApplication.Models
{
    public class Recipient : IAuditable
    {
        public Recipient()
        {
            ProductsDistributed = new List<string>();
        }

        public Guid recipientId { get; set; }

        // public string Name { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(150, ErrorMessage = "First name must not exceed 150 characters.")]
        public string FirstName { get; set; }

        [StringLength(150, ErrorMessage = "Middle name must not exceed 150 characters.")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(150, ErrorMessage = "Last name must not exceed 150 characters.")]
        public string LastName { get; set; }

        // public string Zipcode { get; set; }
        [Required(ErrorMessage = "Zip code is required.")]
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        // public string HouseholdSize { get; set; }
        [Required(ErrorMessage = "Household size is required.")]
        [Range(1, 15, ErrorMessage = "Householde size range is only from 1 to 15.")]
        public int HouseholdSize { get; set; }

        public string Email { get; set; }

        public List<string> ProductsDistributed { get; set; }

        DateTime IAuditable.CreatedOn { get; set; }
        Guid IAuditable.CreatedBy { get; set; }
    }
}
