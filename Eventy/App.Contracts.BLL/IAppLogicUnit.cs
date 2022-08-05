using Base.Contracts.BLL;
using App.Contracts.BLL.Services;
using App.Contracts.BLL.Services.Identity;


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
    
    
    // Identity Related Only
    
    
    /// <summary>
    /// Business Logic Layer Service Definition For Storing Refresh Tokens.
    /// </summary>
    IRefreshTokenService RefreshTokens { get; }
    
}