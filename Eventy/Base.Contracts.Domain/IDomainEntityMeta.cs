
namespace Base.Contracts.Domain;


/// <summary>
/// Basic Database Entity Meta-Data Design:
/// - Defines Common Meta Rows Such As Created At, Created By and etc. 
/// </summary>
public interface IDomainEntityMeta
{
    
    /// <summary>
    /// Defines Data Related to Entity Insertion Time. 
    /// </summary>
    string? CreatedBy { get; set; }
    
    /// <summary>
    /// Defines Data Related to User/System Who Proceed Insertion. 
    /// </summary>
    DateTime? CreatedAt { get; set; }
    
    /// <summary>
    /// Defines Data Related to Entity Update Time. 
    /// </summary>
    string? ModifiedBy { get; set; }
    
    /// <summary>
    /// Defines Data Related to User/System Who Proceed Update. 
    /// </summary>
    DateTime? ModifiedAt { get; set; }
    
}