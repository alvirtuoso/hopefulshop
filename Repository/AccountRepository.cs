using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using shop.DbContext;
using shop.Models;
using ServiceStack.OrmLite;

namespace shop.Repository
{
    /// <summary>
    /// Account repository. Uses plain Dapper for database access. No ORM implemented.
    /// </summary>
    public class AccountRepository:ConnectionFactory, IRepository<Account>
    {
        public AccountRepository()
        {
			using (var conn = GetDapperConnection)
			{
				conn.Open();
                conn.CreateTableIfNotExists<Account>();
			}
        }
        public Account Add(Account account)
        {
            account.AccountId = DbHelper.NewID();
            using (IDbConnection dbConnection = GetDapperConnection)
			{
                dbConnection.Open();
				var i = dbConnection.Execute(
				   @"INSERT INTO account(accountId, accountTypeId, active, userId) 
                            VALUES(@AccountId, @AccountTypeId, @Active, @UserId)", account);
                if (i == 0) { 
                    account = null; 
                }
			}
			return account;
        }

        public IEnumerable<Account> FindAll()
        {
            IEnumerable<Account> accounts;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
                accounts = dbCon.Query<Account>("SELECT * FROM shop.Account");
			}
			return accounts;
        }

        public Account FindByID(string id)
        {
            Account account = null;
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
                account = dbConnection.QuerySingle<Account>("SELECT * FROM shop.Account WHERE accountId = @Id", new { id = id });
			}
			return account;
		}

        public void Remove(string id)
        {
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Execute("DELETE FROM shop.Account WHERE accountId=@id", new { id = id });
			}

        }

		public bool RemoveById(string id)
		{
			int i = 0;
			bool r = false;
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				i = dbConnection.Execute("DELETE FROM shop.Account WHERE accountId=@id", new { id = id });
			}
			if (i != 0)
				r = true;
			else
				r = false;

            return r;
		}

        public void Update(Account account)
        {
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection
					.Execute("UPDATE show.Account SET accountTypeId = @AccountTypeId,  userId = @UserId, active = @Active WHERE accountId = @AcccountId", account);

			}
        }

		public bool UpdateAccount(Account account)
		{
            int i = 0;
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				i = dbConnection
					.Execute("UPDATE shop.Account SET accountTypeId = @AccountTypeId,  userId = @UserId, active = @Active WHERE accountId = @AccountId", account);

			}
            bool result = false;
            if (i > 0)
                result = true;

            return result;
		}
    }
}
