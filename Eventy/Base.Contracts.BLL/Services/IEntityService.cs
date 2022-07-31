using Base.Contracts.Domain;
using Base.Contracts.DAL.Repositories;


namespace Base.Contracts.BLL.Services;


/// <summary>
/// Business Logic Layer Service Basic Design With Predefined Primary Key Type as Guid:
/// </summary>
/// <typeparam name="TBllEntity">Defines Type Of Data Transfer Object To Be Processed In Business Logic Layer.</typeparam>
/// <typeparam name="TDalEntity">Defines Type Of Data Transfer Object To Be Processed In Data Access Layer.</typeparam>
public interface IEntityService<TBllEntity, TDalEntity> : IEntityService<TBllEntity, TDalEntity, Guid>
    where TBllEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId { }


/// <summary>
/// Business Logic Layer Service Basic Design With Predefined Primary Key Type as Guid:
/// </summary>
/// <typeparam name="TBllEntity">Defines Type Of Data Transfer Object To Be Processed In Business Logic Layer.</typeparam>
/// <typeparam name="TDalEntity">Defines Type Of Data Transfer Object To Be Processed In Data Access Layer.</typeparam>
/// <typeparam name="TKey">Defines Type Of Entity Primary Key Value.</typeparam>
// ReSharper disable once UnusedTypeParameter
public interface IEntityService<TBllEntity, TDalEntity, TKey> : IEntityRepository<TBllEntity, TKey>, IBaseService
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey> { }