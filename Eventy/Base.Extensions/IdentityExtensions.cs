using System.Text;
using System.ComponentModel;
using System.Security.Claims;
using Base.Extensions.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;


namespace Base.Extensions;


/// <summary>
/// Class Extends Identity System Capability.
/// </summary>
public static class IdentityExtensions
{

    /// <summary>
    /// Method Gets Currently Logged In Users' ID Value With Already Defined TKey Value as Guid.
    /// </summary>
    /// <param name="user">Defines Users Claims To Be Processed.</param>
    /// <returns>Currently Logged In Users' ID Value.</returns>
    public static Guid GetUserId(this ClaimsPrincipal user) => GetUserId<Guid>(user);
    
    
    /// <summary>
    /// Method Gets Currently Logged In Users' ID Value.
    /// </summary>
    /// <param name="user">Defines Users Claims To Be Processed.</param>
    /// <typeparam name="TKey">Defines Entity ID Value Type.</typeparam>
    /// <returns>Currently Logged In Users' ID Value.</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public static TKey GetUserId<TKey>(this ClaimsPrincipal user)
    {
        var idClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        // Check If Name Identifier Is Found.
        if (idClaim == null) throw new IdentifierInClaimExistenceException($"Name identifier for given claim is not found.");

        return (TKey) TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(idClaim.Value)!;
    }

    
    /// <summary>
    /// Method Generates JWT For The User Based On The Given Data.
    /// </summary>
    /// <param name="claims">Constitute The Payload Part of a JSON Web Token And Represent
    /// a Set of Information Exchanged Between Two Parties</param>
    /// <param name="key">Defines Party That "Created" The Token And Signed It With Its Private Key.</param>
    /// <param name="issuer">Defines Intended Recipient of The Token.</param>
    /// <param name="audience">Defines Intended Recipient of The Token.</param>
    /// <param name="expirationDateTime">Defines JWT Token Expiration Date and Time.</param>
    /// <returns>Generated JWT Token Value.</returns>
    public static string GenerateJwt(
        IEnumerable<Claim> claims, 
        string key, 
        string issuer, 
        string audience, 
        DateTime expirationDateTime
        )
    {
        
        // Generate Symmetric Security Key.
        var stringKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        // Encode Key.
        var signingCredentials = new SigningCredentials(stringKey, SecurityAlgorithms.HmacSha256);

        // Generate New Token For User Security.
        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: expirationDateTime,
            signingCredentials: signingCredentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}