using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Global;
using beethoven_api.Global.Engine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using log4net;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using beethoven_api.Global.Token;

var builder = WebApplication.CreateBuilder(args);

ILog log = LogManager.GetLogger(typeof(Program));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<BeeEngine>();
builder.Services.AddDbContext<BeeDBContext>();
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
/*
using (var scope = app.Services.CreateScope())
{
    using var context = scope.ServiceProvider.GetRequiredService<BeeDBContext>();

    context.SaveChanges();

}
*/
log.Info("Beethoven API started");
app.Run();