using beethoven_api.Database;
using beethoven_api.Global;
using beethoven_api.Global.Engine;
using log4net;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using beethoven_api.Global.Token;
using beethoven_api.Managers;
using beethoven_api.JobManagers;

var builder = WebApplication.CreateBuilder(args);

// Add log4net
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net("log4net.config");

// Exception handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails(); // Required

ILog log = LogManager.GetLogger(typeof(Program));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<BeeEngine>();
builder.Services.AddDbContext<BeeDBContext>();

// Add managers
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<AuthManager>();
builder.Services.AddScoped<DocumentManager>();
builder.Services.AddScoped<FileManager>();
builder.Services.AddScoped<GlobalParameterManager>();
builder.Services.AddScoped<ProjectManager>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllersWithViews(
    o=>o.ModelBinderProviders.Insert(0, new FromJsonBinderProvider())
);

// Disables the string conversions from empty to null
builder.Services.AddMvc().AddMvcOptions(options => options.ModelMetadataDetailsProviders.Add(new CustomMetadataProvider()));


// Configure json
builder.Services.AddControllers().AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        OverrideSpecifiedNames = false
                    }
                };
                o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                o.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                o.SerializerSettings.Converters.Add(new UtcDateTimeJsonConverter());

            });

// Add bearer support
builder.Services.AddAuthentication(options =>{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>{
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConstants.JwtSecret));
    //var signinKey = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //ClockSkew = TimeSpan.Zero,

        ValidateAudience = true,
        ValidAudience = AuthConstants.JwtAudience,

        ValidateIssuer = true,
        ValidIssuer = AuthConstants.JwtIssuer,

        //ValidateLifetime = true,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        LogValidationExceptions = true,
        LogTokenId = true
    };
    options.MapInboundClaims = false;
});

            

var app = builder.Build();

// Disable CORS
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Create default data
using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<BeeDBContext>();
BeeInitializer.InitGlobalParameters(context);
BeeInitializer.CreateDefaultUser(context);

log.Info("Beethoven API started");
app.Run();