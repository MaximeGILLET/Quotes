using System.Collections.Generic;

namespace Quotes.Models
{
    public class UserQuoteListModel
    {
        public List<QuoteModel> UserQuotes { get; set; } = new List<QuoteModel>();

        public UserModel User { get; set; } = new UserModel();
    }
}