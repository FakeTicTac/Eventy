using AutoMapper;
using App.Contracts.DAL;
using Base.BLL.Services;
using App.BLL.Mappers.Identity;
using App.Contracts.BLL.Services.Identity;
using App.Contracts.DAL.Repositories.Identity;


using DalAppDTO = App.DAL.DTO.Identity;
using BllAppDTO = App.BLL.DTO.Identity;


namespace App.BLL.Services.Identity;


/// <summary>
/// Refresh Token Related Business Logic Layer Service Implementation.
///  - Defines All Methods That Can Be Processed With Service.
/// </summary>
public class RefreshTokenService : BaseEntityService<BllAppDTO.RefreshToken, DalAppDTO.RefreshToken, IAppUnitOfWork, IRefreshTokenRepository>, 
    IRefreshTokenService
{
    
    /// <summary>
    /// Basis Business Logic Layer Constructor.
    ///  - Defines Connection With Repository, Unit Of Work and Mapper Profile. 
    /// </summary>
    /// <param name="serviceUow">Data Access Layer Unit of Work Connection Definition.</param>
    /// <param name="serviceRepository">Data Access Layer Specific Repository Connection Definition.</param>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public RefreshTokenService(IAppUnitOfWork serviceUow, IRefreshTokenRepository serviceRepository, IMapper mapper) 
        : base(serviceUow, serviceRepository, new RefreshTokenMapper(mapper)) { }
    
}