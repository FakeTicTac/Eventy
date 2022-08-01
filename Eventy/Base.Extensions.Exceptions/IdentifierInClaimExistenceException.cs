namespace Base.Extensions.Exceptions;


/// <summary>
/// Claim Existence Exception In Database Layer.
///  - Thrown If Claims Doesn't Exist For Given User.
/// Class Defines Exception For Claim Existence In Database Layer Issues.
/// </summary>
public class IdentifierInClaimExistenceException : Exception
{
    
    /// <summary>
    /// Basic Parameterless Constructor. 
    /// </summary>
    public IdentifierInClaimExistenceException() { }
    
    
    /// <summary>
    /// Basic Constructor With Message To Be Displayed To The System. 
    /// </summary>
    /// <param name="message">Defines Message To Be Displayed To The System.</param>
    public IdentifierInClaimExistenceException(string message) : base(message) { }
    
    
    /// <summary>
    /// Basic Constructor That Allows to Track Back Original Exception With Message To Be Displayed To The System.
    /// </summary>
    /// <param name="message">Defines Message To Be Displayed To The System.</param>
    /// <param name="inner">Defines Tracking Back of Original Exception</param>
    public IdentifierInClaimExistenceException(string message, Exception inner) : base(message, inner) { }
    
    
    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client.
    /// <summary>
    /// Constructor is Needed For Serialization When an Exception Propagates From a Remoting Server To The Client.
    /// </summary>
    /// <param name="info">Stores All Data Needed To Serialize and Deserialize an Object.</param>
    /// <param name="context">Defines The Source and Destination of a Given Stream.</param>
    protected IdentifierInClaimExistenceException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    
}