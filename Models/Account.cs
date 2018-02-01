using System;
namespace shop.Models
{
    public class Account:BaseEntity
    {
        public Account()
        {
        }

        public string AccountId
        {
            get;
            set;
        }

        public String UserId
        {
            get;
            set;
        }

        public bool Active
        {
            get;
            set;
        }

        public string AccountTypeId{
            get;
            set;
        }
    }
}
