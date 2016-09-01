using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Quotes.DAL
{
    public class DatabaseDAL
    {
        /// <summary>
        /// Singleton Database instance
        /// </summary>
        public static SqlConnection DbInstance { get; } = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

        /// <summary>
        /// Execute procedure with given parameters
        /// </summary>
        /// <param name="storedProcedure">The procedure name.</param>
        /// <param name="storedProcedureParameters">The sql parameters for the procedure.</param>
        /// <returns></returns>
        public static DataSet ExecuteProcedureDataSet(string storedProcedure, List<SqlParameter> storedProcedureParameters)
        {
            var dataSet = new DataSet();

            using (var command = new SqlCommand(storedProcedure, DbInstance) { CommandType = CommandType.StoredProcedure })
            {
                try
                {
                    if (storedProcedureParameters != null)
                    {
                        foreach (var param in storedProcedureParameters)
                        {
                            command.Parameters.Add(param);
                        }
                    }

                    DbInstance.Open();

                    using (var dataReader = command.ExecuteReader())
                    {
                        var dataTable = new DataTable();
                        dataTable.Load(dataReader);
                        
                        dataSet.Tables.Add(dataTable);
                    }
                }
                catch (SqlException e)
                { }
                finally
                {
                    DbInstance.Close();
                }
            }

            return dataSet;
        }

        public static void ExecuteProcedure(string storedProcedure, List<SqlParameter> sqlParameters)
        {
            using (var command = new SqlCommand(storedProcedure, DbInstance) { CommandType = CommandType.StoredProcedure })
            {
                if (sqlParameters != null)
                {
                    foreach (var param in sqlParameters)
                    {
                        command.Parameters.Add(param);
                    }
                }

                DbInstance.Open();
                command.ExecuteNonQuery();
                DbInstance.Close();
            }
        }
    }
}
