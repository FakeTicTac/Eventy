using Base.Domain;
using App.Domain.Identity;
using Base.Contracts.Domain;
using Base.Domain.Translation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


// ReSharper disable CollectionNeverUpdated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global


namespace App.Domain;


/// <summary>
/// Application Data Access Layer Event Data Transfer Object.
///  - Data That Should Define Event.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class Event : DomainEntityUser<AppUser>, IDomainEntityId
{

    /// <summary>
    /// Defines Event Title.
    /// </summary>
    [Column(TypeName = "jsonb")]
    public LanguageString Title { get; set; } = new();
    
    
    /// <summary>
    /// Defines Event Description.
    /// </summary>
    [Column(TypeName = "jsonb")]
    public LanguageString Description { get; set; } = new();

    
    /// <summary>
    /// Defines Event Full Address and Location.
    /// </summary>
    [MaxLength(256)]
    public string? Address { get; set; }
    
    
    /// <summary>
    /// Defines Event Starting Time.
    /// </summary>
    public DateTime? StartTime { get; set; }
    
    
    /// <summary>
    /// Defines Event Cover Image Path on Server Side.
    /// </summary>
    [MaxLength(260)]
    public string? CoverImagePath { get; set; }
    
}