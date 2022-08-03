
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global


namespace Api.DTO.v1.DTO.Identity;


/// <summary>
/// Application JWT Token Data Transfer Object.
///  - Data That Should Define User.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class Jwt
{
    
    /// <summary>
    /// JWT Token Value Produced And Encrypted By Backend.
    /// </summary>
    public string? TokenValue { get; set; }
    
    /// <summary>
    /// Part of Users' Account Data and Especially Username. 
    /// </summary>
    public string? Username { get; set; }
    
}