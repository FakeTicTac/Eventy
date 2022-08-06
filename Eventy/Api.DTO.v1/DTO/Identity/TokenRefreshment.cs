
// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global


namespace Api.DTO.v1.DTO.Identity;


/// <summary>
/// Application Token Refreshment Data Transfer Object.
///  - Defines Data That Should Be Collected From User To Successfully Refresh Token. 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class TokenRefreshment
{
    
    /// <summary>
    /// User Access Token To Be Refreshed.
    /// </summary>
    public string? AccessToken { get; set; }

    /// <summary>
    /// User Refresh Token To Process Access Token Refreshment.
    /// </summary>
    public string? RefreshToken { get; set; }
    
}