using AutoMapper;
using Base.DAL.EF;
using App.Contracts.DAL;
using App.DAL.EF.Repositories;
using App.Contracts.DAL.Repositories;
using App.DAL.EF.Repositories.Identity;
using App.Contracts.DAL.Repositories.Identity;


namespace App.DAL.EF;


/// <summary>
/// App Specific Unit of Work Design Implementation.
///  - Implements All Repositories.
/// </summary>
public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
{
    
    /// <summary>
    /// Data Access Layer Event Repository Implementation.
    /// </summary>
    public IEventRepository Events =>
            GetRepository(() => new EventRepository(UowDbContext, Mapper));

    
    // Identity Related Only.
    
    
    /// <summary>
    /// Data Access Layer Refresh Token Repository Implementation.
    /// </summary>
    public IRefreshTokenRepository RefreshTokens =>
        GetRepository(() => new RefreshTokenRepository(UowDbContext, Mapper));
    

    /// <summary>
    /// Defines Connection to The Mapper Profile.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly IMapper Mapper;


    /// <summary>
    /// App Specific Unit of Work Constructor.
    /// - Defines Connection to The Database Layer.
    /// </summary>
    /// <param name="uowDbContext">Defines Connection to The Database Layer.</param>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext) => Mapper = mapper;

}