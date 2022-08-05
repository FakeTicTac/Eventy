using AutoMapper;
using Base.BLL.Mappers;

using DalAppDTO = App.DAL.DTO.Identity;
using BllAppDTO = App.BLL.DTO.Identity;


namespace App.BLL.Mappers.Identity;


/// <summary>
/// Refresh Token Mapping Profile Definition:
///  - Basic Implementation + Custom Implementations.
/// </summary>
public class RefreshTokenMapper : BaseMapper<BllAppDTO.RefreshToken, DalAppDTO.RefreshToken>
{
    
    /// <summary>
    /// Basic Refresh Token Mapper Constructor. Defines Connection To The Mapper.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public RefreshTokenMapper(IMapper mapper) : base(mapper) { }
    
}