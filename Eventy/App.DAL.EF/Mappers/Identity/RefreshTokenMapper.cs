using AutoMapper;
using Base.DAL.EF.Mappers;

using DomainApp = App.Domain.Identity;
using DalAppDTO = App.DAL.DTO.Identity;


namespace App.DAL.EF.Mappers.Identity;


/// <summary>
/// Refresh Token Mapping Profile Definition:
///  - Basic Implementation + Custom Implementations.
/// </summary>
public class RefreshTokenMapper : BaseMapper<DalAppDTO.RefreshToken, DomainApp.RefreshToken>
{
    
    /// <summary>
    /// Basic Refresh Token Mapper Constructor. Defines Connection To The Mapper.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public RefreshTokenMapper(IMapper mapper) : base(mapper) { }
    
}