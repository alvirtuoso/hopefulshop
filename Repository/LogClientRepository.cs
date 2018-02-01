using System;
using shop.Models;
using ServiceStack.OrmLite.Dapper;
using ServiceStack.OrmLite;
using shop.DbContext;
using System.Collections.Generic;

namespace shop.Repository
{
    public class LogClientRepository: ConnectionFactory, IRepository<LogClient>
    {
        
        public LogClientRepository()
        {
            using(var conn = GetDapperConnection){
                conn.Open();
                conn.CreateTableIfNotExists<LogClient>();
            }
        }

        public LogClient Add(LogClient item)
        {
            item.Id = DbHelper.NewID();
			using (var conn = GetDapperConnection)
			{
				conn.Open();
                conn.Save(item);
			}
            return item;
        }

        public IEnumerable<LogClient> FindAll()
        {
			using (var conn = GetDapperConnection)
			{
				conn.Open();
                return conn.Select<LogClient>();
			}
        }

        public LogClient FindByID(string id)
        {
			using (var conn = GetDapperConnection)
			{
				conn.Open();
                return conn.SingleById<LogClient>(id);
			}
        }

        public void Remove(string id)
        {
			using (var conn = GetDapperConnection)
			{
				conn.Open();
                conn.DeleteById<LogClient>(id);
			}
        }

        public void Update(LogClient item)
        {
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				if (item != null && !string.IsNullOrEmpty(item.Id))
				{
					conn.Open();
                    conn.Update<LogClient>(item);
				}
			}
        }
    }
}
