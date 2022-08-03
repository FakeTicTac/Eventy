using AutoMapper;
using Base.DAL.EF.Mappers;

using BllAppDto = App.BLL.DTO;
using ApiDtoV1 = Api.DTO.v1.DTO;


namespace Api.DTO.v1.Mappers;


/// <summary>
/// Event Mapping Profile Definition:
///  - Basic Implementation + Custom Implementations.
/// </summary>
public class EventMapper : BaseMapper<ApiDtoV1.Event, BllAppDto.Event>
{
    
    /// <summary>
    /// Basic Event Mapper Constructor. Defines Connection To The Mapper.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public EventMapper(IMapper mapper) : base(mapper) { }
    
}