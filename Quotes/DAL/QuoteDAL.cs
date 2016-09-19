using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Quotes.Models;
using System.Runtime.Caching;

namespace Quotes.DAL
{
    public static class QuoteDAL
    {


        private static MemoryCache _adminQuoteList { get; set; }

        /// <summary>
        /// Find and return all the quotes of a user.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns> UserQuoteListModel object</returns>
        public static UserQuoteListModel FindUserQuotes(int userId)
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

            var quotes = new UserQuoteListModel {UserQuotes = new List<QuoteModel>(),User = new UserModel()};
            
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
        public static bool SaveQuote(QuoteModel newQuote)
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

        public static void TagQuote(QuoteModel quote,string tag)
        {
            
                var param = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@TagLabel",SqlDbType = SqlDbType.VarChar, Value = tag},
                    new SqlParameter() {ParameterName = "@UserId",SqlDbType = SqlDbType.Int, Value = quote.UserId}
                };
                if (quote.QuoteId != null)
                {
                    param.Add(new SqlParameter() { ParameterName = "@QuoteId", SqlDbType = SqlDbType.Int, Value = quote.QuoteId });
                }

                DatabaseDAL.ExecuteProcedure("dbo.QuoteTagSave", param);

        }

        public static List<UserQuoteModel> FindQuotes(string text)
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

        /// <summary>
        /// Returns a list of quotes filtered on several optional criterias
        /// </summary>
        /// <param name="text"> Text that should occures once in the quote.</param>
        /// <param name="userName">Quotes posted by specific user only</param>
        /// <param name="fromDate">from date</param>
        /// <param name="toDate">to date</param>
        /// <param name="maxRecords">max number of records returned</param>
        /// <param name="order">order by date ASC or DESC</param>
        /// <returns></returns>
        public static List<UserQuoteModel> QuoteFilterList(string text, string userName, DateTime? fromDate, DateTime? toDate, int maxRecords, string order)
        {
            var param = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@text",SqlDbType = SqlDbType.NVarChar, Value = text},
                    new SqlParameter() {ParameterName = "@userName",SqlDbType = SqlDbType.VarChar, Value = userName},
                    new SqlParameter() {ParameterName = "@from",SqlDbType = SqlDbType.DateTime2, Value = fromDate},
                    new SqlParameter() {ParameterName = "@to",SqlDbType = SqlDbType.DateTime2, Value = toDate},
                    new SqlParameter() {ParameterName = "@maxRecords",SqlDbType = SqlDbType.Int, Value = maxRecords}
                };

            var quoteList = new List<UserQuoteModel>();
            var ds = DatabaseDAL.ExecuteProcedureDataSet("dbo.QuoteFilterList", param);
            if (ds != null)
                quoteList.AddRange(Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new UserQuoteModel()
                {
                    Quote = new QuoteModel()
                    {
                        QuoteId = item.Field<int>("QuoId"),
                        UserId = item.Field<int>("QuousrId"),
                        QuoteText = item.Field<string>("QuoText"),
                        OriginalDate = item.Field<DateTime>("QuoDate")
                    },
                    User = new UserModel()
                    {
                        UserName = item.Field<string>("UserName"),
                        ProfileIconPath = "Coming soon!" //Not implemented yet
                    }
                }));
            return quoteList;
        }

        public static DateTime? CheckLastUserPostDate(int userId)
        {
            var param = new List<SqlParameter>
            {
                new SqlParameter() {ParameterName = "@UsrId", SqlDbType = SqlDbType.Int, Value = userId},

            };
            var ds = DatabaseDAL.ExecuteProcedureDataSet("dbo.CheckLastUserPostDate", param);

            try
            {
                return ds.Tables[0].Rows[0].Field<DateTime>("QuoDate");

            }
            catch (Exception e)
            {
                return null;
            }
           
        }
    }
}