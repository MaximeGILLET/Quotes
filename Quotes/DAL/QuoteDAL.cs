using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Quotes.Models;

namespace Quotes.DAL
{
    public class QuoteDAL
    {

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
    }
}