using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quotes.Models;
using System.Data;
using System.Data.SqlClient;

namespace Quotes.DAL
{
    public static class AnnouncementDAL
    {

        public static bool SaveAnnouncement(AnnouncementModel model,int userId)
        {
            var parameters = new List<SqlParameter>();

            if (model.Id != null)
                parameters.Add(new SqlParameter("@AnnId",SqlDbType.Int) {Value=model.Id});

            parameters.Add(new SqlParameter("@rawHtml", SqlDbType.NVarChar) { Value = model.RawHtml });
            parameters.Add(new SqlParameter("@title", SqlDbType.VarChar) { Value = model.Title });
            parameters.Add(new SqlParameter("@UsrId", SqlDbType.Int) { Value = userId });
            parameters.Add(new SqlParameter("@status", SqlDbType.VarChar) { Value = model.Status });

            Database.ExecuteProcedure("dbo.AnnouncementSave", parameters);

            return true;
        }

        public static List<AnnouncementModel> GetList()
        {
            var annList = new List<AnnouncementModel>();
            var ds = Database.ExecuteProcedureDataSet("dbo.AnnouncementList", null);
            if (ds != null)
                annList.AddRange(Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new AnnouncementModel()
                {
                   Id = item.Field<int>("AnnId"),
                   Title = item.Field<string>("AnnTitle"),
                   RawHtml = item.Field<string>("AnnRawHtmlBody"),
                   Status = item.Field<string>("Status"),
                   CreationTime = item.Field<DateTime>("AnnCreationDate"),
                   Author = item.Field<string>("Author"),
                   UpdateTime = item.Field<DateTime?>("AnnUpdateDate")

                }));
            return annList;

        }

        internal static void Delete(int id)
        {
            var parameters = new List<SqlParameter> {new SqlParameter("@AnnId", SqlDbType.Int) {Value = id}};
            Database.ExecuteProcedure("dbo.AnnouncementDelete", parameters);
        }
    }
}