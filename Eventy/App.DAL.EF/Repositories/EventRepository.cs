using AutoMapper;
using App.DAL.EF.Mappers;
using App.Domain.Identity;
using Base.DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using App.Contracts.DAL.Repositories;

using DomainApp = App.Domain;
using DalAppDTO = App.DAL.DTO;


namespace App.DAL.EF.Repositories;


/// <summary>
/// Event Data Access Layer Repository Implementation.
/// </summary>
public class EventRepository : 
    BaseEntityRepository<DalAppDTO.Event, DomainApp.Event, AppUser, AppDbContext>, 
    IEventRepository
{
    
    /// <summary>
    /// Data Access Layer Event Repository Basic Constructor.
    ///  - Defines Connection To The Database Layer.
    /// </summary>
    /// <param name="dbContext">Database Layer Connection Definition.</param>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public EventRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new EventMapper(mapper)) { }

    
    // Asynchronous Operations.


    /// <summary>
    /// Method Gets All Events With Partial Name Asynchronously.
    /// </summary>
    /// <param name="partialName">Defines Part Of Event Name.</param>
    /// <param name="noTracking">Defines Tracking Options.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Enumerable of Entities.</returns>
    public async Task<IEnumerable<DalAppDTO.Event>> GetAllByPartialNameAsync(string? partialName, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);

        return (await query.Where(x => x.Title.ToString().ToUpper().Contains(partialName!.ToUpper()))
                .ToListAsync())
            .Select(x => Mapper.Map(x))!;
    }
}