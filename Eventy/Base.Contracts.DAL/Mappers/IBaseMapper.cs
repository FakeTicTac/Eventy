
namespace Base.Contracts.DAL.Mappers;


/// <summary>
/// Data Access Layer Mapper Rules Design.
/// </summary>
/// <typeparam name="TLeftObject">First (Left) Object To Be Proceed For Mapping.</typeparam>
/// <typeparam name="TRightObject">Second (Right) Object To Be Proceed For Mapping.</typeparam>
public interface IBaseMapper<TLeftObject, TRightObject> 
    where TLeftObject: class 
    where TRightObject: class
{

    /// <summary>
    /// Method Maps the First (Left) Object to the Second (Right) Object.
    /// </summary>
    /// <param name="inObject">Defines Object To Be Mapped.</param>
    /// <returns>First (Left) Object Mapped To Second (Right) Object.</returns>
    TRightObject? Map(TLeftObject? inObject);

    
    /// <summary>
    /// Method Maps the Second (Right) Object to the First (Left) Object.
    /// </summary>
    /// <param name="inObject">Defines Object To Be Mapped.</param>
    /// <returns>Second (Right) Object Mapped To First (Left) Object.</returns>
    TLeftObject? Map(TRightObject? inObject);

}