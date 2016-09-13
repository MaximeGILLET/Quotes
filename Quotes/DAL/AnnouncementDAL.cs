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

        public static bool SaveAnnouncement(AnnouncementModel model)
        {


            var parameters = new List<SqlParameter>();

            if (model.Id != null)
                parameters.Add(new SqlParameter("@AnnId",SqlDbType.Int) {Value=model.Id});

            DatabaseDAL.ExecuteProcedure("dbo.AnnouncementSave", parameters);

            return true;
        }
    }
}