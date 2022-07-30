
namespace Base.Contracts.Domain.Identity;


/// <summary>
/// Basic Database Related To User Entity Design With Predefined Foreign Key Type as Guid:
/// - Defines Common Rows Such As User ID. 
/// </summary>
/// <typeparam name="TAppUser">Entity User Related Foreign Key Type Definition.</typeparam>
public interface IDomainEntityUser<TAppUser> : IDomainEntityUser<Guid, TAppUser>
    where TAppUser: class { }


/// <summary>
/// Basic Database Related To User Entity Design:
/// - Defines Common Rows Such As User ID. 
/// </summary>
/// <typeparam name="TKey">Entity Primary Key Type Definition.</typeparam>
/// <typeparam name="TAppUser">Entity User Related Foreign Key Type Definition.</typeparam>
public interface IDomainEntityUser<TKey, TAppUser>
    where TKey: IEquatable<TKey>
    where TAppUser: class
{
    
    /// <summary>
    /// Defines Entity User Foreign Key Related To User.
    /// </summary>
    TKey AppUserId { get; set; }
    
    /// <summary>
    /// Defines Entity User Object Related To User.
    /// </summary>
    TAppUser? AppUser { get; set; }
    
}