using System;
using System.Collections.Generic;
using System.Linq;

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

        public string FormatedDate
        {
            get { return OriginalDate.ToString("yyyy-MM-dd hh:mm:ss"); }
        }

    }

    /// <summary>
    /// Base social model
    /// </summary>
    public class SocialModel
    {
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Stars { get; set; }
        public int Reports { get; set; }
        public List<Tag> Tags { get; set; }

        public SocialModel()
        {
            Tags = new List<Tag>();
        }

        public double SumActivity()
        {
            return Likes + Dislikes + Stars+ Tags.Sum(x => x.Amount);
        }
    }

    public class Tag
    {
        public int TagType { get; set; }
        public string TagLabel { get; set; }
        public int Amount { get; set; }
    }

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

    public class UserModel
    {

        public string ProfileIconPath { get; set; }
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