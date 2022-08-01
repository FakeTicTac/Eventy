using Base.Contracts.DAL;
using Microsoft.EntityFrameworkCore;


namespace Base.DAL.EF;


/// <summary>
/// Implementation of Data Access Layer Single Transaction That Involves Multiple Operations.
///  - Unit of Work Implementation.
/// </summary>
/// <typeparam name="TDbContext">Database Layer Connection Definition.</typeparam>
public abstract class BaseUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{

    /// <summary>
    /// Database Layer Connection Definition.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly TDbContext UowDbContext;
    
    
    /// <summary>
    /// Unit of Work Constructor Defines Connection With DataBase Layer. 
    /// </summary>
    /// <param name="uowDbContext">Database Layer Connection Definition.</param>
    // ReSharper disable once PublicConstructorInAbstractClass
    public BaseUnitOfWork(TDbContext uowDbContext) => UowDbContext = uowDbContext;


    /// <summary>
    /// Method Saves Changes in The Database Synchronously.
    /// </summary>
    /// <returns>Number of Objects Updated in The Database.</returns>
    public int SaveChanges() => UowDbContext.SaveChanges();

    
    /// <summary>
    /// Method Saves Changes in The Database Asynchronously.
    /// </summary>
    /// <returns>Asynchronous Operation That Returns Number of Objects Updated in The Database.</returns>
    public Task<int> SaveChangesAsync() => UowDbContext.SaveChangesAsync();
    
    
    /// <summary>
    /// Dictionary Stores Data Access Layer Repositories.
    /// </summary>
    private readonly Dictionary<Type, object> _repoCache = new();
    
    
    /// <summary>
    /// Method Creates Repositories - Repository Factory.
    ///  - Manages Unlimited Instances Creation.
    /// </summary>
    /// <param name="repoCreationMethod">Defines Method Responsible For Repository Creation.</param>
    /// <typeparam name="TRepository">Defines Type of Repository To Be Created.</typeparam>
    /// <returns>Created Repository of The Type Presented As "TRepository" Generic.</returns>
    public TRepository GetRepository<TRepository>(Func<TRepository> repoCreationMethod) where TRepository: class
    {
        // Check if Repository Was Already Created.
        if (_repoCache.TryGetValue(typeof(TRepository), out var repo)) return (TRepository) repo;
        
        // Create New Repository And Add It To The Repository Dictionaries.
        var repoInstance = repoCreationMethod();
        _repoCache.Add(typeof(TRepository), repoInstance);
        
        return repoInstance;
    }
}