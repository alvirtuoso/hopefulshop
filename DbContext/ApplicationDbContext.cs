using System;
using Microsoft.EntityFrameworkCore;
//using Npgsql.EntityFrameworkCore.PostgreSQL;
namespace shop.DbContext
{
	public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
			
		}

		//public DbSet<Person> People { get; set; }
		//public DbSet<Course> Courses { get; set; }
		//public DbSet<Enrollment> Enrollments { get; set; }
		//public DbSet<Student> Students { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("ConnectionString"));
		}
	}
}
