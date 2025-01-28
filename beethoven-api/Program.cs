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
    var keyByteArray = Encoding.UTF8.GetBytes(AuthConstants.JwtSecret);
    var signinKey = new SymmetricSecurityKey(keyByteArray);
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ClockSkew = TimeSpan.Zero,
 
        ValidateAudience = true,
        ValidAudience = AuthConstants.JwtAudience,
 
        ValidateIssuer = true,
        ValidIssuer = AuthConstants.JwtIssuer,
 
        ValidateLifetime = true,
 
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = signinKey
    };
});

            

var app = builder.Build();

// Disable CORS
app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
        );

app.MapControllers();

// Create default data
using (var scope = app.Services.CreateScope()){
    using var context = scope.ServiceProvider.GetRequiredService<BeeDBContext>();

    List<TicketStatus> statuses = [.. context.TicketStatuses];

    if (!statuses.Any(s=>s.Name!.Equals("New"))){
        context.TicketStatuses.Add(new TicketStatus{Id = 1,Name = "New"});
    }
    if (!statuses.Any(s=>s.Name!.Equals("Assigned"))){
        context.TicketStatuses.Add(new TicketStatus{Id = 2,Name = "Assigned"});
    }
    if (!statuses.Any(s=>s.Name!.Equals("InReview"))){
        context.TicketStatuses.Add(new TicketStatus{Id = 3,Name = "InReview"});
    }
    if (!statuses.Any(s=>s.Name!.Equals("OnHold"))){
        context.TicketStatuses.Add(new TicketStatus{Id = 4,Name = "OnHold"});
    }
    if (!statuses.Any(s=>s.Name!.Equals("Closed (Won't Fix)"))){
        context.TicketStatuses.Add(new TicketStatus{Id = 5,Name = "Closed (Won't Fix)"});
    }
    if (!statuses.Any(s=>s.Name!.Equals("Closed (Fixed)"))){
        context.TicketStatuses.Add(new TicketStatus{Id = 6,Name = "Closed (Fixed)"});
    }
    context.SaveChanges();

    List<SLA> slas = [.. context.SLAs];

    if (!slas.Any(s=>s.Name!.Equals("Low"))){
        context.SLAs.Add(new SLA{Name = "Low"});
    }
    if (!slas.Any(s=>s.Name!.Equals("Medium"))){
        context.SLAs.Add(new SLA{Name = "Medium"});
    }
    if (!slas.Any(s=>s.Name!.Equals("High"))){
        context.SLAs.Add(new SLA{Name = "High"});
    }
    context.SaveChanges();
}

log.Info("Beethoven API started");
app.Run();