namespace Base.Contracts.BLL.Mappers;


/// <summary>
/// Business Logic Layer Mapper Rules Design.
/// </summary>
/// <typeparam name="TLeftObject">First (Left) Object To Be Proceed For Mapping.</typeparam>
/// <typeparam name="TRightObject">Second (Right) Object To Be Proceed For Mapping.</typeparam>
public interface IBaseMapper<TLeftObject, TRightObject> : Base.Contracts.DAL.Mappers.IBaseMapper<TLeftObject, TRightObject>
    where TLeftObject : class
    where TRightObject : class { }