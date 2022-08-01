using Base.Contracts.Domain;
using System.ComponentModel.DataAnnotations;


namespace Base.Domain;


/// <summary>
/// Database Meta-Data Entity Basic Implementation With Predefined Primary Key Type as Guid:
/// </summary>
public abstract class DomainEntityMetaId : DomainEntityMetaId<Guid>, IDomainEntityId { }


/// <summary>
/// Basic Database Entity Meta-Data Implementation:
/// - Defines Common Meta Rows Such As Created At, Created By and etc. 
/// </summary>
/// <typeparam name="TKey">Entity Primary Key Type Definition.</typeparam>
public abstract class DomainEntityMetaId<TKey> : DomainEntityId<TKey>, IDomainEntityMeta
    where TKey : IEquatable<TKey>
{
    
    /// <summary>
    /// Defines Data Related to Entity Insertion Time. 
    /// </summary>
    [MaxLength(64)]
    [Display(ResourceType = typeof(Base.Resources.Base.Domain.Domain), Name = nameof(CreatedBy))]
    public string? CreatedBy { get; set; }
    
    /// <summary>
    /// Defines Data Related to User/System Who Proceed Insertion. 
    /// </summary>
    [Display(ResourceType = typeof(Base.Resources.Base.Domain.Domain), Name = nameof(CreatedAt))]
    public DateTime? CreatedAt { get; set; }
    
    /// <summary>
    /// Defines Data Related to Entity Update Time. 
    /// </summary>
    [MaxLength(64)]
    [Display(ResourceType = typeof(Base.Resources.Base.Domain.Domain), Name = nameof(ModifiedBy))]
    public string? ModifiedBy { get; set; }
    
    /// <summary>
    /// Defines Data Related to User/System Who Proceed Update. 
    /// </summary>
    [Display(ResourceType = typeof(Base.Resources.Base.Domain.Domain), Name = nameof(ModifiedAt))]
    public DateTime? ModifiedAt { get; set; }
    
}