using Base.Contracts.Domain;


namespace Base.Contracts.DAL.Repositories;


/// <summary>
/// Data Access Layer Repository Design.
///  - Only Synchronous Execution Methods.
/// </summary>
/// <typeparam name="TDalEntity">Defines Type Of Data Transfer Object To Be Processed In Data Access Layer.</typeparam>
/// <typeparam name="TKey">Defines Type Of Entity Primary Key Value.</typeparam>
public interface IEntityRepositorySync<TDalEntity, in TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    
    /// <summary>
    /// Method Adds Entity To The Database Synchronously.
    /// </summary>
    /// <param name="entity">Defines Entity To Be Added To The Database.</param>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <returns>The Value of Added Entity.</returns>
    TDalEntity Add(
        TDalEntity entity, 
        object? userId = null
        );

    
    /// <summary>
    /// Method Updates Entity in The Database Synchronously.
    /// </summary>
    /// <param name="entity">Defines Entity To Be Updated in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <returns>The Value of Updated Entity.</returns>
    TDalEntity Update(
        TDalEntity entity, 
        object? userId = null
        );

    
    /// <summary>
    /// Method Removes Entity From The Database Synchronously Using Primary Key.
    /// </summary>
    /// <param name="id">Defines Entity Primary Be Removed From The Database.</param>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <returns>The Value of Removed Entity.</returns>
    TDalEntity Remove(
        TKey id, 
        object? userId = null
        );

    
    /// <summary>
    /// Method Removes Entity From The Database Synchronously Using Generated Entity.
    /// </summary>
    /// <param name="entity">Defines Entity To Be Removed From The Database.</param>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <returns>The Value of Removed Entity.</returns>
    TDalEntity Remove(
        TDalEntity entity, 
        object? userId = null
        );

    
    /// <summary>
    /// Method Get All Entities of The Given Type From The Database Synchronously.
    /// </summary>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>The Value of Enumerable of Entities.</returns>
    IEnumerable<TDalEntity> GetAll(
        object? userId = null,
        bool noTracking = true
        );

    
    /// <summary>
    /// Method Get Entity of The Given Type Primary Key The Database Synchronously if Found or Null.
    /// </summary>
    /// <param name="id">Defines Entity Primary Key To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>The Value of Found Entity or Null.</returns>
    TDalEntity? FirstOrDefault(
        TKey id, 
        object? userId = null, 
        bool noTracking = true
        );

    
    /// <summary>
    /// Method Indicates If Entity With Given Primary Key Exist In Database.
    /// </summary>
    /// <param name="id">Defines Entity Primary Key To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User Foreign Key Value.</param>
    /// <returns>The Value of Boolean as an Indicator of Entity Existence.</returns>
    bool Exist(
        TKey id, 
        object? userId = null
        );
    
}
