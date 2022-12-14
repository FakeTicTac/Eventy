using AutoMapper;
using Base.Contracts.DAL.Mappers;


namespace Base.DAL.EF.Mappers;


/// <summary>
/// Data Access Layer Mapper Rules Implementation.
/// </summary>
/// <typeparam name="TLeftObject">First (Left) Object To Be Proceed For Mapping.</typeparam>
/// <typeparam name="TRightObject">Second (Right) Object To Be Proceed For Mapping.</typeparam>
public class BaseMapper<TLeftObject, TRightObject> : IBaseMapper<TLeftObject, TRightObject>
    where TLeftObject : class
    where TRightObject : class
{
    
    /// <summary>
    /// Mapper Connection Definition.
    /// </summary>
    // ReSharper disable once MemberCanBePrivate.Global
    protected readonly IMapper Mapper;
        
        
    /// <summary>
    /// Basic Mapper Constructor: Defines Connection to The Mapper Profile.
    /// </summary>
    /// <param name="mapper">Mapper Connection Definition.</param>
    // ReSharper disable once MemberCanBeProtected.Global
    public BaseMapper(IMapper mapper) => Mapper = mapper;

    
    /// <summary>
    /// Method Maps the First (Left) Object to the Second (Right) Object.
    /// </summary>
    /// <param name="inObject">Defines Object To Be Mapped.</param>
    /// <returns>First (Left) Object Mapped To Second (Right) Object.</returns>
    public virtual TRightObject? Map(TLeftObject? inObject) => inObject == null ? Mapper.Map<TRightObject>(inObject) : null;

    
    /// <summary>
    /// Method Maps the Second (Right) Object to the First (Left) Object.
    /// </summary>
    /// <param name="inObject">Defines Object To Be Mapped.</param>
    /// <returns>Second (Right) Object Mapped To First (Left) Object.</returns>
    public virtual TLeftObject? Map(TRightObject? inObject) => inObject == null ? Mapper.Map<TLeftObject>(inObject) : null;
    
}