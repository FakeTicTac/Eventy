using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;


namespace Base.Domain.Identity;


/// <summary>
/// Identity User Basic Implementation With Predefined Primary Key Type as Guid:
/// </summary>
public class BaseUser : BaseUser<Guid>, IDomainEntityId { }


/// <summary>
///  Identity User Basic Implementation.
/// - Defines Common Rows Such As ID. 
/// </summary>
/// <typeparam name="TKey">Entity Primary Key Type Definition.</typeparam>
public class BaseUser<TKey> : IdentityUser<TKey>, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey> { }