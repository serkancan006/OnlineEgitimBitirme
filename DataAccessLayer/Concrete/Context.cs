using EntityLayer.Concrete;
using EntityLayer.Concrete.identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
	public class Context : IdentityDbContext<AppUser, AppRole, int>
	{
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=.;Database=Online-Egitim-DB;Integrated Security=True;TrustServerCertificate=True;");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PurchasedCourse>()
				.HasOne(pc => pc.Course)
				.WithMany(c => c.PurchasedCourses)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<PurchasedCourse>()
				.HasOne(pc => pc.AppUser)
				.WithMany(u => u.PurchasedCourses)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(modelBuilder);
			//Yukarıdaki kod, PurchasedCourse tablosundaki Course ve AppUser yabancı anahtarlarına ilişkin silme işlemlerini kısıtlar. Bu sayede, bir Course veya AppUser silindiğinde, ilgili PurchasedCourse’ların otomatik olarak silinmesini engeller. Bu, döngü oluşturma veya birden çok kademeli yol oluşturma sorununu çözer.
			modelBuilder.Entity<WidgetClickLog>()
				.HasOne(pc => pc.Course)
				.WithMany(c => c.WidgetClickLogs)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<WidgetClickLog>()
				.HasOne(pc => pc.AppUser)
				.WithMany(u => u.WidgetClickLogs)
				.OnDelete(DeleteBehavior.Restrict);

			base.OnModelCreating(modelBuilder);
		}

		public DbSet<About> Abouts { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<PurchasedCourse> PurchasedCourses { get; set; }
		public DbSet<WidgetClickLog> WidgetClickLogs { get; set; }
		public DbSet<Instructor> Instructor { get; set; }

		
	}
}
