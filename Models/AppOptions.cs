using System;
namespace shop.Models
{
	public class AppOptions
	{
		public AppOptions()
		{
            // Set default value.
            ACCESS_KEY = "";
            SECRET_KEY = "";
            CONNECTION_STRING = "";
		}
		public string ACCESS_KEY { get; set; }
        public string SECRET_KEY { get; set; }
        public string VULTR_API_KEY { get; set; }

		public string SPARKPOST_API_KEY { get; set; }
		public string CONNECTION_STRING { get; set; }
	}

}
