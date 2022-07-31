
namespace Base.Contracts.DAL;


/// <summary>
/// Design of Data Access Layer Single Transaction That Involves Multiple Operations.
///  - Unit of Work Design.
/// </summary>
public interface IUnitOfWork
{
    
    /// <summary>
    /// Method Saves Changes in The Database Asynchronously.
    /// </summary>
    /// <returns>Asynchronous Operation That Returns Number of Objects Updated in The Database.</returns>
    Task<int> SaveChangesAsync();

    
    /// <summary>
    /// Method Saves Changes in The Database Synchronously.
    /// </summary>
    /// <returns>Number of Objects Updated in The Database.</returns>
    int SaveChanges();
    
    
    /// <summary>
    /// Method Creates Repositories - Repository Factory.
    ///  - Manages Unlimited Instances Creation.
    /// </summary>
    /// <param name="repoCreationMethod">Defines Method Responsible For Repository Creation.</param>
    /// <typeparam name="TRepository">Defines Type of Repository To Be Created.</typeparam>
    /// <returns>Created Repository of The Type Presented As "TRepository" Generic.</returns>
    TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod) 
        where TRepository : class;
    
}