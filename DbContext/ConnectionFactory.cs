using System;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
//using Dapper;
using ServiceStack.OrmLite.Dapper;
using ServiceStack.OrmLite;
using Microsoft.Extensions.Options;
using shop.Models;
using shop.DbContext;
namespace shop.DbContext
{
	public class ConnectionFactory
	{
        public IDbConnection GetDapperConnection => new OrmLiteConnectionFactory(
            ConnectionSetting.ConnectionString
			, MySqlDialect.Provider).CreateDbConnection(); //new MySqlConnection(Environment.GetEnvironmentVariable("CONNECTION_STRING"));

        public IDbConnection DefaultConnection(){
            var con = ConnectionSetting.ConnectionString;
            var dbFactory = new OrmLiteConnectionFactory(con, MySqlDialect.Provider);
            return dbFactory.CreateDbConnection();

        }

        public bool ExecuteProc(string sql, List<SqlParameter> paramList = null)
		{

			try
			{
				using (IDbConnection conn = GetDapperConnection)
				{
					DynamicParameters dp = new DynamicParameters();
					if (paramList != null)
						foreach (SqlParameter sp in paramList)
							dp.Add(sp.ParameterName, sp.SqlValue, sp.DbType); 
					conn.Open();
					return conn.Execute(sql, dp, commandType: CommandType.StoredProcedure) > 0;
				}
			}
			catch (Exception)
			{
				//do logging
				return false;
			}
		}
	}
}
