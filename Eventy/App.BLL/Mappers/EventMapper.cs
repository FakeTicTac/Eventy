using AutoMapper;
using Base.BLL.Mappers;

using DalAppDTO = App.DAL.DTO;
using BllAppDTO = App.BLL.DTO;


namespace App.BLL.Mappers;


/// <summary>
/// Event Mapping Profile Definition:
///  - Basic Implementation + Custom Implementations.
/// </summary>
public class EventMapper : BaseMapper<BllAppDTO.Event, DalAppDTO.Event>
{
    
    /// <summary>
    /// Basic Event Mapper Constructor. Defines Connection To The Mapper.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public EventMapper(IMapper mapper) : base(mapper) { }
    
}