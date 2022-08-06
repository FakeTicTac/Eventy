using System.Net;
using Base.Extensions;
using App.Contracts.BLL;
using System.Diagnostics;
using Api.DTO.v1.DTO.Errors;
using System.Security.Claims;
using Api.DTO.v1.DTO.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

using AppUser = App.Domain.Identity.AppUser;


namespace WebApp.ApiControllers.Identity;


/// <summary>
/// API Controller For Account Manipulations And Data Transfer.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/identity/[controller]/[action]")]
public class AccountController : ControllerBase
{ 
    
    /// <summary>
    /// Defines Connection API For User Sign In Operations.
    /// </summary>
    private readonly SignInManager<AppUser> _signInManager;
    
    /// <summary>
    /// Defines Connection API For User Managing Operations.
    /// </summary>
    private readonly UserManager<AppUser> _userManager;

    /// <summary>
    /// Defines Logging Connection.
    /// </summary>
    private readonly ILogger<AccountController> _logger;
    
    /// <summary>
    /// Defines Connection To Application Configurations.
    /// </summary>
    private readonly IConfiguration _configuration;
    
    /// <summary>
    /// Defines Connection To Random Generator.
    /// </summary>
    private readonly Random _rnd = new();

    /// <summary>
    /// Business Logic Layer Connection Definition.
    /// </summary>
    private readonly IAppBusinessLogic _bll;
    

    /// <summary>
    /// Basic Account Controller Constructor.
    /// </summary>
    /// <param name="signInManager">Defines Connection API For User Sign In Operations.</param>
    /// <param name="userManager">Defines Connection API For User Managing Operations.</param>
    /// <param name="logger">Defines Logging Connection.</param>
    /// <param name="configuration">Defines Connection To Application Configurations.</param>
    /// <param name="bll">Defines Business Logic Layer</param>
    public AccountController(
        SignInManager<AppUser> signInManager, 
        UserManager<AppUser> userManager, 
        ILogger<AccountController> logger, 
        IConfiguration configuration,
        IAppBusinessLogic bll
        )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _configuration = configuration;
        _bll = bll;
    }
    
    
    /// <summary>
    /// Method Process User Login Into System.
    ///  - Generates JWT And Send It Back To User.
    ///  - Authorize Type: Bearer
    /// </summary>
    /// <param name="login">Supply Unique Identifier and Password</param>
    /// <returns>Generated JWT For User Authorization.</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Jwt>> LogIn([FromBody] Login login)
    {
        // Verify Login Identifier (Search For Email or Username)
        
        // Trying To Verify By Email and Username.
        var user = await _userManager.FindByEmailAsync(login.LoginIdentifier) ?? 
                   await _userManager.FindByNameAsync(login.LoginIdentifier);
        
        if (user == null)
        {
            _logger.LogWarning("API login attempt failed, unique identifier {} not found", login.LoginIdentifier);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("Problem with Login credentials.");
        }
        
        
        // Try To Login Using Provided Identifier and Password.
        var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        
        if (!result.Succeeded)
        {
            _logger.LogWarning("API login attempt failed, provided password is incorrect for User with Identifier {}", 
                login.LoginIdentifier);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("Problem with Login credentials.");
        }

        
        // Try To Generate Claims For Specific User.
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

        if (claimsPrincipal == null)
        {
            _logger.LogWarning("API cannot generate claims principal for User with Identifier {}", 
                login.LoginIdentifier);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("Problem with Login credentials.");
        }

        // Handle Old Tokens.
        OldRefreshTokenHandler(user);
        
        // Create New Tokens And Save It Into System.
        var refreshToken = new App.BLL.DTO.Identity.RefreshToken
        {
            AppUserId = user.Id,
            Value = Guid.NewGuid().ToString(),
            ExpirationDateTime = DateTime.UtcNow.AddDays(7)
        };

        _bll.RefreshTokens.Add(refreshToken);
        await _bll.SaveChangesAsync();
        
        // Generate JWT Token For User.
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes")));
        
        
        // Send JWT Token Data Transfer Object From Backend.
        return Ok(new Jwt
        {
            AccessTokenValue = jwt,
            RefreshTokenValue = refreshToken.Value,
            Username = user.UserName
        });
    }
    
    
    /// <summary>
    /// Method Process User Registration Into System.
    ///  - Generates JWT And Send It Back To User.
    ///  - Authorize Type: Bearer
    /// </summary>
    /// <param name="register">Supply Unique Identifiers and Password</param>
    /// <returns>Generated JWT For User Authorization.</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Jwt>> Register([FromBody] Register register)
    {
        // Verify Register Identifier (Search For Email or Username if Already Registered)
        
        // Trying To Verify By Email.
        var user = await _userManager.FindByEmailAsync(register.Email);

        if (user != null)
        {
            _logger.LogWarning("API register attempt failed. Email {} is already registered", register.Email);
            var errorResponse = new RestApiErrorResponse
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "API register attempt error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Errors =
                {
                    ["email"] = new List<string> { "Provided email already registered." }
                }
            };

            return BadRequest(errorResponse);
        }
        
        // Trying To Verify By Username.
        user = await _userManager.FindByNameAsync(register.UserName);
        
        if (user != null)
        {
            _logger.LogWarning("API register attempt failed. Username {} is already registered", register.UserName);
            var errorResponse = new RestApiErrorResponse
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                Title = "API register attempt error",
                Status = HttpStatusCode.BadRequest,
                TraceId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Errors =
                {
                    ["username"] = new List<string> { "Provided username already registered." }
                }
            };

            return BadRequest(errorResponse);
        }
        
        
        // Try To Create User Based on Provided Data.
        
        user = new AppUser
        {
            Email = register.Email,
            UserName = register.UserName,
        };
        
        var result = await _userManager.CreateAsync(user, register.Password);
        
        // Check If Operation Process Was Successful.
        if (!result.Succeeded) return BadRequest(result);
  
        
        // Try to Find Account By Registration Email.
        user = await _userManager.FindByEmailAsync(user.Email);

        if (user == null)
        {
            _logger.LogWarning("API login attempt after registration failed, unique identifier {} not found", register.Email);
            return BadRequest("Account was not found after registration process.");
        }
        
        
        // Try To Generate Claims For Specific User.
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

        if (claimsPrincipal == null)
        {
            _logger.LogWarning("API cannot generate claims principal for User with Identifier {}", 
                register.UserName);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("Problem with Login credentials.");
        }

        // Create New Tokens And Save It Into System.
        var refreshToken = new App.BLL.DTO.Identity.RefreshToken
        {
            AppUserId = user.Id,
            Value = Guid.NewGuid().ToString(),
            ExpirationDateTime = DateTime.UtcNow.AddDays(7)
        };

        _bll.RefreshTokens.Add(refreshToken);
        await _bll.SaveChangesAsync();
        
        // Generate JWT Token For User.
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:Issuer"],
            _configuration["JWT:Issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes")));
        
        
        // Send JWT Token Data Transfer Object From Backend.
        return Ok(new Jwt
        {
            AccessTokenValue = jwt,
            RefreshTokenValue = refreshToken.Value,
            Username = user.UserName
        });
    }
    
    
    /// <summary>
    /// Method Process User Token Refreshment System.
    ///  - Generates JWT And Send It Back To User.
    ///  - Authorize Type: Bearer
    /// </summary>
    /// <param name="tokenRefreshment">Supply Access And Refresh Tokens.</param>
    /// <returns>Generated Access And Refresh Tokens.</returns>
    [HttpPost]
    [Produces("application/json")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Jwt>> RefreshToken([FromBody] TokenRefreshment tokenRefreshment)
    {
        
        // Try To Get User Info From JWT Access Token.
        JwtSecurityToken accessToken;
        
        try
        {
            accessToken = new JwtSecurityTokenHandler().ReadJwtToken(tokenRefreshment.AccessToken);
        }
        catch (Exception)
        {
            return BadRequest("Can't parse token.");
        }
        
        // Validate Token Signature.
        var userEmail = accessToken
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Email)?
            .Value;

        if (userEmail == null) return BadRequest("No email in jwt token.");
        
        // Get User And Tokens

        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user == null) return NotFound($"User with email {userEmail} not found");

        var oldRefreshTokens = (await _bll.RefreshTokens.GetAllAsync(user.Id))
            .Where(x => x.Value!.Equals(tokenRefreshment.RefreshToken) && x.ExpirationDateTime > DateTime.UtcNow)
            .ToList();
        
        if (oldRefreshTokens.Equals(null)) return Problem("RefreshTokens collection is null");

        if (oldRefreshTokens.Count == 0) 
            return Problem("RefreshTokens collection is empty, no valid refresh tokens found");

        if (oldRefreshTokens.Count > 1) return Problem("More than one valid refresh token found.");
        
        // New JWT
        // Get Claims based user
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

        if (claimsPrincipal == null)
        {
            _logger.LogWarning("WebApi, Cannot Get Claims Principal for User {}", userEmail);
            await Task.Delay(_rnd.Next(100, 1000));
            return NotFound("User/Password problem");
        }

        // Remove Old Token.
        _bll.RefreshTokens.Remove(oldRefreshTokens[0].Id);

        // Create New Tokens And Save It Into System.
        var newRefreshToken = new App.BLL.DTO.Identity.RefreshToken
        {
            AppUserId = user.Id,
            Value = Guid.NewGuid().ToString(),
            ExpirationDateTime = DateTime.UtcNow.AddDays(7)
        };

        _bll.RefreshTokens.Add(newRefreshToken);
        await _bll.SaveChangesAsync();
        
        
        // Generate JWT
        var jwt = IdentityExtensions.GenerateJwt(
            claimsPrincipal.Claims,
            _configuration["JWT:Key"],
            _configuration["JWT:issuer"],
            _configuration["JWT:issuer"],
            DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes")));

        
        // Send JWT Token Data Transfer Object From Backend.
        return Ok(new Jwt
        {
            AccessTokenValue = jwt,
            RefreshTokenValue = newRefreshToken.Value,
            Username = user.UserName
        });
    }
    
    
    // Helping Methods.


    /// <summary>
    /// Method Deletes All Refresh Tokens That Expire.
    /// </summary>
    /// <param name="user">Defines User To Remove Old Refresh Tokens.</param>
    public async void OldRefreshTokenHandler(AppUser user)
    {
        // Get All Tokens Related To User.
        var refreshTokens = (await _bll.RefreshTokens.GetAllAsync(user.Id))
            .Where(x => x.AppUserId.Equals(user.Id));

        foreach (var refreshToken in refreshTokens)
        {
            if (refreshToken.ExpirationDateTime < DateTime.UtcNow) _bll.RefreshTokens.Remove(refreshToken);
        }
    }
}