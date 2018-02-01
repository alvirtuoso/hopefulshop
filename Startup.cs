using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using NLog.Web;
using shop.Models;
using shop.DbContext;
namespace shop
{
    public class Startup
    {
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				// ASP.net: Environment variables are used when present, otherwise appsettings.json variables will be used. Both places must have the same variables
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();

			env.ConfigureNLog("nlog.config");
		}

		public IConfigurationRoot Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			// Add service and create Policy with options
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());                
			});

			services.AddOptions();

			// Register the IConfiguration instance which AppOptions binds against.
			services.Configure<AppOptions>(Configuration);

            shop.DbContext.ConnectionSetting.ConnectionString = Configuration.GetConnectionString("DefaultConnection");

			// Add the MVC feature. Use this instead of AddMvcCore() for a reason specified somewhere but i don't remember where that was. Perhaps goolge the difference..Adrian
			services.AddMvc();
			//services.AddMvcCore(); 
			//.AddJsonFormatters();

			// Use firebase authentication. In controllers, add attribute header [Authorize(Policy = "Firebase")]
			// Uncomment when requiring user's auhentication.
			//services.AddFirebaseAuthorization("my api key");

			//call this in case you need aspnet-user-authtype/aspnet-user-identity
			//services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			//loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			//loggerFactory.AddDebug();

			//add NLog to ASP.NET Core
			loggerFactory.AddNLog();

			//add NLog.Web
			app.AddNLogWeb();

			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
			});

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}

			app.UseCors("CorsPolicy");
			app.UseMvc();
		}
    }
}
