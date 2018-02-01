using System;
using shop.Models;
using System.Collections.Generic;
using System.Data;
using Dapper;
using shop.DbContext;

namespace shop.Repository
{
    public class TestRepository: ConnectionFactory, IRepository<Test>
    {
        
        //private string connectionString;
        public TestRepository()
        {
            //this.connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        }
    
        /// <summary>
        /// Adds the user.
        /// </summary>
        /// <returns>The user.</returns>
        /// <param name="test">User.</param>
        public Test Add(Test t)
        {
            try
            {
                // Prepare GUID values in SQL format
                string guidForChar36 = DbHelper.NewID();
                //Test e = new Test { First = "Dino", Last = "Sor", Id = guidForChar36 };
                using (IDbConnection dbConnection = GetDapperConnection) 
                {
                    dbConnection.Open();

					dbConnection.Execute(
					@"INSERT INTO test(first,last,id) VALUES(@First, @Last, @Id)", t);
                }
            }
            catch (Exception ex)
            {
                throw ex;
			}

            return t;
        }


        public IEnumerable<Test> FindAll()
        {
            throw new NotImplementedException();
        }

        public Test FindByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Remove(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Test item)
        {
            throw new NotImplementedException();
        }
    }
}
