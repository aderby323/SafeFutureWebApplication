using System.Collections.Generic;
using System.Linq;

namespace SafeFutureWebApplication.Models.ViewModels
{
    public partial class GetRecipientsViewModel
    {
        
        public IEnumerable<Recipient> Recipients { get; set; }
        public int CurrentPage { get; set; }
        public int NumOfPages { get; private set; }
        public int PreviousPage { get; private set; }
        public int NextPage { get; private set; }

        public GetRecipientsViewModel(IEnumerable<Recipient> Recipients, int CurrentPage, int NumOfPages)
        {
            this.Recipients = Recipients.ToList();
            this.CurrentPage = CurrentPage;
            this.NumOfPages = NumOfPages;
            PreviousPage = CurrentPage - 1;
            NextPage = CurrentPage + 1;
        }
    }
}
