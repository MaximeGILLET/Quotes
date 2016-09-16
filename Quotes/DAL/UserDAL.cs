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

        /// <summary>
        /// Get the last 10 users registered in database (with mail confirmed).
        /// </summary>
        /// <returns>return list of username , registration date</returns>
        public static List<LastRegisterUserViewModel> LastRegisteredUsers()
        {
            var pmlist = new List<LastRegisterUserViewModel>();
            var ds = DatabaseDAL.ExecuteProcedureDataSet("dbo.LastRegisterUserList", null);
            if (ds != null)
                pmlist.AddRange(Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new LastRegisterUserViewModel()
                {
                    Username = item.Field<string>("UserName"),
                    RegisterDate = item.Field<DateTime>("RegistrationDate"),

                }));
            return pmlist;
        }

        /// <summary>
        /// Get a cluster object containing several list of users for different periods
        /// </summary>
        /// <returns> return a cluster of user lists</returns>
        public static TopUserClusterViewModel GetTopUsers()
        {

            var userCluster = new TopUserClusterViewModel
            {
                AllTimeUsers = new List<TopUserViewModel>(),
                MonthUsers = new List<TopUserViewModel>(),
                WeekUsers = new List<TopUserViewModel>()
            };

            //Get All time top Users
            var ds = DatabaseDAL.ExecuteProcedureDataSet("dbo.TopUserList", null);
            if (ds != null)
                userCluster.AllTimeUsers.AddRange(Enumerable.Select(ds.Tables[0].AsEnumerable(), item => new TopUserViewModel()
                {
                    Username = item.Field<string>("UserName"),
                    Points = item.Field<int>("Points"),

                }));

            //Get Last Month top Users
            var firstDayOfLastMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
            var lastDayOfLastMonth = firstDayOfLastMonth.AddMonths(1).AddDays(-1);
            var param = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@from",SqlDbType = SqlDbType.DateTime2, Value = firstDayOfLastMonth},
                     new SqlParameter() {ParameterName = "@to",SqlDbType = SqlDbType.DateTime2, Value = lastDayOfLastMonth}
                };
            var ds1 = DatabaseDAL.ExecuteProcedureDataSet("dbo.TopUserList", param);
            if (ds1 != null)
                userCluster.MonthUsers.AddRange(Enumerable.Select(ds1.Tables[0].AsEnumerable(), item => new TopUserViewModel()
                {
                    Username = item.Field<string>("UserName"),
                    Points = item.Field<int>("Points"),

                }));

            //Get Last Week top Users
            DateTime input = DateTime.Now;
            int delta = DayOfWeek.Monday - input.DayOfWeek;
            DateTime monday = input.AddDays(delta);
            var lastMonday = monday.AddDays(-7);
            var lastSunday = monday.AddDays(-1);
            param = new List<SqlParameter>
                {
                    new SqlParameter() {ParameterName = "@from",SqlDbType = SqlDbType.Date, Value = lastMonday},
                     new SqlParameter() {ParameterName = "@to",SqlDbType = SqlDbType.Date, Value = lastSunday}
                };
            var ds2 = DatabaseDAL.ExecuteProcedureDataSet("dbo.TopUserList", param);
            if (ds2 != null)
                userCluster.WeekUsers.AddRange(Enumerable.Select(ds2.Tables[0].AsEnumerable(), item => new TopUserViewModel()
                {
                    Username = item.Field<string>("UserName"),
                    Points = item.Field<int>("Points"),

                }));

            return userCluster;
        }
 
    }
}