using AutoMapper;
using Base.BLL.Mappers;

using DalAppDTO = App.DAL.DTO.Identity;
using BllAppDTO = App.BLL.DTO.Identity;


namespace App.BLL.Mappers.Identity;


/// <summary>
/// App Role Mapping Profile Definition:
///  - Basic Implementation + Custom Implementations.
/// </summary>
public class AppRoleMapper : BaseMapper<BllAppDTO.AppRole, DalAppDTO.AppRole>
{
    
    /// <summary>
    /// Basic App Role Mapper Constructor. Defines Connection To The Mapper.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public AppRoleMapper(IMapper mapper) : base(mapper) { }
    
}