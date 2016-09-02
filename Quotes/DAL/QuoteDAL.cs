using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Quotes.Models;

namespace Quotes.DAL
{
    public class QuoteDAL
    {

        /// <summary>
        /// Find and return all the quotes of a user.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns> UserQuoteListModel object</returns>
        public UserQuoteListModel FindUserQuotes(int userId)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName = "@UsrId",
                    SqlDbType = SqlDbType.Int,
                    Value = userId
                }
            };

            var quotes = new UserQuoteListModel();
            
            var dataSet = DatabaseDAL.ExecuteProcedureDataSet("dbo.UserQuoteList", parameters);

            if (dataSet.Tables.Count != 0)
            {
                foreach (var item in dataSet.Tables[0].AsEnumerable())
                {
                    quotes.UserQuotes.Add(
                        new QuoteModel
                        {
                            QuoteId = item.Field<int>("QuoId"),
                            QuoteText = item.Field<string>("QuoText"),
                            OriginalDate = item.Field<DateTime>("QuoDate")
                        }
                    );
                }
            }
            

            return quotes;
        }

        /// <summary>
        /// SaveQuote can be used to save a new quote or update an existing one (decided on wether Quote Id is Null or not).
        /// </summary>
        /// <param name="newQuote">the Quote to be created or updated.</param>
        /// <returns></returns>
        public bool SaveQuote(QuoteModel newQuote)
        {
            var result = false;
            try
            {
                var param = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@text", SqlDbType = SqlDbType.Text, Value = newQuote.QuoteText },
                    new SqlParameter { ParameterName = "@UsrId", SqlDbType = SqlDbType.Int, Value = newQuote.UserId }
                };

                if (newQuote.QuoteId != null)
                {
                    param.Add(new SqlParameter { SqlDbType = SqlDbType.Int, Value = newQuote.QuoteId });
                }

                DatabaseDAL.ExecuteProcedure("dbo.QuoteSave", param);

                result = true;
            }
            catch (SqlException)
            { }

            return result;
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
            catch (Exception)
            {

                return false;
            }
            return true;
        }

        public static List<UserQuoteModel> FindQuote(string text)
        {

            var param = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@text",SqlDbType = SqlDbType.NVarChar, Value = text}
                };

            var quoteList = new List<UserQuoteModel>();
            var ds = DatabaseDAL.ExecuteProcedureDataSet("dbo.QuoteFind", param);
            if (ds != null)
                quoteList.AddRange(Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new UserQuoteModel()
                {
                    Quote = new QuoteModel()
                    {
                        QuoteId = item.Field<int>("QuoId"), 
                        QuoteText = item.Field<string>("QuoText"), 
                        OriginalDate = item.Field<DateTime>("QuoDate")
                    },
                    User = new UserModel()
                    {
                        UserName = item.Field<string>("UserName"), ProfileIconPath = "" //Not implemented yet
                    }
                }));
            return quoteList;
        }
    }
}