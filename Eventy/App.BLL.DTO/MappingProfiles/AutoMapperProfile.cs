using AutoMapper;
using App.BLL.DTO.Identity;

using DalAppDTO = App.DAL.DTO;


namespace App.BLL.DTO.MappingProfiles;


/// <summary>
/// Class Defines AutoMapper Configuration.
///  - Defines Business Logic Layer Object Mappings To Data Access Layer And Reverse.
/// </summary>
public class AutoMapperProfile : Profile
{
        
    /// <summary>
    /// Basic AutoMapper Configuration Constructor.
    ///  - Configures All Mapping Profiles.
    /// </summary>
    public AutoMapperProfile()
    {

        CreateMap<Event, DalAppDTO.Event>().ReverseMap();
        
        
        // Identity Related Mappings
        
        
        CreateMap<AppUser, DalAppDTO.Identity.AppUser>().ReverseMap();
        
        CreateMap<AppRole, DalAppDTO.Identity.AppRole>().ReverseMap();
        
    }
}