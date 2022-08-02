using Base.Domain;
using Base.Domain.Translation;


// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global


namespace App.DAL.DTO;


/// <summary>
/// Application Event Table Implementation.
///  - Defines Specific Entity Rows for Event. 
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class Event : DomainEntityId
{

    /// <summary>
    /// Defines Event Title.
    /// </summary>
    public LanguageString Title { get; set; } = new();
    
    
    /// <summary>
    /// Defines Event Description.
    /// </summary>
    public LanguageString Description { get; set; } = new();

    
    /// <summary>
    /// Defines Event Full Address and Location.
    /// </summary>
    public string? Address { get; set; }
    
    
    /// <summary>
    /// Defines Event Starting Time.
    /// </summary>
    public DateTime? StartTime { get; set; }
    
    
    /// <summary>
    /// Defines Event Cover Image Path on Server Side.
    /// </summary>
    public string? CoverImagePath { get; set; }
    
    
    // EF CORE Related Relations Are Going Next -->
    
    
    /// <summary>
    /// Defines Event Belonging To The User ID.
    /// </summary>
    public Guid AppUserId { get; set; }
    
}