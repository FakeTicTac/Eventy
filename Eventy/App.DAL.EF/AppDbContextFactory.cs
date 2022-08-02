using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace App.DAL.EF;


/// <summary>
/// Factory For Creation Derived AppDbContext Instances. 
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    
    /// <summary>
    /// Factory For Creation Derived AppDbContext Instances.
    /// </summary>
    /// <param name="args">Run Parameters.</param>
    /// <returns>New Instance of AppDbContext.</returns>
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
        const string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=postgres;database=Eventy";
        
        optionBuilder.UseNpgsql(connectionString);
        
        return new AppDbContext(optionBuilder.Options);
    }
}