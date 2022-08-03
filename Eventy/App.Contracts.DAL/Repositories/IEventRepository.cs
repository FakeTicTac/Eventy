using App.DAL.DTO;
using Base.Contracts.DAL.Repositories;


namespace App.Contracts.DAL.Repositories;


/// <summary>
/// Event Data Access Layer Repository Design:
///  - Basic and Custom Event Repository Methods.
/// </summary>
public interface IEventRepository: 
    IEntityRepository<Event>, 
    IEventRepositoryCustom<Event> { }


/// <summary>
/// Event Data Access Layer Repository Design:
///  - Custom Event Repository Methods. 
/// </summary>
/// <typeparam name="TEntity">Defines Type Of Entity To Work With.</typeparam>
public interface IEventRepositoryCustom<TEntity>
{
    
    // Asynchronous Operations.
    
    
    /// <summary>
    /// Method Gets All Events With Partial Name Asynchronously.
    /// </summary>
    /// <param name="partialName">Defines Part Of Event Name.</param>
    /// <param name="noTracking">Defines Tracking Options.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Enumerable of Entities.</returns>
    public Task<IEnumerable<TEntity>> GetAllByPartialNameAsync(string? partialName, bool noTracking = true);
    
}