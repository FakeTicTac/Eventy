using Base.Contracts.BLL;
using Base.Contracts.DAL;


namespace Base.BLL;


/// <summary>
/// Implementation of Business Logic Layer Single Transaction That Involves Multiple Operations.
///  - Logic Unit Implementation.
/// </summary>
/// <typeparam name="TUnitOfWork">Data Access Layer Unit Of Work Connection Definition.</typeparam>
public class BaseLogicUnit<TUnitOfWork> : ILogicUnit
    where TUnitOfWork : IUnitOfWork
{
    
    /// <summary>
    /// Data Access Layer Connection Definition. Connection To All Unit Of Work Repositories.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly TUnitOfWork Uow;


    /// <summary>
    /// Service Constructor Defines Connection With Data Access Layer and Especially Unit of Work. 
    /// </summary>
    /// <param name="uow">Data Access Layer Connection Definition.</param>
    // ReSharper disable once MemberCanBeProtected.Global
    public BaseLogicUnit(TUnitOfWork uow) => Uow = uow;
 

    /// <summary>
    /// Method Saves Changes in The Database Asynchronously.
    /// </summary>
    /// <returns>Asynchronous Operation That Returns Number of Objects Updated in The Database.</returns>
    public async Task<int> SaveChangesAsync() => await Uow.SaveChangesAsync();

    
    /// <summary>
    /// Method Saves Changes in The Database Synchronously.
    /// </summary>
    /// <returns>Number of Objects Updated in The Database.</returns>
    public int SaveChanges() => Uow.SaveChanges();
    
    
    /// <summary>
    /// Dictionary Stores Business Logic Layer Services.
    /// </summary>
    private readonly Dictionary<Type, object> _serviceCache = new();
    
    
    /// <summary>
    /// Method Creates Services - Service Factory.
    ///  - Manages Unlimited Instances Creation.
    /// </summary>
    /// <param name="serviceCreationMethod">Defines Method Responsible For Service Creation.</param>
    /// <typeparam name="TService">Defines Type of Service To Be Created.</typeparam>
    /// <returns>Created Service of The Type Presented As "TService" Generic.</returns>
    public TService GetService<TService>(Func<TService> serviceCreationMethod) where TService : class
    {
        // Check if Service Was Already Created.
        if (_serviceCache.TryGetValue(typeof(TService), out var repo)) return (TService) repo;
            
        // Create New Repository And Add It To The Repository Dictionaries.
        var serviceInstance = serviceCreationMethod();
        _serviceCache.Add(typeof(TService), serviceInstance);
            
        return serviceInstance;
    }
}