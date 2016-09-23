using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public static List<AnnouncementModel> GetList(string status = null)
        {
            var parameters = new List<SqlParameter>();
            if (status != null)
                parameters.Add(new SqlParameter("@status", SqlDbType.VarChar) { Value = status });

            var annList = new List<AnnouncementModel>();
            var ds = Database.ExecuteProcedureDataSet("dbo.AnnouncementList", parameters);
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

        public static AnnouncementModel AnnouncementFind(int id)
        {
            return null;
        }

        public static List<AnnouncementModel> AnnouncementFilterList(string text, string userName, DateTime? fromDate, DateTime? toDate, int maxRecords, string order)
        {
            var param = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@text",SqlDbType = SqlDbType.NVarChar, Value = text},
                    new SqlParameter() {ParameterName = "@userName",SqlDbType = SqlDbType.VarChar, Value = userName},
                    new SqlParameter() {ParameterName = "@from",SqlDbType = SqlDbType.DateTime2, Value = fromDate},
                    new SqlParameter() {ParameterName = "@to",SqlDbType = SqlDbType.DateTime2, Value = toDate},
                    new SqlParameter() {ParameterName = "@maxRecords",SqlDbType = SqlDbType.Int, Value = maxRecords}
                };

            var annList = new List<AnnouncementModel>();
            var ds = Database.ExecuteProcedureDataSet("dbo.AnnouncementFilterList", param);
            if (ds != null)
                annList.AddRange(Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new AnnouncementModel()
                {
                        Id = item.Field<int>("AnnId"),
                        Title = item.Field<string>("AnnTitle"),
                        Author = item.Field<string>("UserName"),
                        CreationTime = item.Field<DateTime>("AnnCreationDate")
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