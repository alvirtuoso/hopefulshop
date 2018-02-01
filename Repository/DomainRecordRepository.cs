using System;
using shop.Models;
using Dapper;
using System.Data;
using shop.DbContext;
using System.Collections.Generic;
using ServiceStack.OrmLite;

namespace shop.Repository
{
	/// <summary>
	/// Domain record repository. 
	/// </summary>
	public class DomainRecordRepository:ConnectionFactory, IRepository<DomainRecord>
    {
        public DomainRecordRepository()
        {
			using (var conn = GetDapperConnection)
			{
				conn.Open();
                conn.CreateTableIfNotExists<DomainRecord>();
			}
        }

        public DomainRecord Add(DomainRecord dr)
        {
            dr.DomainRecordId = DbHelper.NewID();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Execute(
				   @"INSERT INTO account(domainRecordId, domainName) 
                            VALUES(@DomainRecordId, @DomainName, @Subdomain)", dr);

			}
			return dr;
        }

        public IEnumerable<DomainRecord> FindAll()
        {
            IEnumerable<DomainRecord> records;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
                records = dbCon.Query<DomainRecord>("SELECT * FROM shop.domainRecord");
			}
			return records;
        }

        public DomainRecord FindByID(string id)
        {
            var rec = new DomainRecord();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
                rec = dbConnection.QuerySingle<DomainRecord>("SELECT * FROM shop.domainRecord WHERE domainRecordId = @DomainRecordId", new { id = id });
			}
			return rec;
        }

        public void Remove(string id)
        {
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Execute("DELETE FROM shop.DomainRecord WHERE domainRecordId=@DomainRecordId", new { id = id });
			}
        }

        public void Update(DomainRecord rec)
        {
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Execute("UPDATE show.DomainRecord SET domainName = @DomainName, subdomain = @Subdomain WHERE domainRecordId = @DomainRecordId", rec);

			}
        }
    }
}
