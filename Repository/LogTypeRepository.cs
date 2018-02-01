using System;
using shop.Models;
using ServiceStack.OrmLite.Dapper;
using ServiceStack.OrmLite;
using shop.DbContext;
using System.Collections.Generic;

namespace shop.Repository
{
	public class LogTypeRepository : ConnectionFactory, IRepository<LogType>
	{

		public LogTypeRepository()
		{
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				conn.CreateTableIfNotExists<LogType>();
			}
		}

		public LogType Add(LogType item)
		{
			item.Id = DbHelper.NewID();
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				conn.Save(item);
			}
			return item;
		}

		public IEnumerable<LogType> FindAll()
		{
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				return conn.Select<LogType>();
			}
		}

		public LogType FindByID(string id)
		{
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				return conn.SingleById<LogType>(id);
			}
		}

		public void Remove(string id)
		{
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				conn.DeleteById<LogType>(id);
			}
		}

		public void Update(LogType item)
		{
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				if (item != null && !string.IsNullOrEmpty(item.Id))
				{
					conn.Open();
					conn.Update<LogType>(item);
				}
			}
		}
	}
}

