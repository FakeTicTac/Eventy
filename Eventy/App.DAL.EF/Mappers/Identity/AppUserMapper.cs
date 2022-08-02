using AutoMapper;
using Base.DAL.EF.Mappers;

using DomainApp = App.Domain.Identity;
using DalAppDTO = App.DAL.DTO.Identity;


namespace App.DAL.EF.Mappers.Identity;


/// <summary>
/// App User Mapping Profile Definition:
///  - Basic Implementation + Custom Implementations.
/// </summary>
public class AppUserMapper : BaseMapper<DalAppDTO.AppUser, DomainApp.AppUser>
{
    
    /// <summary>
    /// Basic App User Mapper Constructor. Defines Connection To The Mapper.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public AppUserMapper(IMapper mapper) : base(mapper) { }
    
}