
namespace Base.Contracts.Domain;


/// <summary>
/// Basic Database Entity Design: Uses Guid Value as Primary Key Type.
/// </summary>
// ReSharper disable UnusedMemberInSuper.Global
public interface IDomainEntityId : IDomainEntityId<Guid> { }


/// <summary>
/// Basic Database Entity Design: Defines Common Rows in Database Table.
/// </summary>
/// <typeparam name="TKey">Entity Primary Key Definition.</typeparam>
public interface IDomainEntityId<TKey> 
    where TKey: IEquatable<TKey>
{ 
    
    /// <summary>
    /// Entity Primary Key Definition.
    /// </summary>
    TKey Id { get; set; }
    
}