namespace Base.DAL.EF.Exceptions;


/// <summary>
/// Data Existence Exception In Database Layer.
///  - Thrown If Data Doesn't Exits In The Database.
/// </summary>
public class DataExistenceException : Exception
{
    
    /// <summary>
    /// Basic Parameterless Constructor. 
    /// </summary>
    public DataExistenceException() { }
    
    
    /// <summary>
    /// Basic Constructor With Message To Be Displayed To The System. 
    /// </summary>
    /// <param name="message">Defines Message To Be Displayed To The System.</param>
    public DataExistenceException(string message) : base(message) { }
    
    
    /// <summary>
    /// Basic Constructor That Allows to Track Back Original Exception With Message To Be Displayed To The System.
    /// </summary>
    /// <param name="message">Defines Message To Be Displayed To The System.</param>
    /// <param name="inner">Defines Tracking Back of Original Exception</param>
    public DataExistenceException(string message, Exception inner) : base(message, inner) { }
    
    
    // A constructor is needed for serialization when an
    // exception propagates from a remoting server to the client.
    /// <summary>
    /// Constructor is Needed For Serialization When an Exception Propagates From a Remoting Server To The Client.
    /// </summary>
    /// <param name="info">Stores All Data Needed To Serialize and Deserialize an Object.</param>
    /// <param name="context">Defines The Source and Destination of a Given Stream.</param>
    protected DataExistenceException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    
}