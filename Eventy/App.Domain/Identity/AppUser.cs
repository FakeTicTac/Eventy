using Base.Domain.Identity;


// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global


namespace App.Domain.Identity;


/// <summary>
/// Application Identity User Implementation.
///  - Defines Specific Entity Rows for Identity User. 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class AppUser : BaseUser
{
    
    // EF CORE Related Relations Are Going Next -->
    
    
    /// <summary>
    /// Users' Refresh Tokens For JWT Token Update.
    /// </summary>
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
    
}