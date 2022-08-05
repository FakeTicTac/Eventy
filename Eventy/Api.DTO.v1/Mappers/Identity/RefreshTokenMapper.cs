using Api.DTO.v1.DTO.Identity;
using AutoMapper;
using Base.BLL.Mappers;
using BllAppDTO = App.BLL.DTO.Identity;


namespace Api.DTO.v1.Mappers.Identity;


/// <summary>
/// Refresh Token Mapping Profile Definition:
///  - Basic Implementation + Custom Implementations.
/// </summary>
public class RefreshTokenMapper : BaseMapper<RefreshToken, BllAppDTO.RefreshToken>
{
    
    /// <summary>
    /// Basic Refresh Token Mapper Constructor. Defines Connection To The Mapper.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public RefreshTokenMapper(IMapper mapper) : base(mapper) { }
    
}