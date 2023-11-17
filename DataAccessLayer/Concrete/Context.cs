﻿using EntityLayer.Concrete;
using EntityLayer.Concrete.Common;
using EntityLayer.Concrete.File;
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
     
		//protected override void OnModelCreating(ModelBuilder modelBuilder)
		//{
			//modelBuilder.Entity<PurchasedCourse>()
			//	.HasOne(pc => pc.Course)
			//	.WithMany(c => c.PurchasedCourses)
			//	.OnDelete(DeleteBehavior.Restrict);

			//modelBuilder.Entity<PurchasedCourse>()
			//	.HasOne(pc => pc.AppUser)
			//	.WithMany(u => u.PurchasedCourses)
			//	.OnDelete(DeleteBehavior.Restrict);

			//base.OnModelCreating(modelBuilder);

			//Yukarıdaki kod, PurchasedCourse tablosundaki Course ve AppUser yabancı anahtarlarına ilişkin silme işlemlerini kısıtlar. Bu sayede, bir Course veya AppUser silindiğinde, ilgili PurchasedCourse’ların otomatik olarak silinmesini engeller. Bu, döngü oluşturma veya birden çok kademeli yol oluşturma sorununu çözer.

			//modelBuilder.Entity<WidgetClickLog>()
			//	.HasOne(pc => pc.Course)
			//	.WithMany(c => c.WidgetClickLogs)
			//	.OnDelete(DeleteBehavior.Restrict);

			//modelBuilder.Entity<WidgetClickLog>()
			//	.HasOne(pc => pc.AppUser)
			//	.WithMany(u => u.WidgetClickLogs)
			//	.OnDelete(DeleteBehavior.Restrict);

			//base.OnModelCreating(modelBuilder);
		//}


		public DbSet<About> Abouts { get; set; }
		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<PurchasedCourse> PurchasedCourses { get; set; }
		public DbSet<WidgetClickLog> WidgetClickLogs { get; set; }

		public DbSet<Instructor> Instructor { get; set; }

        public DbSet<EntityLayer.Concrete.File.File> Files { get; set; }
        public DbSet<CourseImageFile> CourseImageFiles { get; set; }
        public DbSet<CourseVideoFile> CourseVideoFiles { get; set; }
        //public DbSet<CourseCourseVideoFile> CourseCourseVideoFiles { get; set; }



        //Interceptor
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            baseEntity.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                            baseEntity.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                            baseEntity.Status = true;
                            break;
                        case EntityState.Modified:
                            baseEntity.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                            entry.Property("CreatedDate").IsModified = false;
                            break;
                        default:
                            // Bilinmeyen bir durumla karşılaşıldığında yapılacaklar
                            break;
                    }
                }

                if (entry.Entity is AppUser appUser)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            appUser.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                            appUser.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                            appUser.Status = true;
                            break;
                        case EntityState.Modified:
                            appUser.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                            entry.Property("CreatedDate").IsModified = false;
                            break;
                        default:
                            // Bilinmeyen bir durumla karşılaşıldığında yapılacaklar
                            break;
                    }
                }
            }

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }


        //public override int SaveChanges(bool acceptAllChangesOnSuccess)
        //{
        //    var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

        //    foreach (var entry in ChangeTracker.Entries())
        //    {
        //        if (entry.Entity is BaseEntity baseEntity)
        //        {
        //            if (entry.State == EntityState.Added)
        //            {
        //                baseEntity.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        //                baseEntity.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        //                baseEntity.Status = true;
        //            }
        //            else if (entry.State == EntityState.Modified)
        //            {
        //                baseEntity.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        //                entry.Property("CreatedDate").IsModified = false;
        //            }
        //        }

        //        if (entry.Entity is AppUser appUser)
        //        {
        //            // AppUser sınıfındaki benzer işlemleri gerçekleştirin
        //            if (entry.State == EntityState.Added)
        //            {
        //                appUser.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        //                appUser.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        //                appUser.Status = true;
        //            }
        //            else if (entry.State == EntityState.Modified)
        //            {
        //                appUser.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        //                entry.Property("CreatedDate").IsModified = false;
        //            }
        //        }
        //    }

        //    return base.SaveChanges(acceptAllChangesOnSuccess);
        //}




        //Orjinal
        //public override int SaveChanges(bool acceptAllChangesOnSuccess)
        //{
        //    var datas = ChangeTracker.Entries<BaseEntity>();
        //    var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
        //    foreach (var data in datas)
        //    {
        //        switch (data.State)
        //        {
        //            case EntityState.Added:
        //                //data.Entity.CreatedDate = DateTime.UtcNow;
        //                data.Entity.CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        //                data.Entity.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        //                data.Entity.Status = true;
        //                break;
        //            case EntityState.Modified:
        //                data.Entity.UpdatedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
        //                data.Property("CreatedDate").IsModified = false;
        //                break;
        //            default:
        //                // Bilinmeyen bir durumla karşılaşıldığında yapılacaklar
        //                break;
        //        }
        //    }

        //    return base.SaveChanges(acceptAllChangesOnSuccess);
        //}


    }
}
