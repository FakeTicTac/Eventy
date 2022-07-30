
namespace Base.Contracts.Domain;


/// <summary>
/// Basic Database Meta-Data Entity Design: Defines Common Rows in Database Table.
/// </summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface IDomainEntityMeta
{
    
    /// <summary>
    /// Defines Data Insertion Time. 
    /// </summary>
    string? CreatedBy { get; set; }
    
    /// <summary>
    /// Defines User/System Who Inserted The Data.
    /// </summary>
    DateTime? CreatedAt { get; set; }
    
    /// <summary>
    /// Defines Data Update Time. 
    /// </summary>
    string? ModifiedBy { get; set; }
    
    /// <summary>
    /// Defines User/System Who Updated The Data.
    /// </summary>
    DateTime? ModifiedAt { get; set; }
    
}