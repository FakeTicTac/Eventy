using App.DAL.EF;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace WebApp.Helpers;


/// <summary>
/// Application Helpers Class Implementation. (Data Seeding, Identity Seeding and etc.)
/// </summary>
public static class AppDataHelper
{

    /// <summary>
    /// Method Initializes Database With Data.
    /// </summary>
    public static void SetupAppData(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
    {
        using var serviceScore = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

        using var dbContext = serviceScore.ServiceProvider.GetService<AppDbContext>();
        
        // Check If Database Is Found In Services.
        if (dbContext == null) throw new ApplicationException("Problem in Services: No Database Context");
        
        
        // Delete Database If Configuration Is Set To True.
        if (configuration.GetValue<bool>("DataInitialization:DropDatabase")) dbContext.Database.EnsureDeleted();
        
        // Apply Migrations To The Database If Configuration Is Set To True.
        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase")) dbContext.Database.Migrate();
        
        // Seed Database With Identities If Configuration Is Set To True.
        if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            // Accessing User and Role Managers.
            using var userManager = serviceScore.ServiceProvider.GetService<UserManager<AppUser>>();
            using var roleManager = serviceScore.ServiceProvider.GetService<RoleManager<AppRole>>();
            
            // Check If Managers Exist.
            if (userManager == null || roleManager == null)
                throw new NullReferenceException("User Manager and Role Manager Cannot be Null.");
            
            
            // Create Roles For Seeding.
            RoleSeeding(roleManager);
            
            // Create Users For Seeding.
            UserSeeding(userManager);
            
        }
        
        // Seed Database With Data If Configuration Is Set To True.
        if (configuration.GetValue<bool>("DataInitialization:SeedData")) {}
        
    }
    
    /// <summary>
    /// Method Creates and Seeds Roles Into Database.
    /// </summary>
    /// <param name="roleManager">Defines Connection To User Role Manager.</param>
    private static void RoleSeeding(RoleManager<AppRole> roleManager) 
    {
        var roleNames = new[] { "Admin", "User" };
            
        foreach (var roleName in roleNames)
        {
            // Check if Role Already Exist in the Database.
            if (roleManager.FindByNameAsync(roleName).Result != null) continue;
                
            var identityResult = roleManager.CreateAsync(new AppRole {Name = roleName}).Result;
                    
            // Check If Identity Role Seeding Succeeded.
            if (!identityResult.Succeeded) throw new ApplicationException($"{roleName} role creation failed.");
            
        }
    }

    /// <summary>
    /// Method Creates and Seeds Users Into Database With Initial Data.
    /// </summary>
    /// <param name="userManager">Defines Connection To User Manager.</param>
    private static void UserSeeding(UserManager<AppUser> userManager)
    {
        
        // Initialize Users Data For User Creation.
        var users = new (string userName, string password, string roles)[]
        {
            ("romake@ttu.ee", "123456QWE", "Admin,User"),
            ("semovs@ttu.ee", "123456QWE", "User"),
            ("darbel@ttu.ee", "123456QWE", "User")
        };
        
        // Create Users From Given Data and Seed Them Into Database.
        foreach (var (userName, password, roles) in users)
        {
            // Check if User Already Exist in the Database.
            if (userManager.FindByEmailAsync(userName).Result != null) continue;
                
            var user = new AppUser { Email = userName, UserName = userName, EmailConfirmed = true};
                
            var identityResult = userManager.CreateAsync(user, password).Result;
                
            // Check If Identity User Seeding Succeeded.
            if (!identityResult.Succeeded) throw new ApplicationException($"{userName} user creation failed.");

            // Check If Role Defined.
            if (!string.IsNullOrWhiteSpace(roles))
            {
                // ReSharper disable once UnusedVariable
                var result = userManager.AddToRolesAsync(user, roles.Split(",").Select(x => x.Trim())).Result;
            }
        }
    }
}