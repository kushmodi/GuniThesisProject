using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Thesis.Web.Models;
using Thesis.Web.Models.Enums;

namespace Thesis.Web.Data
{
   

       public static class ApplicationDbContextSeed
        {

            public static async Task SeedIdentityRolesAsync(RoleManager<MyIdentityRole> rolemanager)
            {
                foreach (MyIdentityRoleNames role in Enum.GetValues(typeof(MyIdentityRoleNames)))
                {
                    string rolename = role.ToString();
                    if (!await rolemanager.RoleExistsAsync(rolename))
                    {
                        await rolemanager.CreateAsync(
                            new MyIdentityRole { Name = rolename });
                    }
                }
            }


            public static async Task SeedIdentityUserAsync(UserManager<MyIdentityUser> usermanager)
            {
                MyIdentityUser user;

                user = await usermanager.FindByNameAsync("admin@gunithesis.com");
                if (user == null)
                {
                    user = new MyIdentityUser()
                    {
                        UserName = "admin@gunithesis.com",
                        Email = "admin@gunithesis.com",
                        EmailConfirmed = true,
                        DisplayName = "The Admin User",
                        DateOfBirth = new DateTime(2000, 1, 1),
                        Gender = MyIdentityGenders.Female,
                        IsAdminUser = true
                    };
                    await usermanager.CreateAsync(user, password: "Admin@123");
                    await usermanager.AddToRolesAsync(user, new string[] {
                    MyIdentityRoleNames.Administrator.ToString(),
                    MyIdentityRoleNames.Faculty.ToString()
                });
                }

                user = await usermanager.FindByNameAsync("faculty@gunithesis.com");
                if (user == null)
                {
                    user = new MyIdentityUser()
                    {
                        UserName = "faculty@gunithesis.com",
                        Email = "faculty@gunithesis.com",
                        EmailConfirmed = true,
                        DisplayName = "The Faculty",
                        DateOfBirth = new DateTime(2000, 1, 1),
                        Gender = MyIdentityGenders.Male,
                        IsAdminUser = true
                    };
                    await usermanager.CreateAsync(user, password: "Faculty@123");
                    await usermanager.AddToRolesAsync(user, new string[] {
                    MyIdentityRoleNames.Faculty.ToString()
                });
                }

            }

        }
    }

