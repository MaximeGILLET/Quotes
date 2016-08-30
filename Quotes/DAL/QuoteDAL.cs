using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Quotes.Models;

namespace Quotes.DAL
{
    public static class QuoteDAL
    {

        public static UserQuoteListModel FindUserQuotes(string userName)
        {


            return null;
        }

        public static bool SaveQuote(QuoteModel newQuote)
        {
            try
            {
                var param = new List<SqlParameter>();
                param.Add(new SqlParameter() {SqlDbType = SqlDbType.Text, Value = newQuote.QuoteText});
                param.Add(new SqlParameter() {SqlDbType = SqlDbType.Int, Value = newQuote.UserId});

            }
            catch (Exception e)
            {

                return false;
            }
            return true;
        }
    }
}