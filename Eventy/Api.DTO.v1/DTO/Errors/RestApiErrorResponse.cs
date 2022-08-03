using System.Net;


// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global


namespace Api.DTO.v1.DTO.Errors;


/// <summary>
/// Application API Error Response Data Transfer Object.
///  - Defines Data Sent To User In Case Of Error. 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global 
public class RestApiErrorResponse
{
    
    /// <summary>
    /// Type Of The Error.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Name of The Error in Human Readable Format. Basically Shows Whats' Gone Wrong.
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Appropriate To The Error HTTP Response Code. 
    /// </summary>
    public HttpStatusCode Status { get; set; }

    /// <summary>
    /// Preconfigured Algorithm That Systematically Travels a Network to Return Results.
    /// </summary>
    public string? TraceId { get; set; }

    /// <summary>
    /// Defines Collection Of Happened Errors.
    /// </summary>
    // ReSharper disable once CollectionNeverQueried.Global
    public Dictionary<string, List<string>> Errors { get; set; } = new();
    
}