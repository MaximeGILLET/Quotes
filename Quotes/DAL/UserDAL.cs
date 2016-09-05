using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quotes.Models;
using System.Data.SqlClient;
using System.Data;

namespace Quotes.DAL
{
    public class UserDAL
    {
        /// <summary>
        /// Returns the list of profiles hold by a User.
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>List of profiles</returns>
        public static List<CustomRole> GetUserProfiles(int userId)
        {
            var param = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@UsrId",SqlDbType = SqlDbType.Int, Value = userId}
                };

            var pmlist = new List<CustomRole>();
            var ds = DatabaseDAL.ExecuteProcedureDataSet("dbo.UserProfileList", param);
            if (ds != null)
                pmlist.AddRange(Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new CustomRole()
                {                    
                    Id = item.Field<int>("PrfId"),
                    Name = item.Field<string>("PrfLabel"),                 
                   
                }));
            return pmlist;
        }

        /// <summary>
        /// Get the whole list of profiles.
        /// </summary>
        /// <returns>List of profiles</returns>
       /* public static List<ProfileModel> GetProfileList()
        {
            var pmlist = new List<ProfileModel>();
            var ds = DatabaseDAL.ExecuteProcedureDataSet("dbo.UserProfileList",null);
            if (ds != null)
                pmlist.AddRange(Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new ProfileModel()
                {
                    PrfId = item.Field<int>("PrfId"),
                    PrfLabel = item.Field<string>("PrfLabel")

                }));
            return pmlist;
        }*/
    }
}