using System;
namespace shop.Models
{
    public class Dns: BaseEntity
    {
        public Dns()
        {
        }

        public string Id
        {
            get;
            set;
        }

        public String RecordId
        {
            get;
            set;
        }

        public String Domain
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public string Data
        {
            get;
            set;
        }

        public string OwnerEmail{
            get;
            set;
        }

        public string AmazonId
        {
            get; set;
        }
    }
}
