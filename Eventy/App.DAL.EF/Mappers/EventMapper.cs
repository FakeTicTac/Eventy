using AutoMapper;
using Base.DAL.EF.Mappers;

using DomainApp = App.Domain;
using DalAppDTO = App.DAL.DTO;


namespace App.DAL.EF.Mappers;


/// <summary>
/// Event Mapping Profile Definition:
///  - Basic Implementation + Custom Implementations.
/// </summary>
public class EventMapper : BaseMapper<DalAppDTO.Event, DomainApp.Event>
{
    
    /// <summary>
    /// Basic Event Mapper Constructor. Defines Connection To The Mapper.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public EventMapper(IMapper mapper) : base(mapper) { }
    
}