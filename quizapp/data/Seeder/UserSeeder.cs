using System;
using System.Globalization;
using System.Threading.Tasks;
using data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using models.Auth;
using Newtonsoft.Json;

namespace data.Seeder;

public static class UserSeeder
{


    public static void Seed(
        IServiceProvider service,
        UserManager<models.Auth.User> userManager,
        RoleManager<models.Auth.Role> roleManager,
        string userPath,
        string rolePath
    )
    {
        using var context = new AppDbContext(service.GetRequiredService<DbContextOptions<AppDbContext>>());

        context.Database.EnsureCreated();

        var usersContent = File.ReadAllText(userPath);
        var roleContent = File.ReadAllText(rolePath);

        var users = JsonConvert.DeserializeObject<List<UserJson>>(usersContent);
        var roles = JsonConvert.DeserializeObject<List<Role>>(roleContent);

        var passwordHasher = new PasswordHasher<User>();

        if (users == null || roles == null)
        {
            return;
        }

        foreach (var user in users)
        {
            var newUser = new models.Auth.User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                EmailConfirmed = true,
                DateOfBirth = DateTime.Parse(user.DateOfBirth, CultureInfo.CurrentCulture.DateTimeFormat).ToUniversalTime(),
                IsActive = true,
            };

            var hashedpw = passwordHasher.HashPassword(newUser, user.Password);

            newUser.PasswordHash = hashedpw;

            // add to db
            var userAdded = userManager.CreateAsync(newUser, user.Password).Result;

            if (userAdded.Succeeded)
            {
                var userRole = roleManager.FindByNameAsync(user.Role).Result;

                if (userRole == null)
                {
                    var newRole = roles!.FirstOrDefault(x => x.Name == user.Role);
                    if (newRole == null)
                    {
                        continue;
                    }
                    roleManager.CreateAsync(newRole).Wait();
                }

                var result2 = userManager.AddToRoleAsync(newUser, user.Role).Result;

                if (!result2.Succeeded)
                {
                    continue;
                }
            }

        }

    }

    internal class UserJson
    {
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public required string PhoneNumber { get; set; }

        public required string DateOfBirth { get; set; }

        public required string Role { get; set; }
    }
}