using AutoMapper;
using Base.DAL.EF.Repositories;
using App.DAL.EF.Mappers.Identity;
using App.Contracts.DAL.Repositories.Identity;

using DomainApp = App.Domain.Identity;
using DalAppDTO = App.DAL.DTO.Identity;


namespace App.DAL.EF.Repositories.Identity;


/// <summary>
/// Refresh Token Data Access Layer Repository Implementation.
/// </summary>
public class RefreshTokenRepository : 
    BaseEntityRepository<DalAppDTO.RefreshToken, DomainApp.RefreshToken, DomainApp.AppUser, AppDbContext>, 
    IRefreshTokenRepository
{
    
    /// <summary>
    /// Data Access Layer Refresh Token Repository Basic Constructor.
    ///  - Defines Connection To The Database Layer.
    /// </summary>
    /// <param name="dbContext">Database Layer Connection Definition.</param>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public RefreshTokenRepository(AppDbContext dbContext, IMapper mapper) : 
        base(dbContext, new RefreshTokenMapper(mapper)) { }
    
}