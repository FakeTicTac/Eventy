using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Base.Contracts.BLL.Mappers;
using Base.Contracts.BLL.Services;
using Base.Contracts.DAL.Repositories;


namespace Base.BLL.Services;


/// <summary>
/// Business Logic Layer Service Basic Implementation With Predefined Primary Key Type as Guid:
/// </summary>
/// <typeparam name="TBllEntity">Defines Type Of Data Transfer Object To Be Processed In Business Logic Layer.</typeparam>
/// <typeparam name="TDalEntity">Defines Type Of Data Transfer Object To Be Processed In Data Access Layer.</typeparam>
/// <typeparam name="TUnitOfWork">Data Access Layer Unit of Work Type Connection Definition.</typeparam>
/// <typeparam name="TRepository">Data Access Layer Repository Type Definition To Work With.</typeparam>
// ReSharper disable MemberCanBePrivate.Global
public class BaseEntityService<TBllEntity, TDalEntity, TUnitOfWork, TRepository> : 
    BaseEntityService<TBllEntity, TDalEntity, Guid, TUnitOfWork, TRepository>, IEntityService<TBllEntity, TDalEntity>

    where TBllEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TUnitOfWork : IUnitOfWork
    where TRepository : IEntityRepository<TDalEntity>
{
    /// <summary>
    /// Basis Business Logic Layer Constructor Defines Connection With Repository And Unit Of Work. 
    /// </summary>
    /// <param name="serviceUow">Data Access Layer Unit of Work Connection Definition.</param>
    /// <param name="serviceRepository">Data Access Layer Specific Repository Connection Definition.</param>
    /// <param name="mapper">Mapper Connection Definition.</param>
    // ReSharper disable once MemberCanBeProtected.Global
    public BaseEntityService(TUnitOfWork serviceUow, TRepository serviceRepository, IBaseMapper<TBllEntity, TDalEntity> mapper) 
        : base(serviceUow, serviceRepository, mapper) { }
    
}


/// <summary>
/// Business Logic Layer Service Basic Implementation:
/// </summary>
/// <typeparam name="TBllEntity">Defines Type Of Data Transfer Object To Be Processed In Business Logic Layer.</typeparam>
/// <typeparam name="TDalEntity">Defines Type Of Data Transfer Object To Be Processed In Data Access Layer.</typeparam>
/// <typeparam name="TKey">Defines Type Of Entity Primary Key Value.</typeparam>
/// <typeparam name="TUnitOfWork">Data Access Layer Unit of Work Type Connection Definition.</typeparam>
/// <typeparam name="TRepository">Data Access Layer Repository Type Definition To Work With.</typeparam>
public class BaseEntityService<TBllEntity, TDalEntity, TKey, TUnitOfWork, TRepository> : IEntityService<TBllEntity, TDalEntity, TKey>
    where TBllEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TUnitOfWork : IUnitOfWork
    where TRepository : IEntityRepository<TDalEntity, TKey>
{
    
    /// <summary>
    /// Data Access Layer Unit Of Work Connection Definition.
    /// </summary>
    protected readonly TUnitOfWork ServiceUow;
    
    
    /// <summary>
    /// Data Access Layer Mapper Connection Definition.
    /// </summary>
    protected readonly IBaseMapper<TBllEntity, TDalEntity> Mapper;
    
    
    /// <summary>
    /// Data Access Layer Repository Definition To Work With.
    /// </summary>
    protected readonly TRepository ServiceRepository;


    /// <summary>
    /// Service Constructor Defines Connection With Data Access Layer, Unit of Work and Repository. 
    /// </summary>
    /// <param name="serviceUow">Data Access Layer Unit of Work Connection Definition.</param>
    /// <param name="serviceRepository">Data Access Layer Specific Repository Connection Definition.</param>
    /// <param name="mapper">Mapper Connection Definition.</param>
    // ReSharper disable once MemberCanBeProtected.Global
    public BaseEntityService(TUnitOfWork serviceUow, TRepository serviceRepository, IBaseMapper<TBllEntity, TDalEntity> mapper)
    {
        ServiceUow = serviceUow;
        ServiceRepository = serviceRepository;
        Mapper = mapper;
    }

    
    // Synchronous Methods.


    /// <summary>
    /// Method Adds Given Entity to the Database. (Not Secured at Database Layer)
    /// </summary>
    /// <param name="entity">Entity Value To Be Process.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>The Value of Added Entity.</returns>
    public virtual TBllEntity Add(
        TBllEntity entity, 
        object? userId = null
        ) => Mapper.Map(ServiceRepository.Add(Mapper.Map(entity)!, userId))!;

    
    /// <summary>
    /// Method Updates Given Entity in the Database. (Secured at Database Layer)
    /// </summary>
    /// <param name="entity">Entity Value To Be Process.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>The Value of Updated Entity.</returns>
    public virtual TBllEntity Update(
        TBllEntity entity, 
        object? userId = null
        ) => Mapper.Map(ServiceRepository.Update(Mapper.Map(entity)!, userId))!;
    
    
    /// <summary>
    /// Method Removes Given Entity From the Database. (Secured at Database Layer)
    /// </summary>
    /// <param name="entity">Entity Value To Be Process.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>The Value of Removed Entity.</returns>
    public virtual TBllEntity Remove(
        TBllEntity entity, 
        object? userId = null
        ) => Mapper.Map(ServiceRepository.Remove(Mapper.Map(entity)!, userId))!;

    
    /// <summary>
    /// Method Removes Given Entity From the Database using Guid Value as Indicator. (Secured at Database Layer)
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Removed From The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>The Value of Removed Entity.</returns>
    /// <exception cref="NullReferenceException">Thrown if Entity Doesn't Exist in Database.</exception>
    public virtual TBllEntity Remove(
        TKey id, 
        object? userId = null
        ) => Mapper.Map(ServiceRepository.Remove(id, userId))!;

    
    /// <summary>
    /// Method Get Entity of The Given Type ID The Database if Found or Null.
    /// Secured at Database Layer Only User Related If Implements Class)
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>The Value of Found Entity or Null.</returns>
    public virtual TBllEntity? FirstOrDefault(
        TKey id, 
        object? userId = null, 
        bool noTracking = true
        ) => Mapper.Map(ServiceRepository.FirstOrDefault(id, userId, noTracking));

    
    /// <summary>
    /// Method Get All Entities of The Given Type From The Database.
    /// Secured at Database Layer Only User Related If Implements Class)
    /// </summary>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>The Value of Enumerable of Entities.</returns>
    public virtual IEnumerable<TBllEntity> GetAll(
        object? userId = null, 
        bool noTracking = true
        ) => ServiceRepository.GetAll(userId, noTracking).Select(x => Mapper.Map(x))!;

    
    /// <summary>
    /// Method Indicates If Entity With Given ID Exist In Database.
    /// Secured at Database Layer Only User Related If Implements Class)
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>The Value of Boolean as an Indicator of Entity Existence.</returns>
    public virtual bool Exist(
        TKey id, 
        object? userId = null
        ) => ServiceRepository.Exist(id, userId);

    
    // Asynchronous Methods.

    
    /// <summary>
    /// Method Removes Entity From The Database Asynchronously Using ID. (Secured at Database Layer)
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Removed From The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Removed Entity.</returns>
    /// <exception cref="NullReferenceException">Thrown if Entity Doesn't Exist in Database.</exception>
    public virtual async Task<TBllEntity> RemoveAsync(
        TKey id, 
        object? userId = null
        ) => Mapper.Map(await ServiceRepository.RemoveAsync(id, userId))!;

    
    /// <summary>
    /// Method Get All Entities of The Given Type From The Database Asynchronously.
    /// Secured at Database Layer Only User Related If Implements Class)
    /// </summary>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Enumerable of Entities.</returns>
    public virtual async Task<IEnumerable<TBllEntity>> GetAllAsync(
        object? userId = null, 
        bool noTracking = true
        ) => (await ServiceRepository.GetAllAsync(userId, noTracking)).Select(x => Mapper.Map(x))!;

    
    /// <summary>
    /// Method Get Entity of The Given Type ID The Database Asynchronously if Found or Null.
    /// Secured at Database Layer Only User Related If Implements Class)
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Found Entity or Null.</returns>
    public virtual async Task<TBllEntity?> FirstOrDefaultAsync(
        TKey id, 
        object? userId = null, 
        bool noTracking = true
        ) => Mapper.Map(await ServiceRepository.FirstOrDefaultAsync(id, userId, noTracking));

    
    /// <summary>
    /// Method Indicates If Entity With Given ID Exist In Database.
    /// Secured at Database Layer Only User Related If Implements Class)
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Boolean as an Indicator of Entity Existence.</returns>
    public async Task<bool> ExistAsync(
        TKey id, 
        object? userId = null
        ) => await ServiceRepository.ExistAsync(id, userId);
    
}