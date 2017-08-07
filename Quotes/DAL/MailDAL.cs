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
                    ParameterName = "@UserId",
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
                            Label = item.Field<string>("MaiLabel"),
                            Content = item.Field<string>("MaiContent"),
                            CreationDate = item.Field<DateTime>("MaiCreatedDate"),
                            ReceptionDate = item.Field<DateTime?>("MaiReceptionDate")
                        }
                    );
                }
            }

            return mails;

        }

        public static void SaveMail(MailModel newMail)
        {
            try
            {
                var param = new List<SqlParameter>
                {
                    
                    new SqlParameter { ParameterName = "@UsrId", SqlDbType = SqlDbType.Int, Value = newMail.SenderId },
                    new SqlParameter { ParameterName = "@Content", SqlDbType = SqlDbType.Text, Value = newMail.Content },
                    new SqlParameter { ParameterName = "@Label", SqlDbType = SqlDbType.Text, Value = newMail.Label },
                    new SqlParameter { ParameterName = "@Object", SqlDbType = SqlDbType.Text, Value = newMail.Object },
                    new SqlParameter { ParameterName = "@MailId", SqlDbType = SqlDbType.Text, Value = newMail.MailId },
                    new SqlParameter { ParameterName = "@MailParentId", SqlDbType = SqlDbType.Text, Value = newMail.ParentId },
                    new SqlParameter { ParameterName = "@MailSendDate", SqlDbType = SqlDbType.Text, Value = newMail.CreationDate },
                    new SqlParameter { ParameterName = "@RecipientListId", SqlDbType = SqlDbType.Text, Value = newMail.RecipientListId },
                };

                Database.ExecuteProcedure("dbo.MailSave", param);
            }
            catch (SqlException e)
            {
                   //TODO
            }

        }

        public static void MailFindById(int id)
        {


        }

        public static void MailRecipientFindById(int mailId, int userId)
        {


        }

        public static void MailUserFindById(int mailId,int userId)
        {


        }
    }
}