using Base.Contracts.Domain;
using System.ComponentModel.DataAnnotations;


namespace Base.Domain;


/// <summary>
/// Database Entity Basic Implementation With Predefined Primary Key Type as Guid:
/// </summary>
public abstract class DomainEntityId : DomainEntityId<Guid>, IDomainEntityId { }


/// <summary>
///  Database Entity Basic Implementation.
/// - Defines Common Rows Such As ID. 
/// </summary>
/// <typeparam name="TKey">Entity Primary Key Type Definition.</typeparam>
public abstract class DomainEntityId<TKey> : IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    
    /// <summary>
    /// Defines Entity Primary Key.
    /// </summary>
    [Display(ResourceType = typeof(Base.Resources.Base.Domain.Domain), Name = nameof(Id))]
    public TKey Id { get; set; } = default!;
    
}