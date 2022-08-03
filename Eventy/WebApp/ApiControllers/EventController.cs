using AutoMapper;
using Api.DTO.v1.DTO;
using App.Contracts.BLL;
using Api.DTO.v1.Mappers;
using Microsoft.AspNetCore.Mvc;


namespace WebApp.ApiControllers;


/// <summary>
/// API Controller For Event Related Data Procession.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
public class EventController : ControllerBase
{

    /// <summary>
    /// Business Logic Layer Connection Definition.
    /// </summary>
    private readonly IAppBusinessLogic _bll;

    /// <summary>
    /// Drink Mapper Connection Definition.
    /// </summary>
    private readonly EventMapper _mapper;


    /// <summary>
    /// Basic API Constructor.
    /// - Defines Business Logic Layer and Mapper Connection.
    /// </summary>
    /// <param name="bll">Defines Business Logic Layer</param>
    /// <param name="mapper">Mapper Connection Definition.</param>
    public EventController(
        IAppBusinessLogic bll, 
        IMapper mapper
        )
    {
        _bll = bll;
        _mapper = new EventMapper(mapper);
    }

    
    /// <summary>
    /// Method Gets All Possible Events Located In Database.
    /// </summary>
    /// <returns>IEnumerable of All Possible Events Located In Database.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents() => 
        Ok((await _bll.Events.GetAllAsync()).Select(x => _mapper.Map(x)));

    
    
    
    
    
    
    
    
    
    
    
    // GET: api/Event/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(Guid id)
    {
        if (_context.Events == null)
        {
            return NotFound();
        }
        var @event = await _context.Events.FindAsync(id);

        if (@event == null)
        {
            return NotFound();
        }

        return @event;
    }
    
    

    // PUT: api/Event/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEvent(Guid id, Event @event)
    {
        if (id != @event.Id)
        {
            return BadRequest();
        }

        _context.Entry(@event).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EventExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Event
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(Event @event)
    {
        if (_context.Events == null)
        {
            return Problem("Entity set 'AppDbContext.Events'  is null.");
        }
        _context.Events.Add(@event);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
    }

    // DELETE: api/Event/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        if (_context.Events == null)
        {
            return NotFound();
        }
        var @event = await _context.Events.FindAsync(id);
        if (@event == null)
        {
            return NotFound();
        }

        _context.Events.Remove(@event);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EventExists(Guid id)
    {
        return (_context.Events?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}