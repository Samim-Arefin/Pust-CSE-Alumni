using Alumni.Domain;
using Alumni.Domain.Entities;
using Alumni.Domain.Enums;
using Alumni.Services.Security;
using Alumni.Services.Services;
using Alumni.Services.Utilities;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

namespace Alumni.Infrastructure
{
    public static class ServiceRegistry
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddRazorComponents()
                    .AddInteractiveServerComponents();
            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();
            services.AddAuthenticationCore()
                    .AddDbContext<AppDbContext>(options =>
                    {
                        options.UseSqlServer(AppSettings.Settings.DBConnection);
                    })
                    .AddIdentity<User, Role>(options =>
                    {
                        options.Password.RequireDigit = true;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 6;
                        options.Password.RequireNonAlphanumeric = true;
                        options.User.RequireUniqueEmail = true;
                        options.Tokens.PasswordResetTokenProvider = "resetPassword";
                    })
                    .AddDefaultTokenProviders()
                    .AddTokenProvider<ResetPasswordEmailTokenProvider<User>>("resetPassword")
                    .AddEntityFrameworkStores<AppDbContext>();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(12);
            })
            .Configure<ResetPasswordEmailTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(1);
            });

            services.AddScoped<AuthenticationStateProvider, AuthStateProvider>()
                    .AddScoped<IAuthenticationService, AuthenticationService>()
                    .AddScoped<IEmailService, EmailService>()
                    .AddScoped<IDataProtectionService, DataProtectionService>()
                    .AddScoped<IUserService, UserService>()
                    .AddScoped<CurrentUserState>()
                    .AddBlazoredSessionStorage()
                    .AddMudServices()
                    .AddDataProtection();

            return services;
        }

        public static void ApplyDatabaseMigrations(this IServiceCollection services)
        {
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if (dbContext.Database.GetMigrations().Any())
            {
                dbContext.Database.Migrate();
            }
            if (!dbContext.Users.Any())
            {
                var adminRoleId = Guid.NewGuid().ToString();
                var adminUser = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    FullName = "Admin",
                    UserName = AppSettings.Settings.AdminEmail.Split('@')[0],
                    Email = AppSettings.Settings.AdminEmail,
                    NormalizedEmail = AppSettings.Settings.AdminEmail.ToUpper(),
                    NormalizedUserName = AppSettings.Settings.AdminEmail.ToUpper(),
                    EmailConfirmed = true,
                    Status = StatusEnum.Approved,
                };

                var passwordHasher = new PasswordHasher<User>();
                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, AppSettings.Settings.AdminPassword);
                dbContext.Users.Add(adminUser);

                dbContext.Roles.AddRange(
                  new List<Role> {
                        new()
                        {
                            Id = adminRoleId,
                            Name = UserRoleType.Admin.ToString(),
                            NormalizedName = UserRoleType.Admin.ToString().ToUpper(),
                        },
                        new()
                        {
                            Name = UserRoleType.User.ToString(),
                            NormalizedName = UserRoleType.User.ToString().ToUpper()
                        }
                  });

                dbContext.UserRoles.Add(
                   new UserRole
                   {
                       UserId = adminUser.Id,
                       RoleId = adminRoleId
                   });

                dbContext.SaveChanges();
            }
        }
    }
}
