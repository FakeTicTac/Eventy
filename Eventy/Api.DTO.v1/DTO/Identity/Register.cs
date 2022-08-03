
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global


namespace Api.DTO.v1.DTO.Identity;


/// <summary>
/// Application Register Data Transfer Object.
///  - Defines Data That Should Be Collected From User To Be Successfully Registered. 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class Register
{
    
    /// <summary>
    /// Users' Personal Data and Especially Email Address.
    /// </summary>
    public string? Email { get; set; } 
        
    /// <summary>
    /// Users' Personal Data and Especially Unique Identifier.
    /// </summary>
    public string? UserName { get; set; }
    
    /// <summary>
    /// Users' Personal Data and Especially Password.
    /// </summary>
    public string? Password { get; set; }
    
}