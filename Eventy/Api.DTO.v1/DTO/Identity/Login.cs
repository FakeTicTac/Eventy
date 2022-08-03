
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global


namespace Api.DTO.v1.DTO.Identity;


/// <summary>
/// Application Login Data Transfer Object.
///  - Defines Data That Should Be Collected From User To Be Successfully Logged In. 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class Login
{
    
    /// <summary>
    /// Users' Personal Data and Especially Unique Identifier.
    ///  - Can Be Email or Username.
    /// </summary>
    public string? LoginIdentifier { get; set; } 
    
    /// <summary>
    /// Users' Personal Data and Especially Password.
    /// </summary>
    public string? Password { get; set; }
    
}