using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quotes.DAL
{
    using Quotes.Models;
    using System.Data;
    using System.Data.SqlClient;

    public static class MailDAL
    {

        public static List<MailViewModel> UserMailList(int userId, bool? sent = null, bool? arichived = null)
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

            var mails = new List<MailViewModel>();

            var dataSet = Database.ExecuteProcedureDataSet("dbo.MailList", parameters);

            if (dataSet.Tables.Count != 0)
            {
                foreach (var item in dataSet.Tables[0].AsEnumerable())
                {
                    mails.Add(
                        new MailViewModel
                        {
                            MailId = item.Field<int>("MaiId"),
                            senderName = item.Field<string>("UserName"),
                            Object = item.Field<string>("MaiObject"),
                            Content = item.Field<string>("MaiContent"),
                            CreationDate = item.Field<DateTime>("MaiCreatedDate")
                        }
                    );
                }
            }

            return mails;

        }

        public static void SaveMail(MailModel model)
        {


        }
    }
}