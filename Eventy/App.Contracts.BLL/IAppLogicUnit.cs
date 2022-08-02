using Base.Contracts.BLL;
using App.Contracts.BLL.Services;


namespace App.Contracts.BLL;


/// <summary>
/// App Specific Business Logic Design.
///  - Defines Services That Should Be Implemented.
/// </summary>
public interface IAppBusinessLogic : ILogicUnit
{
    
    /// <summary>
    /// Business Logic Layer Service Definition For Storing and Manipulating Events.
    /// </summary>
    IEventService Events { get; }
    
}