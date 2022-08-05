using App.DAL.DTO.Identity;
using Base.Contracts.DAL.Repositories;


namespace App.Contracts.DAL.Repositories.Identity;


/// <summary>
/// Refresh Token Data Access Layer Repository Design:
///  - Basic and Custom Refresh Token Repository Methods.
/// </summary>
public interface IRefreshTokenRepository : 
    IEntityRepository<RefreshToken>, 
    IRefreshTokenRepositoryCustom<RefreshToken> { }


/// <summary>
/// Refresh Token Data Access Layer Repository Design:
///  - Custom Refresh Token Repository Methods. 
/// </summary>
/// <typeparam name="TEntity">Defines Type Of Entity To Work With.</typeparam>
public interface IRefreshTokenRepositoryCustom<TEntity> { }