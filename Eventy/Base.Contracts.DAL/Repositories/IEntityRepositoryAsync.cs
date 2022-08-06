using Base.Contracts.Domain;


namespace Base.Contracts.DAL.Repositories;


/// <summary>
/// Data Access Layer Repository Design.
///  - Only Asynchronous Execution Methods.
/// </summary>
/// <typeparam name="TDalEntity">Defines Type Of Data Transfer Object To Be Processed In Data Access Layer.</typeparam>
/// <typeparam name="TKey">Defines Type Of Entity Primary Key Value.</typeparam>
// ReSharper disable once TypeParameterCanBeVariant
public interface IEntityRepositoryAsync<TDalEntity, TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    
    /// <summary>
    /// Method Removes Entity From The Database Asynchronously Using Primary Key As Identifier.
    /// </summary>
    /// <param name="id">Defines Entity Primary Key To Be Removed From The Database.</param>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Removed Entity.</returns>
    Task<TDalEntity?> RemoveAsync(
        TKey id, 
        object? userId = null
        );

    
    /// <summary>
    /// Method Get All Entities of The Given Type From The Database Asynchronously.
    /// </summary>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Enumerable of Entities.</returns>
    Task<IEnumerable<TDalEntity>> GetAllAsync(
        object? userId = null, 
        bool noTracking = true
        );

    
    /// <summary>
    /// Method Get Entity of The Given Type and Primary Key From The Database Asynchronously if Found or Null.
    /// </summary>
    /// <param name="id">Defines Entity Primary Key To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Found Entity or Null.</returns>
    Task<TDalEntity?> FirstOrDefaultAsync(
        TKey id, 
        object? userId = null, 
        bool noTracking = true
        );

    
    /// <summary>
    /// Method Indicates If Entity With Given Primary Key Exist In Database.
    /// </summary>
    /// <param name="id">Defines Entity Primary Key To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Boolean as an Indicator of Entity Existence.</returns>
    Task<bool> ExistAsync(
        TKey id, 
        object? userId = null
        );
    
}
