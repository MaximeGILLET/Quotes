using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Quotes.DAL
{
    public class DatabaseDAL
    {
        /// <summary>
        /// Singleton Database instance
        /// </summary>
        public static SqlConnection DbInstance
        {
            get
            {
                if (_DbInstances == null)
                {
                    _DbInstances = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                }
                return _DbInstances;
            }
        }
        private static SqlConnection _DbInstances;

        /// <summary>
        /// Execute procedure with given parameters
        /// </summary>
        /// <param name="procName">The procedure name.</param>
        /// <param name="sqlParams">The sql parameters for the procedure.</param>
        /// <returns></returns>
        public static DataSet ExecuteProcedureDataSet(string procName, List<SqlParameter> sqlParams)
        {
            using (var command = new SqlCommand(procName, DbInstance) { CommandType = CommandType.StoredProcedure })
            {
                try
                {
                    DataSet ds = null;
                    if (sqlParams != null) foreach (var param in sqlParams) command.Parameters.Add(param);
                    DbInstance.Open();
                    using (var dr = command.ExecuteReader())
                    {

                        DataTable dt = new DataTable();
                        dt.Load(dr);
                        ds = new DataSet();
                        ds.Tables.Add(dt);

                    }

                    DbInstance.Close();

                    return ds;

                }
                catch (Exception e)
                {
                    //Log
                }
                finally
                {
                    DbInstance.Close();
                }

                return null;
            }
        }

        public static void ExecuteProcedure(string procName, List<SqlParameter> sqlParams)
        {
            using (var command = new SqlCommand(procName, DbInstance) { CommandType = CommandType.StoredProcedure })
            {
                if (sqlParams != null) foreach (var param in sqlParams) command.Parameters.Add(param);
                DbInstance.Open();
                command.ExecuteNonQuery();
                DbInstance.Close();
                
            }
        }

        public static DataSet ExecuteProcedureDataSet(string procName)
        {
            return ExecuteProcedureDataSet(procName, null);

        }
    }
}
