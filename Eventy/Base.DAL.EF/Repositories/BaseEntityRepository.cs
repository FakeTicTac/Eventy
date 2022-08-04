using Base.Domain;
using Base.Contracts.Domain;
using Base.DAL.EF.Exceptions;
using Base.Domain.Translation;
using Base.Contracts.DAL.Mappers;
using Microsoft.EntityFrameworkCore;
using Base.Contracts.Domain.Identity;
using Base.Contracts.DAL.Repositories;


namespace Base.DAL.EF.Repositories;


/// <summary>
/// Data Access Layer Repository Basic Implementation With Predefined Primary Key Type as Guid:
/// </summary>
/// <typeparam name="TDalEntity">Defines Type Of The Data Transfer Object of Data Access Layer To Be Processed.</typeparam>
/// <typeparam name="TDomainEntity">Defines Type Of The Domain Entity To Be Processed.</typeparam>
/// <typeparam name="TDbContext">Defines Type Of The Database Layer Connection.</typeparam>
/// <typeparam name="TUser">Defines Type Of The User Entity.</typeparam>
public class BaseEntityRepository<TDalEntity, TDomainEntity, TUser, TDbContext> :
    BaseEntityRepository<TDalEntity, TDomainEntity, TUser, Guid, TDbContext>

    where TDomainEntity : class, IDomainEntityId
    where TDalEntity : class, IDomainEntityId
    where TUser : class
    where TDbContext : DbContext
{
    
    /// <summary>
    /// Data Access Layer Repository Basic Constructor Defines Connection To The Database Layer and Mapper.
    /// </summary>
    /// <param name="dbContext">Database Layer Connection Definition.</param>
    /// <param name="mapper">Defines Mapper Connection.</param>
    // ReSharper disable once MemberCanBeProtected.Global
    public BaseEntityRepository(TDbContext dbContext, IBaseMapper<TDalEntity, TDomainEntity> mapper) : 
        base(dbContext, mapper) { }
    
}

/// <summary>
/// Data Access Layer Repository Basic Design.
///  - Includes Asynchronous and Synchronous Methods.
/// </summary>
/// <typeparam name="TDalEntity">Defines Type Of The Data Transfer Object of Data Access Layer To Be Processed.</typeparam>
/// <typeparam name="TDomainEntity">Defines Type Of The Domain Entity To Be Processed.</typeparam>
/// <typeparam name="TKey">Defines Type Of Entity Primary Key Value.</typeparam>
/// <typeparam name="TDbContext">Defines Type Of The Database Layer Connection.</typeparam>
/// <typeparam name="TUser">Defines Type Of THe User Entity.</typeparam>
public class BaseEntityRepository<TDalEntity, TDomainEntity, TUser, TKey, TDbContext> : 
    IEntityRepository<TDalEntity, TKey>

    where TDomainEntity : class, IDomainEntityId<TKey>
    where TDalEntity : class, IDomainEntityId<TKey>
    where TUser : class
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
{
    
    /// <summary>
    /// Defines Database Layer Connection.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly TDbContext RepoDbContext;

    
    /// <summary>
    /// Defines Mapper Connection.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly IBaseMapper<TDalEntity, TDomainEntity> Mapper;
    
    
    /// <summary>
    /// Defines Database Layer Entity Set Connection.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly DbSet<TDomainEntity> RepoDbSet;


    /// <summary>
    /// Data Access Layer Repository Basic Constructor Defines Connection To The Database Layer and Mapper.
    /// </summary>
    /// <param name="dbContext">Database Layer Connection Definition.</param>
    /// <param name="mapper">Defines Mapper Connection.</param>
    // ReSharper disable once MemberCanBeProtected.Global
    public BaseEntityRepository(TDbContext dbContext, IBaseMapper<TDalEntity, TDomainEntity> mapper)
    {
        RepoDbContext = dbContext;
        Mapper = mapper;
        
        // Entity Set Creation
        RepoDbSet = dbContext.Set<TDomainEntity>();
    }
    
    
    // Synchronous Methods.


    /// <summary>
    /// Method Adds Given Entity to the Database. (Secured at Database Layer)
    /// </summary>
    /// <param name="entity">Entity Value To Be Process.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>The Value of Added Entity.</returns>
    public virtual TDalEntity Add(
        TDalEntity entity, 
        object? userId = null
        ) => Mapper.Map(
        SecurityBasicHandler(Mapper.Map(entity)!,
            () => RepoDbSet.Add(Mapper.Map(entity)!).Entity, userId))!;

    
    /// <summary>
    /// Method Updates Given Entity in the Database. (Secured at Database Layer)
    /// </summary>
    /// <param name="entity">Entity Value To Be Process.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>The Value of Updated Entity.</returns>
    public virtual TDalEntity Update(
        TDalEntity entity, 
        object? userId = null
        ) => Mapper.Map(
        SecurityBasicHandler(Mapper.Map(entity)!, 
            () => JsonbUpdate(RepoDbSet.Update(Mapper.Map(entity)!).Entity), userId))!;
    
    
    /// <summary>
    /// Method Removes Given Entity From the Database. (Secured at Database Layer)
    /// </summary>
    /// <param name="entity">Entity Value To Be Process.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>The Value of Removed Entity.</returns>
    public virtual TDalEntity Remove(
        TDalEntity entity, 
        object? userId = null
        ) => Mapper.Map(
        SecurityBasicHandler(Mapper.Map(entity)!,
            () => RepoDbSet.Remove(Mapper.Map(entity)!).Entity, userId))!;
    
    
    /// <summary>
    /// Method Removes Given Entity From the Database using Guid Value as Indicator. (Secured at Database Layer)
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Removed From The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>The Value of Removed Entity.</returns>
    /// <exception cref="NullReferenceException">Thrown if Entity Doesn't Exist in Database.</exception>
    public virtual TDalEntity Remove(
        TKey id, 
        object? userId = null
        )
    {
        
        // Try To Get Entity From Database Layer.
        var entity = FirstOrDefault(id);
        
        if (entity == null) 
            throw new DataExistenceException($"Entity with given ID {id} doesn't exist in database layer.");
        
        return Mapper.Map(SecurityBasicHandler(
            Mapper.Map(entity)!, 
            () => RepoDbSet.Remove(Mapper.Map(entity)!).Entity, userId))!;
        
    }
    
    
    /// <summary>
    /// Method Get Entity of The Given Type ID The Database if Found or Null.
    /// Secured at Database Layer Only User Related If Implements Class.
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>The Value of Found Entity or Null.</returns>
    public virtual TDalEntity? FirstOrDefault(
        TKey id, 
        object? userId = null, 
        bool noTracking = true
        ) => Mapper.Map(CreateQuery(userId, noTracking).FirstOrDefault(x => x.Id.Equals(id)));
    
    
    /// <summary>
    /// Method Get All Entities of The Given Type From The Database.
    /// Secured at Database Layer Only User Related If Implements Class.
    /// </summary>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>The Value of Enumerable of Entities.</returns>
    public virtual IEnumerable<TDalEntity> GetAll(
        object? userId = null, 
        bool noTracking = true
        ) => CreateQuery(userId, noTracking).ToList().Select(x => Mapper.Map(x))!;


    /// <summary>
    /// Method Indicates If Entity With Given ID Exist In Database.
    /// Secured at Database Layer Only User Related If Implements Class.
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>The Value of Boolean as an Indicator of Entity Existence.</returns>
    public virtual bool Exist(
        TKey id,
        object? userId = null
    ) => CreateQuery(userId).ToList().Any(x => x.Id.Equals(id));

    
    // Asynchronous Methods.


    /// <summary>
    /// Method Removes Entity From The Database Asynchronously Using ID. (Secured at Database Layer)
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Removed From The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Removed Entity.</returns>
    /// <exception cref="NullReferenceException">Thrown if Entity Doesn't Exist in Database.</exception>
    public virtual async Task<TDalEntity> RemoveAsync(
        TKey id, 
        object? userId = null
        ) 
    {
        
        // Try To Get Entity From Database Layer.
        var entity = await FirstOrDefaultAsync(id);
        
        if (entity == null) 
            throw new DataExistenceException($"Entity with given ID {id} doesn't exist in database layer.");

        return Remove(entity, userId);

    }

    /// <summary>
    /// Method Get All Entities of The Given Type From The Database Asynchronously.
    /// Secured at Database Layer Only User Related If Implements Class)
    /// </summary>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Enumerable of Entities.</returns>
    public virtual async Task<IEnumerable<TDalEntity>> GetAllAsync(
        object? userId = null, 
        bool noTracking = true
        ) => (await CreateQuery(noTracking).ToListAsync()).Select(x => Mapper.Map(x))!;

    
    /// <summary>
    /// Method Get Entity of The Given Type ID The Database Asynchronously if Found or Null.
    /// Secured at Database Layer Only User Related If Implements Class)
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <param name="noTracking">Defines Tracking Option.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Found Entity or Null.</returns>
    public virtual async Task<TDalEntity?> FirstOrDefaultAsync(
        TKey id, 
        object? userId = null, 
        bool noTracking = true
        ) => Mapper.Map(await CreateQuery(noTracking).FirstOrDefaultAsync(x => x.Id.Equals(id)));


    /// <summary>
    /// Method Indicates If Entity With Given ID Exist In Database.
    /// Secured at Database Layer Only User Related If Implements Specific Class.
    /// </summary>
    /// <param name="id">Defines Entity ID To Be Searched in The Database.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns>Asynchronous Operation That Returns The Value of Boolean as an Indicator of Entity Existence.</returns>
    public virtual async Task<bool> ExistAsync(
        TKey id,
        object? userId = null
    ) => (await CreateQuery(userId).ToListAsync()).Any(x => x.Id.Equals(id));


    // Helping Methods.


    /// <summary>
    /// Method Creates Query to Connect with the Database.
    /// </summary>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <param name="noTracking">>Tracking Option Definition.</param>
    /// <returns>IQueryable of TEntity. (Query)</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    protected virtual IQueryable<TDomainEntity> CreateQuery(
        object? userId = null, 
        bool noTracking = true
        )
    {
        var query = RepoDbSet.AsQueryable();
        
        // Take Care of Security Issues. Initially, Only Created Entity Users Can Modify It if Derived From Specific Class.
        if (userId != null && typeof(IDomainEntityUser<TUser>).IsAssignableFrom(typeof(TDomainEntity)))
        {
            query = query.Where(e => Microsoft.EntityFrameworkCore.EF
                    .Property<TKey>(e, nameof(IDomainEntityUser<TUser>.AppUserId))
                    .Equals((TKey) userId));
        }

        // Manage Tracking options.
        return noTracking ? query.AsNoTracking() : query;
    }
    
    
    /// <summary>
    /// Method Handles Database Security on Basic Level. Only Created Entity Users Can Modify It if Derived From Specific Class.
    /// </summary>
    /// <param name="entity">Defines Entity To Be Processed.</param>
    /// <param name="operation">Operation Declaration To Be Performed on Database Layer.</param>
    /// <param name="userId">Defines Entity Demanding User ID Value.</param>
    /// <returns></returns>
    protected virtual TDomainEntity? SecurityBasicHandler(
        TDomainEntity entity, 
        Func<TDomainEntity> operation, 
        object? userId = null
        )
    {
        
        // Take Care of Security Issues. Initially, Only Created Entity Users Can Modify It if Derived From Specific Class.
        if (!typeof(IDomainEntityUser<TUser>).IsAssignableFrom(typeof(TDomainEntity))) return operation();
        
        // Data Update Should Be Secure, But No ID Given For Security Check.
        if (userId == null)
            throw new DataSecurityAccessException($"Trying to modify entity with ID {entity.Id}, while user ID is not defined.");
        
        // Check If Given Entity With ID Really Belongs To User.
        var queryResult = RepoDbSet.AsQueryable()
            .Any(x => Microsoft.EntityFrameworkCore.EF.Property<TKey>(x, nameof(IDomainEntityUser<TUser>.AppUserId))
                .Equals((TKey) userId) && x.Id.Equals(entity.Id));

        if (!queryResult) return null;

            // Operate With The Entity In Database.
        return operation();
        
    }
    
    
    /// <summary>
    /// Methods Checks if Data Exist in Json and Adds It To The Dictionary.
    /// </summary>
    /// <param name="entity">Entity To Be Processed.</param>
    /// <returns>Modified Entity To Be Updated In Database.</returns>
    // ReSharper disable once SuggestBaseTypeForParameter
    private TDomainEntity JsonbUpdate(TDomainEntity entity)
    {
        if (!typeof(TDomainEntity).IsSubclassOf(typeof(DomainEntityMetaId))) 
            throw new NotSupportedEntityTypeException("Entity is not derived from specified entity.");
        
        var oldEntity = RepoDbSet.AsNoTracking().ToList().First(x =>
        {
            var searchedObj = x.GetType().GetProperty("Id");
            var givenObj = entity.GetType().GetProperty("Id");
            
            // Check If Entities Have ID Property.
            if (searchedObj == null) throw new PropertyIdExistenceException("Searched object doesn't have ID property.");

            if (givenObj == null) throw new PropertyIdExistenceException("Given object doesn't have ID property.");

            return searchedObj.GetValue(x)!.Equals(givenObj.GetValue(entity));
        });
        
        var properties = oldEntity.GetType().GetProperties();
        
        // Change All LanguageString Properties By Adding To Existed In Dictionary.
        foreach (var property in properties)
        {
            if (property.PropertyType != typeof(LanguageString)) continue;
            
            var value = (LanguageString) property.GetValue(oldEntity)!;

            value.SetTranslation((LanguageString) property.GetValue(entity)!);
            property.SetValue(entity, value);
        }

        return entity;
    }
}