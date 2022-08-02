using AutoMapper;
using Base.BLL.Mappers;

using DalAppDTO = App.DAL.DTO.Identity;
using BllAppDTO = App.BLL.DTO.Identity;


namespace App.BLL.Mappers.Identity;


/// <summary>
/// App User Mapping Profile Definition:
///  - Basic Implementation + Custom Implementations.
/// </summary>
public class AppUserMapper : BaseMapper<BllAppDTO.AppUser, DalAppDTO.AppUser>
{
    
    /// <summary>
    /// Basic App User Mapper Constructor. Defines Connection To The Mapper.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public AppUserMapper(IMapper mapper) : base(mapper) { }
    
}