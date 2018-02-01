using System;
namespace shop.Models
{
    public class AccountType: BaseEntity
    {
        public AccountType()
        {
        }
        public string Name
        {
            get;
            set;
        }
        public string AccountTypeId
        {
            get;
            set;
        }
    }
}
