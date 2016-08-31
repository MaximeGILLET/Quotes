using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class QuoteModel : SocialModel
    {
        public int QuoteId { get; set; }
        public int UserId { get; set; }
        public string QuoteText { get; set; }
        public DateTime OriginalDate { get; set; }
        public string FormatedDate { get{return OriginalDate.ToString("yyyy-MM-dd");}}

    }

    public class SocialModel
    {
        public int likes { get; set; }
        public int dislikes { get; set; }
        public int favorites { get; set; }
        public int reports { get; set; }
    }
}