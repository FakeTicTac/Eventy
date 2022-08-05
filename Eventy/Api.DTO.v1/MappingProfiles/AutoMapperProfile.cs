using Api.DTO.v1.DTO.Identity;
using AutoMapper;
using BllAppDto = App.BLL.DTO;
using ApiDtoV1 = Api.DTO.v1.DTO;


namespace Api.DTO.v1.MappingProfiles;


/// <summary>
/// Class Defines AutoMapper Configuration.
///  - Defines API Object Mappings To Business Logic Layer And Reverse.
/// </summary>
public class AutoMapperProfile : Profile
{
        
    /// <summary>
    /// Basic AutoMapper Configuration Constructor.
    ///  - Configures All Mapping Profiles.
    /// </summary>
    public AutoMapperProfile()
    {

        CreateMap<ApiDtoV1.Event, BllAppDto.Event>().ReverseMap();
        
        
        // Identity Related Mappings
        
        
        CreateMap<RefreshToken, BllAppDto.Identity.RefreshToken>().ReverseMap();
        
    }
}