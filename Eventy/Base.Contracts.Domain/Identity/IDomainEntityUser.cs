
namespace Base.Contracts.Domain.Identity;


/// <summary>
/// Basic Database User Identity Related Entity Design: Uses Guid Value as Foreign Key Type.
/// </summary>
/// <typeparam name="TAppUser">Entity User Identity Object Definition.</typeparam>
// ReSharper disable UnusedMemberInSuper.Global
public interface IDomainEntityUser<TAppUser> : IDomainEntityUser<Guid, TAppUser>
    where TAppUser: class { }


/// <summary>
/// Basic Database User Identity Related Entity Design: Defines Common Rows in Database Table.
/// </summary>
/// <typeparam name="TKey">Entity Primary Key Definition.</typeparam>
/// <typeparam name="TAppUser">Entity User Identity Object Definition.</typeparam>
public interface IDomainEntityUser<TKey, TAppUser>
    where TKey: IEquatable<TKey>
    where TAppUser: class
{
    
    /// <summary>
    /// Entity Foreign Key To Identity System Definition.
    /// </summary>
    TKey AppUserId { get; set; }
    
    /// <summary>
    /// Entity Connection Object To Identity System Definition.
    /// </summary>
    TAppUser? AppUser { get; set; }
    
}