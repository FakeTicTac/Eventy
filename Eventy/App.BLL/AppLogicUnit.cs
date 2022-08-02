using Base.BLL;
using AutoMapper;
using App.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.BLL;
using App.Contracts.BLL.Services;


namespace App.BLL;


/// <summary>
/// App Specific Business Logic Design Implementation.
/// - Implements All Services.
/// </summary>
public class AppBusinessLogic : BaseLogicUnit<IAppUnitOfWork>, IAppBusinessLogic
{
    
    /// <summary>
    /// Business Logic Layer Service Implementation For Storing Events.
    /// </summary>
    public IEventService Events =>
        GetService<IEventService>(() => new EventService(Uow, Uow.Events, Mapper));

    
    /// <summary>
    /// Defines Connection to The Mapper Profile.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly IMapper Mapper;


    /// <summary>
    /// Service Constructor Defines Connection With Data Access Layer Meaning Unit of Work. 
    /// </summary>
    /// <param name="uow">Data Access Layer Connection Definition.</param>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public AppBusinessLogic(IAppUnitOfWork uow, IMapper mapper) : base(uow) => Mapper = mapper;

}