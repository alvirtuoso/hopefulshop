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
	/// Account type repository.Uses Dapper, and no ORM implemented.
	/// </summary>
	public class AccountTypeRepository:ConnectionFactory, IRepository<AccountType>
    {
        public AccountTypeRepository()
        {
			using (var conn = GetDapperConnection)
			{
				conn.Open();
                conn.CreateTableIfNotExists<AccountType>();
			}
        }

        public AccountType Add(AccountType aType)
        {
            aType.AccountTypeId = DbHelper.NewID();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
                dbConnection.Open();
				dbConnection.Execute(
				   @"INSERT INTO account(accountTypeId, name) 
                            VALUES(@AccountTypeId, @Name)", aType);

			}
            return aType;
        }



        public IEnumerable<AccountType> FindAll()
        {
            IEnumerable<AccountType> accountTypes;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
				accountTypes = dbCon.Query<AccountType>("SELECT * FROM shop.accountType");
			}
			return accountTypes;
        }

        public AccountType FindByID(string id)
        {
            var accountType = new AccountType();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				accountType = dbConnection.QuerySingle<AccountType>("SELECT * FROM shop.accountType WHERE accountTypeId = @AccountTypeId", new { id = id });
			}
			return accountType;
        }

        public void Remove(string id)
        {
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Execute("DELETE FROM shop.accountType WHERE accountTypeId=@AccountTypeId", new { id = id });
			}
        }

        public void Update(AccountType aType)
        {
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
                dbConnection.Execute("UPDATE show.accountType SET name = @Name WHERE accountTypeId = @AcccountTypeId", aType);

			}
        }
    }
}
