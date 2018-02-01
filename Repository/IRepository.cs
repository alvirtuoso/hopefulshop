using System;
using shop.Models;
using System.Collections.Generic;

namespace shop.Repository
{
	public interface IRepository<T> where T : shop.Models.BaseEntity
	{
		T Add(T item);
		void Remove(String id);
		void Update(T item);
		T FindByID(String id);
		IEnumerable<T> FindAll();
	}
}
