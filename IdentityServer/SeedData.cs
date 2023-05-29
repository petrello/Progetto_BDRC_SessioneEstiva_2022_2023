using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using Duende.IdentityServer.EntityFramework.Storage;
using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IdentityServer
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();

            services.AddLogging();

            services.AddDbContext<AspNetIdentityDbContext>(
                options => options.UseSqlServer(connectionString)
            );

            services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddOperationalDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
                        );
                }
            );
            services.AddConfigurationDbContext(
                options =>
                {
                    options.ConfigureDbContext = db =>
                        db.UseSqlServer(
                            connectionString,
                            sql => sql.MigrationsAssembly(typeof(SeedData).Assembly.FullName)
                        );
                }
            );

            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

            // Seeding Persisted Grant DB Context
            var persistedGrantContext = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
            persistedGrantContext?.Database.Migrate();

            // Seeding Configuration DB Context
            var configurationContext = scope.ServiceProvider.GetService<ConfigurationDbContext>();
            configurationContext?.Database.Migrate();
            SeedConfigurationData(configurationContext!);

            // Seeding Identity DB Context
            var identityContext = scope.ServiceProvider.GetService<AspNetIdentityDbContext>();
            identityContext?.Database.Migrate();
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            SeedIdentityData(userMgr, roleMgr);
        }

        private static void SeedIdentityData(UserManager<IdentityUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            if (roleMgr.FindByNameAsync("Admin").Result == null)
            {
                roleMgr.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                
            }

            if (roleMgr.FindByNameAsync("User").Result == null)
            {
                roleMgr.CreateAsync(new IdentityRole("User")).GetAwaiter().GetResult();

            }

            var tomma = userMgr.FindByNameAsync("Tomma").Result;
            if (tomma == null)
            {
                tomma = new IdentityUser
                {
                    Id = "b2rp0z25-b964-409f-bcce-c923266249b4",
                    UserName = "Tomma",
                    Email = "tommasop01@gmail.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(tomma, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(tomma, new Claim[]{
                    new Claim("name", "Tomma Petre"),
                    new Claim("given_name", "Tomma"),
                    new Claim("family_name", "Petre"),
                    new Claim("role", "Admin"),
                    new Claim("position", "Administrator"),
                    new Claim("country", "ITA")
                    //new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    //new Claim(JwtClaimTypes.GivenName, "Alice"),
                    //new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    //new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                }).Result;

                userMgr.AddToRoleAsync(tomma, "Admin").GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                // Log.Debug("alice created");
            }

            var mick = userMgr.FindByNameAsync("Mick").Result;
            if (mick == null)
            {
                mick = new IdentityUser
                {
                    Id = "a9ea0f25-b964-409f-bcce-c923266249b4",
                    UserName = "Mick",
                    Email = "MickSmith@email.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(mick, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(mick, new Claim[]{
                    new Claim("name", "Mick Smith"),
                    new Claim("given_name", "Mick"),
                    new Claim("family_name", "Smith"),
                    new Claim("role", "Admin"),
                    new Claim("position", "Administrator"),
                    new Claim("country", "USA")
                    //new Claim(JwtClaimTypes.Name, "Alice Smith"),
                    //new Claim(JwtClaimTypes.GivenName, "Alice"),
                    //new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    //new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                }).Result;

                userMgr.AddToRoleAsync(mick, "Admin").GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                // Log.Debug("alice created");
            }
            //else
            //{
            //    // Log.Debug("alice already exists");
            //}

            var bob = userMgr.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new IdentityUser
                {
                    Id = "i5ea0f25-b964-09gt-bcce-c923266241w3",
                    UserName = "bob",
                    Email = "BobMining@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(bob, new Claim[] {
                    new Claim("name", "Bob Mining"),
                    new Claim("given_name", "Bob"),
                    new Claim("family_name", "Mining"),
                    new Claim("role", "User"),
					//new Claim("email", "joe@gmail.com"),
					//new Claim("email_verified", "ok"),
					new Claim("position", "Viewer"),
                    new Claim("country", "USA")
                    //new Claim(JwtClaimTypes.Name, "Bob Smith"),
                    //new Claim(JwtClaimTypes.GivenName, "Bob"),
                    //new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    //new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                    //new Claim("location", "somewhere")
                }).Result;

                userMgr.AddToRoleAsync(bob, "User").GetAwaiter().GetResult();

                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                // Log.Debug("bob created");
            }
            //else
            //{
            //    // Log.Debug("bob already exists");
            //}
        }

        private static void SeedConfigurationData(ConfigurationDbContext configurationContext)
        {
            if (!configurationContext.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    configurationContext.Clients.Add(client.ToEntity());
                }
                configurationContext.SaveChanges();
            }

            if (!configurationContext.IdentityResources.Any())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    configurationContext.IdentityResources.Add(resource.ToEntity());
                }
                configurationContext.SaveChanges();
            }

            if (!configurationContext.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources)
                {
                    configurationContext.ApiResources.Add(resource.ToEntity());
                }
                configurationContext.SaveChanges();
            }

            if (!configurationContext.ApiScopes.Any())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    configurationContext.ApiScopes.Add(resource.ToEntity());
                }
                configurationContext.SaveChanges();
            }
        }
    }
}
