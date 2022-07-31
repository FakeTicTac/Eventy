namespace Base.Contracts.BLL;


/// <summary>
/// Design of Business Logic Layer Single Transaction That Involves Multiple Operations.
///  - Logic Unit Design.
/// </summary>
public interface ILogicUnit
{
    
    /// <summary>
    /// Method Saves Changes Made In Service Layer Next Through The Pipeline Asynchronously.
    /// </summary>
    /// <returns>Asynchronous Operation That Returns Number of Objects Updated in The Database.</returns>
    Task<int> SaveChangesAsync();
    
    
    /// <summary>
    /// Method Saves Changes Made In Service Layer Next Through The Pipeline Synchronously.
    /// </summary>
    /// <returns>Number of Objects Updated in The Database.</returns>
    int SaveChanges();
    
    
    /// <summary>
    /// Method Creates Services - Service Factory.
    ///  - Manages Unlimited Instances Creation.
    /// </summary>
    /// <param name="serviceCreationMethod">Defines Method Responsible For Service Creation.</param>
    /// <typeparam name="TService">Defines Type of Service To Be Created.</typeparam>
    /// <returns>Created Service of The Type Presented As "TService" Generic.</returns>
    TService GetService<TService>(Func<TService> serviceCreationMethod) where TService : class;

}