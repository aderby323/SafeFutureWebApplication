using System.Collections.Generic;

namespace SafeFutureWebApplication.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string Value { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
