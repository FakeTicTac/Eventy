using AutoMapper;
using App.BLL.Mappers;
using App.Contracts.DAL;
using Base.BLL.Services;
using App.Contracts.BLL.Services;
using App.Contracts.DAL.Repositories;

using DalAppDTO = App.DAL.DTO;
using BllAppDTO = App.BLL.DTO;


namespace App.BLL.Services;


/// <summary>
/// Event Related Business Logic Layer Service Implementation.
///  - Defines All Methods That Can Be Processed With Service.
/// </summary>
public class EventService : 
    BaseEntityService<BllAppDTO.Event, DalAppDTO.Event, IAppUnitOfWork, IEventRepository>, 
    IEventService
{
    
    /// <summary>
    /// Basis Business Logic Layer Constructor.
    ///  - Defines Connection With Repository, Unit Of Work and Mapper Profile. 
    /// </summary>
    /// <param name="serviceUow">Data Access Layer Unit of Work Connection Definition.</param>
    /// <param name="serviceRepository">Data Access Layer Specific Repository Connection Definition.</param>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public EventService(IAppUnitOfWork serviceUow, IEventRepository serviceRepository, IMapper mapper) 
        : base(serviceUow, serviceRepository, new EventMapper(mapper)) { }

    
    // Asynchronous Operations.


    /// <summary>
    /// Method Gets All Events By Part Of Their Title.
    ///  - Method Mainly Used For Search.
    /// </summary>
    /// <param name="partialName">Defines Part Of Event Name To Be Searched For.</param>
    /// <param name="noTracking">Defines Tracking Options.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Enumerable of Entities.</returns>
    public async Task<IEnumerable<BllAppDTO.Event>> GetAllByPartialNameAsync(
        string? partialName,
        bool noTracking = true
        ) => (await ServiceRepository.GetAllByPartialNameAsync(partialName, noTracking)).Select(x => Mapper.Map(x))!;
    
}