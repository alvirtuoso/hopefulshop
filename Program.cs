﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
			//BuildWebHost(args).Run();
			var host = new WebHostBuilder()
                    	.UseKestrel()
                    	.UseUrls("http://127.0.0.1:5000")
                    	.UseContentRoot(Directory.GetCurrentDirectory())
                    	.UseIISIntegration()
                    	.UseStartup<Startup>()
                    	.Build();

			host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
