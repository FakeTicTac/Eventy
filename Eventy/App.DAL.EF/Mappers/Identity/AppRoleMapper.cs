using AutoMapper;
using Base.DAL.EF.Mappers;

using DomainApp = App.Domain.Identity;
using DalAppDTO = App.DAL.DTO.Identity;


namespace App.DAL.EF.Mappers.Identity;


/// <summary>
/// App Role Mapping Profile Definition:
///  - Basic Implementation + Custom Implementations.
/// </summary>
public class AppRoleMapper : BaseMapper<DalAppDTO.AppRole, DomainApp.AppRole>
{
    
    /// <summary>
    /// Basic App Role Mapper Constructor. Defines Connection To The Mapper.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public AppRoleMapper(IMapper mapper) : base(mapper) { }
    
}