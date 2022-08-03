using AutoMapper;

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

    }
}