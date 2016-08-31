using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class UserQuoteListModel
    {
        public List<QuoteModel> UserQuotes { get; set; }
        public UserModel User { get; set; }
    }

    public class UserModel
    {
        public string ProfileIconPath { get; set; }
        public string UserName { get; set; }

    }

}