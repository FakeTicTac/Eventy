using Base.Contracts.Domain;


namespace Base.Contracts.DAL.Repositories;


/// <summary>
/// Data Access Layer Repository Basic Design With Predefined Primary Key Type as Guid:
/// </summary>
/// <typeparam name="TDalEntity">Defines Type Of Data Transfer Object To Be Processed In Data Access Layer.</typeparam>
public interface IEntityRepository<TDalEntity> : IEntityRepository<TDalEntity, Guid>
    where TDalEntity : class, IDomainEntityId { }


/// <summary>
/// Data Access Layer Repository Basic Design.
///  - Includes Asynchronous and Synchronous Methods.
/// </summary>
/// <typeparam name="TDalEntity">Defines Type Of Data Transfer Object To Be Processed In Data Access Layer.</typeparam>
/// <typeparam name="TKey">Defines Type Of Entity Primary Key Value.</typeparam>
public interface IEntityRepository<TDalEntity, TKey> : IEntityRepositoryAsync<TDalEntity, TKey>, IEntityRepositorySync<TDalEntity, TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey> { }