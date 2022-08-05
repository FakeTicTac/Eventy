using Base.Domain;
using Base.Contracts.Domain;


// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global


namespace App.Domain.Identity;


/// <summary>
/// Application Refresh Token Implementation.
///  - Defines Specific Entity Rows for Refresh Token. 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class RefreshToken : DomainEntityUser<AppUser>, IDomainEntityId
{
    
    /// <summary>
    /// Token Signature. 
    /// </summary>
    public string? Value { get; set; }
    
    /// <summary>
    /// Signature Expiration Date and Time. 
    /// </summary>
    public DateTime ExpirationDateTime { get; set; }

}