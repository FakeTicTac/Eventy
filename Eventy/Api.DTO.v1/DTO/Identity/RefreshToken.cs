using Base.Domain;


// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global


namespace Api.DTO.v1.DTO.Identity;


/// <summary>
/// Application Refresh Token Data Transfer Object.
///  - Defines Data That Should Be Collected From User To Successfully Refresh Token. 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class RefreshToken : DomainEntityId
{
    
    /// <summary>
    /// Token Signature. 
    /// </summary>
    public string? Value { get; set; }
    
    /// <summary>
    /// Signature Expiration Date and Time. 
    /// </summary>
    public DateTime ExpirationDateTime { get; set; }
    
    
    // EF CORE Related Relations Are Going Next -->
    
    
    /// <summary>
    /// Defines Refresh Token Belonging To The User ID.
    /// </summary>
    public Guid AppUserId { get; set; }
    
}