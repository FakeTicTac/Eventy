using Base.Contracts.Domain.Identity;
using System.ComponentModel.DataAnnotations;


namespace Base.Domain;


/// <summary>
/// Database Identity Related Entity Basic Implementation With Predefined Primary Key Type as Guid:
/// </summary>
/// <typeparam name="TAppUser">Entity User Identity Object Type Definition.</typeparam>
public abstract class DomainEntityUser<TAppUser> : DomainEntityUser<Guid, TAppUser>
    where TAppUser : class { }


/// <summary>
/// Basic Database Identity Related Entity Implementation:
/// - Defines Common Rows Such As User ID. 
/// </summary>
/// <typeparam name="TKey">Entity Primary Key Type Definition.</typeparam>
/// <typeparam name="TAppUser">Entity User Identity Object Type Definition.</typeparam>
public abstract class DomainEntityUser<TKey, TAppUser> : DomainEntityMetaId<TKey>, IDomainEntityUser<TAppUser>
    where TKey : IEquatable<TKey>
    where TAppUser : class
{
    
    /// <summary>
    /// Defines Entity User Foreign Key Related To User.
    /// </summary>
    [Display(ResourceType = typeof(Base.Resources.Base.Domain.Domain), Name = nameof(AppUserId))]
    public Guid AppUserId { get; set; }
    
    /// <summary>
    /// Defines Entity User Object Related To User.
    /// </summary>
    public TAppUser? AppUser { get; set; }
    
}