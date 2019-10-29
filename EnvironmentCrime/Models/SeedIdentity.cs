using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EnvironmentCrime.Models
{
    public class SeedIdentity
    {
        public static async Task CheckDbPopulated(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await CreateRoles(roleManager);
            await CreateUsers(userManager);
        }

        private static async Task CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!await roleManager.RoleExistsAsync("Coordinator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Coordinator"));
            }

            if(!await roleManager.RoleExistsAsync("Manager"))
            {
                await roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            if(!await roleManager.RoleExistsAsync("Investigator"))
            {
                await roleManager.CreateAsync(new IdentityRole("Investigator"));
            }
        }
        
            
        private static async Task CreateUsers(UserManager<IdentityUser> uManager)
        {
            IdentityUser E001 = new IdentityUser("E001");
            IdentityUser E100 = new IdentityUser("E100");
            IdentityUser E101 = new IdentityUser("E101");
            IdentityUser E102 = new IdentityUser("E102");
            IdentityUser E103 = new IdentityUser("E103");
            IdentityUser E200 = new IdentityUser("E200");
            IdentityUser E201 = new IdentityUser("E201");
            IdentityUser E202 = new IdentityUser("E202");
            IdentityUser E203 = new IdentityUser("E203");
            IdentityUser E300 = new IdentityUser("E300");
            IdentityUser E301 = new IdentityUser("E301");
            IdentityUser E302 = new IdentityUser("E302");
            IdentityUser E303 = new IdentityUser("E303"); 
            IdentityUser E400 = new IdentityUser("E400");
            IdentityUser E401 = new IdentityUser("E401");
            IdentityUser E402 = new IdentityUser("E402");
            IdentityUser E403 = new IdentityUser("E403");
            IdentityUser E500 = new IdentityUser("E500");
            IdentityUser E501 = new IdentityUser("E501");
            IdentityUser E502 = new IdentityUser("E502");
            IdentityUser E503 = new IdentityUser("E503");

            await uManager.CreateAsync(E001, "Pass01?");
            await uManager.CreateAsync(E100, "Pass02?");
            await uManager.CreateAsync(E101, "Pass03?");
            await uManager.CreateAsync(E102, "Pass04?");
            await uManager.CreateAsync(E103, "Pass05?");
            await uManager.CreateAsync(E200, "Pass06?");
            await uManager.CreateAsync(E201, "Pass07?");
            await uManager.CreateAsync(E202, "Pass08?");
            await uManager.CreateAsync(E203, "Pass09?");
            await uManager.CreateAsync(E300, "Pass10?");
            await uManager.CreateAsync(E301, "Pass11?");
            await uManager.CreateAsync(E302, "Pass12?");
            await uManager.CreateAsync(E303, "Pass13?");
            await uManager.CreateAsync(E400, "Pass14?");
            await uManager.CreateAsync(E401, "Pass15?");
            await uManager.CreateAsync(E402, "Pass16?");
            await uManager.CreateAsync(E403, "Pass17?");
            await uManager.CreateAsync(E500, "Pass18?");
            await uManager.CreateAsync(E501, "Pass19?");
            await uManager.CreateAsync(E502, "Pass20?");
            await uManager.CreateAsync(E503, "Pass21?");

            await uManager.AddToRoleAsync(E001, "Coordinator");
            await uManager.AddToRoleAsync(E100, "Manager");
            await uManager.AddToRoleAsync(E101, "Investigator");
            await uManager.AddToRoleAsync(E102, "Investigator");
            await uManager.AddToRoleAsync(E103, "Investigator");
            await uManager.AddToRoleAsync(E200, "Manager");
            await uManager.AddToRoleAsync(E201, "Investigator");
            await uManager.AddToRoleAsync(E202, "Investigator");
            await uManager.AddToRoleAsync(E203, "Investigator");
            await uManager.AddToRoleAsync(E300, "Manager");
            await uManager.AddToRoleAsync(E301, "Investigator");
            await uManager.AddToRoleAsync(E302, "Investigator");
            await uManager.AddToRoleAsync(E303, "Investigator");
            await uManager.AddToRoleAsync(E400, "Manager");
            await uManager.AddToRoleAsync(E401, "Investigator");
            await uManager.AddToRoleAsync(E402, "Investigator");
            await uManager.AddToRoleAsync(E403, "Investigator");
            await uManager.AddToRoleAsync(E500, "Manager");
            await uManager.AddToRoleAsync(E501, "Investigator");
            await uManager.AddToRoleAsync(E502, "Investigator");
            await uManager.AddToRoleAsync(E503, "Investigator");

        }
    }
}
