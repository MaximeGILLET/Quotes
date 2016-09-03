using System;
using System.Collections.Generic;

namespace Quotes.Models
{

    /// <summary>
    /// Base Quote Model
    /// </summary>
    public class QuoteModel : SocialModel
    {
        public int? QuoteId { get; set; }
        public int UserId { get; set; }
        public string QuoteText { get; set; }
        public DateTime OriginalDate { get; set; }
        public string FormatedDate => OriginalDate.ToString("yyyy-MM-dd hh:mm:ss");
    }

    /// <summary>
    /// Base social model
    /// </summary>
    public class SocialModel
    {
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Favorites { get; set; }
        public int Reports { get; set; }
    }

    /// <summary>
    /// Class used to wrap a user data along with the list of his quotes.
    /// </summary>
    public class UserQuoteListModel
    {
        /// <summary>
        /// Get or Set the Quotes of the user.
        /// </summary>
        public List<QuoteModel> UserQuotes { get; set; } = new List<QuoteModel>();

        /// <summary>
        /// Get or Set the User Data Model
        /// </summary>
        public UserModel User { get; set; } = new UserModel();
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

    /// <summary>
    /// User Quote class
    /// </summary>
    public class UserQuoteModel
    {

        /// <summary>
        /// Get or Set the Quote of the User
        /// </summary>
        public QuoteModel Quote { get; set; }

        /// <summary>
        /// Get or Set the User Data Model
        /// </summary>
        public UserModel User { get; set; }
    }

}