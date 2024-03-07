using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace EmployeeService
{
    public class DbHealper
    {
        public readonly string connectionString = ConfigurationManager.AppSettings["mysqlconn"].ToString();

        public IList<T> ExeQuery<T>(DynamicParameters para, String query)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                return con.QueryAsync<T>(query, para, null, 0, commandType: CommandType.Text).Result.ToList<T>();
            }
        }
        public T ExeQuerySingle<T>(DynamicParameters para, String procname)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                return con.QueryAsync<T>(procname, para, null, 0, commandType: CommandType.Text).Result.FirstOrDefault<T>();
            }
        }

        public int ExcecuteNonQuery(DynamicParameters para, String procname)
        {
            using (var con = new MySqlConnection(connectionString))
            {
                return con.Execute(procname, para, null, 0, commandType: CommandType.Text);
            }
        }


        public IList<T> ExeProc<T>(DynamicParameters para, String procname)
        {
            using (var con = new SqlConnection(connectionString))
            {
                return con.QueryAsync<T>(procname, para, null, 0, commandType: CommandType.StoredProcedure).Result.ToList<T>();
            }
        }

        public T ExeProcSingle<T>(DynamicParameters para, String procname)
        {
            using (var con = new SqlConnection(connectionString))
            {
                return con.QueryAsync<T>(procname, para, null, 0, commandType: CommandType.Text).Result.FirstOrDefault<T>();
            }
        }

        public int Excecute(DynamicParameters para, String procname)
        {
            using (var con = new SqlConnection(connectionString))
            {
                return con.Execute(procname, para, null, 0, commandType: CommandType.StoredProcedure);
            }
        }

    }
}