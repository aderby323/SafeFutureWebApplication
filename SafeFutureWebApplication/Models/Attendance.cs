using System;

namespace SafeFutureWebApplication.Models
{
    public class Attendance : IAuditable
    {
        public Guid AttendanceId { get; set; }
        public Guid RecipientId { get; set; }
        public DateTime EventDate { get; set; }
        public string ItemsDistributed { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        #region Navigation
        public virtual Recipient Recipient { get; set; }
        #endregion
    }
}
