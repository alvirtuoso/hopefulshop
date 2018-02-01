using System;
namespace shop.Models
{
	public class LogClient : BaseEntity
	{
        private string id;
        private string logTypeName;
        private DateTime dateLogged;
        private string note;
        private string userEmail;
        public LogClient(string id, string logTypeName, DateTime dateLogged)
		{
            this.id = id;
            this.logTypeName = logTypeName;
            this.dateLogged = dateLogged;
		}

        public string Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string LogType{
            get { return this.logTypeName;}
            set { this.logTypeName = value; }
        }

        public DateTime DateLogged{
            get { return DateTime.Now; } 
            set { this.dateLogged = value; }
        }

        public string Note{
            get { return this.note; }
            set { this.note = value; }
        }
	    
        public string UserEmail{
            get { return this.userEmail; }
            set { this.userEmail = value; }
        }
    }
}
