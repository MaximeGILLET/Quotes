using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Quotes.Models;

namespace Quotes.DAL
{
    public static class QuoteDAL
    {

        /// <summary>
        /// Find and return all the quotes of a user.
        /// </summary>
        /// <param name="UsrId">Id of the user</param>
        /// <returns> UserQuoteListModel object</returns>
        public static UserQuoteListModel FindUserQuotes(int UsrId)
        {
            var param = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@UsrId",SqlDbType = SqlDbType.Int, Value = UsrId}
                };

            var quotes = new UserQuoteListModel();
            quotes.UserQuotes = new List<QuoteModel>();
            quotes.User = new UserModel();
            var ds= DatabaseDAL.ExecuteProcedureDataSet("dbo.UserQuoteList", param);
            if (ds != null)
            foreach (var item in ds.Tables[0].AsEnumerable())
            {
                quotes.UserQuotes.Add(
                    new QuoteModel()
                    {
                        QuoteId = item.Field<int>("QuoId"),
                        QuoteText = item.Field<string>("QuoText"),
                        OriginalDate = item.Field<DateTime>("QuoDate")
                    }
                );
            }

            return quotes;
        }

        /// <summary>
        /// SaveQuote can be used to save a new quote or update an existing one (decided on wether Quote Id is Null or not).
        /// </summary>
        /// <param name="newQuote">the Quote to be created or updated.</param>
        /// <returns></returns>
        public static bool SaveQuote(QuoteModel newQuote)
        {
            try
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@text",SqlDbType = SqlDbType.Text, Value = newQuote.QuoteText},
                    new SqlParameter() {ParameterName = "@UsrId",SqlDbType = SqlDbType.Int, Value = newQuote.UserId}
                };
                if (newQuote.QuoteId != null)
                {
                    param.Add(new SqlParameter() { SqlDbType = SqlDbType.Int, Value = newQuote.QuoteId });
                }

                DatabaseDAL.ExecuteProcedure("dbo.QuoteSave",param);

            }
            catch (Exception e)
            {

                return false;
            }
            return true;
        }

        public static bool TagQuote(QuoteModel newQuote,string tag)
        {
            try
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@tag",SqlDbType = SqlDbType.VarChar, Value = tag},
                    new SqlParameter() {ParameterName = "@UsrId",SqlDbType = SqlDbType.Int, Value = newQuote.UserId}
                };
                if (newQuote.QuoteId != null)
                {
                    param.Add(new SqlParameter() { SqlDbType = SqlDbType.Int, Value = newQuote.QuoteId });
                }

                DatabaseDAL.ExecuteProcedure("dbo.QuoteTag", param);

            }
            catch (Exception e)
            {

                return false;
            }
            return true;
        }
    }
}