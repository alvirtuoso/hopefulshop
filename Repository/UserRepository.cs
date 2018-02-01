using System;
using shop.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using shop.DbContext;
using ServiceStack.OrmLite;

namespace shop.Repository
{
	public class UserRepository: shop.DbContext.ConnectionFactory, IRepository<User>
	{
		
		//private string connectionString;
		public UserRepository()
		{
			//this.connectionString = Environment.GetEnvironmentVariable("ConnectionString");
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				conn.CreateTableIfNotExists<User>();
			}
		}
	
		/// <summary>
		/// Adds the user.
		/// </summary>
		/// <returns>The user.</returns>
		/// <param name="user">User.</param>
		public User Add(User user)
		{
            user.Id = DbHelper.NewID();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
                if(!IsExisting(user.Email)){
                 dbConnection.Execute(
                    @"INSERT IGNORE INTO shop.User(id, first,last, email, phone, cell, address, city, state, zip, active, associateID) 
                            VALUES(@Id, @First, @Last, @Email, @Phone, @Cell, @Address, @City, @State, @Zip, @Active, @AssociateId)", user);
                }else{
                    Update(user);
                }

            }
            return user;
		}

		/// <summary>
		/// Remove the specified id.
		/// </summary>
		/// <returns>The remove.</returns>
		/// <param name="id">Identifier.</param>
		public void Remove(string id)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				dbConnection.Execute("DELETE FROM shop.User WHERE id=@id", new { id = id });
			}
		
		}

		public bool RemoveById(string id)
		{
			using (var conn = GetDapperConnection)
			{
               var i= conn.Execute("DELETE FROM shop.User WHERE id=@id", new { id = id});
                return i > 0 ? true : false;
			}

		}

		/// <summary>
		/// Update the specified cust.
		/// </summary>
		/// <returns>The update.</returns>
		/// <param name="cust">Cust.</param>
		public void Update(User cust)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
                dbConnection
                    .Execute("UPDATE shop.User SET first = @First,  last = @Last, phone  = @Phone, cell=@Cell, email= @Email, address=@Address, city=@City, zip=@Zip, active=@Active, associateId=@AssociateId WHERE email = @Email", cust);

			}
		}

		/// <summary>
		/// Update the specified cust.
		/// </summary>
		/// <returns>The update.</returns>
		/// <param name="cust">Cust.</param>
        public bool UpdateUser(User cust)
		{
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
				int i = dbConnection
					.Execute("UPDATE shop.User SET first = @First,  last = @Last, phone  = @Phone, cell=@Cell, email= @Email, address=@Address, city=@City, zip=@Zip, active=@Active, associateId=@AssociateId WHERE email = @Email", cust);

                return i > 0 ? true : false;
			}
		}

		/// <summary>
		/// Finds the User by identifier.
		/// </summary>
		/// <returns>The by identifier.</returns>
		/// <param name="id">Identifier.</param>
		public User FindByID(String id)
		{
			var custo = new User();
			using (IDbConnection dbConnection = GetDapperConnection)
			{
				dbConnection.Open();
                custo = dbConnection.QuerySingle<User>("SELECT * FROM shop.User WHERE id = @Id", new { id = id });
			}
			return custo; 	

		}


		public Boolean IsExisting(string email)
		{
			var result = false;
			using (IDbConnection conn = GetDapperConnection)
			{
				result = conn.ExecuteScalar<bool>("select count(1) from shop.User where email=@email", new { email });
			}

			return result;
		}

		public User FindByEmail(string email_param)
		{
            User usr = null;
			try
			{
				using (IDbConnection dbConnection = GetDapperConnection)
				{
					dbConnection.Open();
                    usr = dbConnection.QuerySingle<User>("SELECT * FROM shop.User WHERE email = @email", new { email = email_param });
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

			return usr;

		}

		/// <summary>
		/// Finds all Users.
		/// </summary>
		/// <returns>The all.</returns>
		public IEnumerable<User> FindAll()
		{			

			IEnumerable<User> custList;
			// DapperConnection from ConnectionFactory
			using (var dbCon = GetDapperConnection)
			{
				dbCon.Open();
				custList = dbCon.Query<User>("SELECT * FROM shop.User");
			}
			return custList;
		}


    }
}
