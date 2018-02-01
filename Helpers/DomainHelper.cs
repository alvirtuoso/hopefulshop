using System;
using System.IO;
using System.Net;

namespace shop.Helpers
{
    public static class DomainHelper
    {
        public static string UpdateRecord(string key, shop.Models.Dns dns)
        {
            string dnsData = "domain=" + Constants.SHOP_URL + "&name=" + dns.Name + "&type=" + dns.Type + "&data=" + dns.Data;
			string response = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(Constants.VULTR_DNS_UPDATE_RECORD);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = dnsData.Length;
                request.Headers.Add("API-key: " + key);
				using (StreamWriter writer = new StreamWriter(request.GetRequestStreamAsync().Result))
				{
					writer.Write(dnsData);
				}

				using (Stream str = request.GetResponseAsync().Result.GetResponseStream())
				{

					using (StreamReader reader = new StreamReader(str))
					{
						response = reader.ReadToEnd();

					}
				}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }

		public static void CreateRecord(string key, shop.Models.Dns dns)
		{
            dns.Data = Constants.VULTR_IP;
            string dnsData = "domain=" + Constants.SHOP_URL + "&RECORDID=" + dns.RecordId  + "&name=" + dns.Name + "&type=" + dns.Type + "&data=" + dns.Data;

			try
			{
				string response = String.Empty;

				WebRequest request = WebRequest.Create(Constants.VULTR_DNS_CREATE_RECORD);
				request.Method = "POST"; // POST ou GET
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = dnsData.Length;

				request.Headers.Add("API-Key: " + key);


				using (StreamWriter writer = new StreamWriter(request.GetRequestStreamAsync().Result))
				{
					writer.Write(dnsData);
				}

				using (Stream str = request.GetResponseAsync().Result.GetResponseStream())
				{

					using (StreamReader reader = new StreamReader(str))
					{
						response = reader.ReadToEnd();

					}
				}

			}
			catch (Exception e)
			{
				throw e;
			}
		}
    }
}
