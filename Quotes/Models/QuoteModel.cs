using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class QuoteModel
    {
        public int QuoteId { get; set; }
        public int UserId { get; set; }
        public string QuoteText { get; set; }
        public DateTime OriginalDate { get; set; }

    }
}