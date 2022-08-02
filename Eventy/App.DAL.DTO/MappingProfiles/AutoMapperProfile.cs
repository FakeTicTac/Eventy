using AutoMapper;
using App.DAL.DTO.Identity;

using DomainApp = App.Domain;


namespace App.DAL.DTO.MappingProfiles;


/// <summary>
/// Class Defines AutoMapper Configuration.
///  - Defines Data Access Layer Object Mappings To Entity And Reverse.
/// </summary>
public class AutoMapperProfile : Profile
{
    
    /// <summary>
    /// Basic AutoMapper Configuration Constructor.
    ///  - Configures All Mapping Profiles.
    /// </summary>
    public AutoMapperProfile()
    {
        
        CreateMap<Event, DomainApp.Event>().ReverseMap();
        
        
        // Identity Related Mappings
        
        
        CreateMap<AppUser, DomainApp.Identity.AppUser>().ReverseMap();
        
        CreateMap<AppRole, DomainApp.Identity.AppRole>().ReverseMap();
        
    }
}