using Base.BLL;
using AutoMapper;
using App.BLL.Services;
using App.Contracts.DAL;
using App.Contracts.BLL;
using App.BLL.Services.Identity;
using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;


namespace App.BLL;


/// <summary>
/// App Specific Business Logic Unit Implementation.
/// - Implements and Stores All Services.
/// </summary>
public class AppBusinessLogic : 
    BaseLogicUnit<IAppUnitOfWork>,
    IAppBusinessLogic
{
    
    /// <summary>
    /// Business Logic Layer Service Implementation For Storing and Processing Events.
    /// </summary>
    public IEventService Events =>
        GetService<IEventService>(() => new EventService(Uow, Uow.Events, Mapper));

    
    // Identity Related Only
    
    
    /// <summary>
    /// Business Logic Layer Service Implementation For Storing and Processing Refresh Tokens.
    /// </summary>
    public IRefreshTokenService RefreshTokens =>
        GetService<IRefreshTokenService>(() => new RefreshTokenService(Uow, Uow.RefreshTokens, Mapper));
    
    
    /// <summary>
    /// Defines Connection to The Mapper Profile.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly IMapper Mapper;


    /// <summary>
    /// Service Basic Constructor.
    ///  - Defines Connection With Unit of Work. 
    /// </summary>
    /// <param name="uow">Data Access Layer Connection Definition.</param>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public AppBusinessLogic(IAppUnitOfWork uow, IMapper mapper) : base(uow) => Mapper = mapper;

}