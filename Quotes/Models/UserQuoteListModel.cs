using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.Models
{
    /// <summary>
    /// Class used to wrap a user data along with the list of his quotes.
    /// </summary>
    public class UserQuoteListModel
    {
        /// <summary>
        /// Get or Set the Quotes of the user.
        /// </summary>
        public List<QuoteModel> UserQuotes { get; set; }

        /// <summary>
        /// Get or Set the User Data Model
        /// </summary>
        public UserModel User { get; set; }
    }

    /// <summary>
    /// User Model
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Get or Set the Path of the profile icon.
        /// </summary>
        public string ProfileIconPath { get; set; }

        /// <summary>
        /// Get or Set the User Name
        /// </summary>
        public string UserName { get; set; }

    }

}