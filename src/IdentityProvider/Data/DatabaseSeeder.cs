using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace IdentityProvider.Data
{
    public class DatabaseSeeder
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                Seed(serviceScope.ServiceProvider.GetService<IdentityContext>());
            }
        }

        private static void Seed(IdentityContext context)
        {
            context.Database.EnsureCreated();
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();

                IdentityUser admin = new IdentityUser()
                {
                    Id = Guid.NewGuid().ToString("D"),
                    Email = "admin@eventtracker.com",
                    NormalizedEmail = "admin@eventtracker.com".ToUpper(),
                    EmailConfirmed = true,
                    UserName = "admin",
                    NormalizedUserName = "admin".ToUpper(),
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                  
                };

                admin.PasswordHash = passwordHasher.HashPassword(admin, "rakienovreme");

                IdentityRole adminRole = new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString("D"),
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D")
                    
                };

                IdentityRole regularUserRole = new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString("D"),
                    Name = "RegularUser",
                    NormalizedName = "RegularUser".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D")
                };

                IdentityRole eventHolderRole = new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString("D"),
                    Name = "EventHolder",
                    NormalizedName = "EventHolder".ToUpper(),
                    ConcurrencyStamp = Guid.NewGuid().ToString("D")
                };

                IdentityUserRole<string> identityUserRole = new() { RoleId = adminRole.Id, UserId = admin.Id };


                context.Roles.Add(adminRole);
                context.Roles.Add(eventHolderRole);
                context.Roles.Add(regularUserRole);
                
                context.Users.Add(admin);

                context.UserRoles.Add(identityUserRole);

                context.SaveChanges();
            }
        }
    }
}
