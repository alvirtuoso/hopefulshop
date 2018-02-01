using System;

namespace shop.Models
{
    public class DomainRecord: BaseEntity
    {
        public DomainRecord()
        {
        }

        public string DomainRecordId
        {
            get;
            set;
        }

        public string DomainName
        {
            get;
            set;
        }

        public string Subdomain
        {
            get;
            set;
        }

    }
}
