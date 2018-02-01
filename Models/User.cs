using System;
using System.ComponentModel.DataAnnotations;
namespace shop.Models
{
	public class User: BaseEntity
	{
		public User()
		{
		}
        public string First
		{
			get;
			set;
		}

        public string Last
		{
			get;
			set;
		}
		
		public String Id
		{
			get;
			set;
		}
		
		public string Email
		{
			get;
			set;
		}

		public string Address
		{
			get; set;
		}
		public string State
		{
			get;
			set;
		}
		public string City
		{
			get;
			set;
		}
		public string Zip
		{
			get;
			set;
		}

		public string Phone
		{
			get;
			set;
		}

		public string Cell
		{
			get; set;
		}

        public bool  Active{
            get; set;
        }

		public string AssociateId
		{
			get;
			set;
		}
	}
}
