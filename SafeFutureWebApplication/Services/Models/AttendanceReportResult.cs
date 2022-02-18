using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeFutureWebApplication.Services.Models
{
    public class AttendanceReportResult
    {
        public Guid AttendanceId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string ZipCode { get; set; }
        public int HouseholdSize { get; set; }
        public DateTime EventDate { get; set; }    }
}
