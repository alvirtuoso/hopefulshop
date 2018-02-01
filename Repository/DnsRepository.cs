using System;
using System.Collections.Generic;
using shop.Models;
using ServiceStack.OrmLite.Dapper;
using ServiceStack.OrmLite;
using shop.DbContext;
using shop.Helpers;

namespace shop.Repository
{
    /// <summary>
    /// Dns repository. Uses ServiceStack.ORMLite for CRUD operations and more like create table.
    /// </summary>
    public class DnsRepository:shop.DbContext.ConnectionFactory, IRepository<Dns>
    {
        public DnsRepository()
        {
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				conn.CreateTableIfNotExists<Dns>();
			}
        }
        public Dns Add(Dns item)
        {
            item.Id = DbHelper.NewID();
            using(var conn = GetDapperConnection){
                conn.Open();
                conn.Save<Dns>(item);
            }

            return item;
        }

        /// <summary>
        /// Adds the dns. This will only work when DNS server allows it. Having firebase.google hosting and vultr dns make this method not work
        /// We stick with firebase as host because it's faster than vultr, and has free ssl.
        /// </summary>
        /// <returns><c>true</c>, if dns was added, <c>false</c> otherwise.</returns>
        /// <param name="dns">Dns.</param>
        /// <param name="apiKey">API key.</param>
        public bool AddDns(Dns dns, string apiKey){
            dns.Id = DbHelper.NewID();
            bool result = false;
            using (var conn = GetDapperConnection)
            {
                conn.Open();
                Dns d = conn.Single<Dns>(x => x.OwnerEmail == dns.OwnerEmail);
                // Only save when dns.Name does not already exists.
                if(null == d){  
                    DomainHelper.CreateRecord(apiKey, dns);
					result = conn.Save<Dns>(dns);

                }else{
                    dns.Id = d.Id;
                    result = UpdateDns(dns);
                }
            }
            return result;

        }

		public bool AddDns(Dns dns)
		{
			dns.Id = DbHelper.NewID();
			bool result = false;
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				Dns d = conn.Single<Dns>(x => x.OwnerEmail == dns.OwnerEmail);
				// Only save when dns.Name does not already exists.
				if (null == d)
				{
					result = conn.Save<Dns>(dns);

				}
				else
				{
					dns.Id = d.Id;
					result = UpdateDns(dns);
				}
			}
			return result;

		}

        public IEnumerable<Dns> FindAll()
        {
			using (var conn = GetDapperConnection)
			{
                conn.Open();
                return conn.Select<Dns>();
			}
        }

        public Dns FindByEmail(string email)
		{
			using (var conn = GetDapperConnection)
			{
				conn.Open();
                Dns d = conn.Single<Dns>(x => x.OwnerEmail == email); 
                return d;
			}
		}

        public Dns FindByID(string id)
        {
			using (var conn = GetDapperConnection)
			{
                conn.Open();
                return conn.SingleById<Dns>(id);
			}
        }

        public Dns FindByAmazonId(string id){
            Dns n = null;
            using(var conn = GetDapperConnection){
                conn.Open();
                n = conn.Single<Dns>(x => x.AmazonId == id);
            }
            return n;
        }

		public Dns FindBySubUrlname(string sub)
		{
			Dns n = null;
			using (var conn = GetDapperConnection)
			{
				conn.Open();
                n = conn.Single<Dns>(x => x.Name == sub);
			}
			return n;
		}

		public Dns FindByIDAsync(string id)
		{
			using (var conn = GetDapperConnection)
			{
                conn.Open();
				return conn.SingleByIdAsync<Dns>(id).Result;
			}
		}

        public void Remove(string id)
        {
            using (var conn = GetDapperConnection){
                conn.Open();
                conn.DeleteById<Dns>(id);
            }
        }

		public int RemoveById(string id)
		{
			using (var conn = GetDapperConnection)
			{
				conn.Open();
				return conn.DeleteById<Dns>(id);
			}
		}

		public int RemoveByEmail(string email)
		{
			using (var conn = GetDapperConnection)
			{
                conn.Open();
                return conn.Delete<Dns>(d => d.OwnerEmail == email);
			}
		}

        public void Update(Dns item)
        {
            using (var conn = GetDapperConnection){
				if(item != null && !string.IsNullOrEmpty(item.Id)){
                    conn.Open();
					conn.Update<Dns>(item);
                }
            }
        }

		public bool UpdateDns(Dns item)
		{
            bool result = false;
			using (var conn = GetDapperConnection)
			{
				if (item != null && !string.IsNullOrEmpty(item.Id))
				{
					conn.Open();
					int i = conn.Update<Dns>(item);
                    result = i > 0 ? true : false;
				}
			}
            return result;
		}

    }
}
