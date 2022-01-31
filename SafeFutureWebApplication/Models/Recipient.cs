using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SafeFutureWebApplication.Models
{
    public class Recipient : IAuditable
    {
        public Recipient()
        {
            Attendance = new HashSet<Attendance>();
        }

        public Guid RecipientId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required")]

        public string LastName { get; set; }
        public string Address1 { get; set; }

        public string Address2 { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$", ErrorMessage = "Please enter a valid US zip code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Household size is required and must be greater than 0")]
        [Range(1, 1000, ErrorMessage = "Please enter a valid household size (1 - 1000)")]
        public int HouseholdSize { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        #region Naviagtion
        public virtual ICollection<Attendance> Attendance { get; set; }
        #endregion
    }
}
