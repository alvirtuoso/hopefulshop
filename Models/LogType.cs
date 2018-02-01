using System;
namespace shop.Models
{
    public class LogType: BaseEntity
    {
        public LogType()
        {
        }

        public String Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }
    }
}
