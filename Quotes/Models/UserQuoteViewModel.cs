using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    public class UserQuoteViewModel : SocialModel
    {
        public int QuoteId { get; set; }
        public string Username { get; set; }
        public DateTime QuoteDate { get; set; }
        public string QuoteText { get; set; }

        public UserQuoteViewModel(UserQuoteModel model)
        {
            QuoteId = (int)model.Quote.QuoteId;
            QuoteDate = model.Quote.OriginalDate;
            QuoteText = model.Quote.QuoteText;
            Username = model.User.UserName;

            Likes = model.Quote.Likes;
            Dislikes = model.Quote.Dislikes;
            Favorites = model.Quote.Favorites;
            Reports = model.Quote.Reports;
            

        }
    }
}