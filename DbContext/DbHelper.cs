using System;
namespace shop.DbContext
{
    public static class DbHelper
    {

        public static string NewID()
        {
            // Create a System.GUID
            byte[] ba = new byte[16];
            Random rd = new Random();
            rd.NextBytes(ba);
            System.Guid guid = new Guid(ba);

            return guid.ToString();
        }

        public static string ConnectionString { get; set; }
    }
}
