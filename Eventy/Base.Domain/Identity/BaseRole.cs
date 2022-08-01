using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;


namespace Base.Domain.Identity;


/// <summary>
/// Identity Role Basic Implementation With Predefined Primary Key Type as Guid:
/// </summary>
public class BaseRole : BaseRole<Guid>, IDomainEntityId { }


/// <summary>
///  Identity Role Basic Implementation.
/// - Defines Common Rows Such As ID. 
/// </summary>
/// <typeparam name="TKey">Entity Primary Key Type Definition.</typeparam>
public class BaseRole<TKey> : IdentityRole<TKey>, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey> { }