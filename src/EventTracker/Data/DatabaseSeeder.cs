using EventTracker.DAL.Data;
using EventTracker.DAL.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System;
using EventTracker.DAL.Enums;

namespace EventTracker.Data
{
    public class DatabaseSeeder
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Seed(serviceScope.ServiceProvider.GetService<DatabaseContext>());
            }
        }

        private static void Seed(DatabaseContext context)
        {
            context.Database.EnsureCreated();
            //context.Database.Migrate();

            if (!context.Events.Any())
            {
                context.AddRange(new List<Event>
                {                   
                    new Event
                    {
                        Name = "Test",
                        Description = "Testing",
                        Category = Category.IT,
                        Location = "Remote",
                        CreatedAt = DateTime.Now,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        LastModifiedAt = DateTime.Now
                    },new Event
                    {
                        Name = "Testing",
                        Description = "Notifications",
                        Category = Category.IT,
                        Location = "Remote",
                        CreatedAt = DateTime.Now,
                        StartDate = DateTime.Now,
                        EndDate = DateTime.Now,
                        LastModifiedAt = DateTime.Now
                    },
                });
            }

            context.SaveChanges();
        }
    }
}