
namespace Base.Contracts.Domain;


/// <summary>
/// Basic Database Entity Design With Predefined Primary Key Type as Guid:
/// - Defines Common Rows Such As ID. 
/// </summary>
public interface IDomainEntityId : IDomainEntityId<Guid> { }


/// <summary>
/// Basic Database Entity Design:
/// - Defines Common Rows Such As ID. 
/// </summary>
/// <typeparam name="TKey">Entity Primary Key Type Definition.</typeparam>
public interface IDomainEntityId<TKey> 
    where TKey: IEquatable<TKey>
{ 
    
    /// <summary>
    /// Defines Entity Primary Key.
    /// </summary>
    TKey Id { get; set; }
    
}