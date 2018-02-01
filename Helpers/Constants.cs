using System;
namespace shop.Helpers
{
    internal static class Constants
    {
        public const string AMAZON_DEFAULT_ID = "glow0c1-20";
        public const string VULTR_IP = "45.77.92.30";
        public const string VULTR_API_URL = "https://api.vultr.com/v1";
        public const string VULTR_DNS_CREATE_RECORD = "https://api.vultr.com/v1/dns/create_record";
        public const string VULTR_DNS_DELETE_RECORD = "https://api.vultr.com/v1/dns/delete_record";
        public const string VULTR_DNS_UPDATE_RECORD = "https://api.vultr.com/v1/dns/update_record";
        public const string VULTR_DNS_RECORDS = "https://api.vultr.com/v1/dns/recordS";
        public const string SHOP_API_URL = "api.hopeful.shop";
        public const string SHOP_URL = "hopeful.shop";
        public const string FREE_GEOIP_URL = "http://freegeoip.net/json/";

		public const string ACTIVE = "ACTIVE";
		public const string PENDING = "PENDING";
		public const string UNDER_REVIEW = "REVIEW";
		public const string INACTIVE = "INACTIVE";

        // DNS settings are found on sparkpost account(Dashboard->Account->Sending Domains) 
        // And in Vultr Dns.
		public const string TO_EMAIL = "adrianm13@gmail.com";
        public const string FROM_EMAIL = "noreply@mail.hopeful.shop";
	}
}
