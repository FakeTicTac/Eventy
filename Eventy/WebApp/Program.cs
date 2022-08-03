using App.BLL;
using App.DAL.EF;
using System.Text;
using WebApp.Helpers;
using App.Contracts.DAL;
using App.Contracts.BLL;
using App.Domain.Identity;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Localization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;


var builder = WebApplication.CreateBuilder(args);
var dbConnectionString = builder.Configuration.GetConnectionString("NpgsqlConnection");

// i18n Configurations.
var defaultCulture = builder.Configuration["DefaultCulture"];
var supportedCultures = builder.Configuration
    .GetSection("SupportedCultures")
    .GetChildren()
    .Select(x => new CultureInfo(x.Value))
    .ToArray();


// Adding Needed Services to Container.

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();
builder.Services.AddScoped<IAppBusinessLogic, AppBusinessLogic>();

builder.Services.AddIdentity<AppUser, AppRole>(options => { options.SignIn.RequireConfirmedAccount = false; })
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// JWT Support 
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services
    .AddAuthentication()
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ClockSkew = TimeSpan.Zero
        };
    });


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password Settings Configurations.
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 6;
    
    // Lockout Settings Configurations.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;
    
    // User Settings Configurations.
    options.User.RequireUniqueEmail = true;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsAllowAll",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin();
            policyBuilder.AllowAnyHeader();
            policyBuilder.AllowAnyMethod();
        });
});

builder.Services.AddApiVersioning(options =>
    {
        options.ReportApiVersions = true;
        
        // In Case Of No Explicit Version
        options.DefaultApiVersion = new ApiVersion(1, 0);
    }
);

builder.Services.AddVersionedApiExplorer( options => options.GroupNameFormat = "'v'VVV" );
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // DateTime and Currency Support.
    options.SupportedCultures = supportedCultures;
        
    // UI Translation Strings.
    options.SupportedUICultures = supportedCultures;
        
    // If Nothing is Found Use Default Culture.
    options.DefaultRequestCulture = new RequestCulture(defaultCulture, defaultCulture);
    options.SetDefaultCulture(defaultCulture);
        
    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        // Order Is Important. (Evaluated By Ordering)
        new QueryStringRequestCultureProvider()
    };
});

builder.Services.AddAutoMapper(
    typeof(App.DAL.DTO.MappingProfiles.AutoMapperProfile), 
    typeof(App.BLL.DTO.MappingProfiles.AutoMapperProfile),
    typeof(Api.DTO.v1.MappingProfiles.AutoMapperProfile));


var app = builder.Build();

// Seed Initial Data Into Application.
AppDataHelper.SetupAppData(app, app.Environment, app.Configuration);


// Configuring The HTTP Request Pipeline.


if (app.Environment.IsDevelopment()) app.UseMigrationsEndPoint();
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseCors("CorsAllowAll");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>(); 
    foreach ( var description in provider.ApiVersionDescriptions )
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant() 
        );
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseRequestLocalization(options: app.Services.GetService<IOptions<RequestLocalizationOptions>>()?.Value!);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();


/// <summary>
/// Definition For Testing Purposes.
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program { }


























