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
    };
    options.TokenValidationParameters.LogValidationExceptions = true;
    options.TokenValidationParameters.LogTokenId = true;
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
using (var scope = app.Services.CreateScope()){
    using var context = scope.ServiceProvider.GetRequiredService<BeeDBContext>();

    // Create ticket statuses
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

    // Create SLAs
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

    // Create Ticket Types
    List<TicketType> types = [.. context.TicketTypes];

    if (!types.Any(s=>s.Name!.Equals("Incident"))){
        context.TicketTypes.Add(new TicketType{Id = 1, Name = "Incident", Description = "An unplanned interruption to an IT service."});
    }
    if (!types.Any(s=>s.Name!.Equals("Problem"))){
        context.TicketTypes.Add(new TicketType{Id = 2, Name = "Problem", Description = "The underlying cause of one or more incidents."});
    }
    if (!types.Any(s=>s.Name!.Equals("Change Request"))){
        context.TicketTypes.Add(new TicketType{Id = 3, Name = "Change Request", Description = "A formal proposal for a change to IT services."});
    }
    if (!types.Any(s=>s.Name!.Equals("Service Request"))){
        context.TicketTypes.Add(new TicketType{Id = 4, Name = "Service Request", Description = "A request for information, advice, or access to a service."});
    }

    // Create priorities
    List<Priority> priorities = [.. context.Priorities];

    if (!priorities.Any(s=>s.Name!.Equals("High"))){
        context.Priorities.Add(new Priority{Id = 1, Name = "High"});
    }
    if (!priorities.Any(s=>s.Name!.Equals("Medium"))){
        context.Priorities.Add(new Priority{Id = 2, Name = "Medium"});
    }
    if (!priorities.Any(s=>s.Name!.Equals("Low"))){
        context.Priorities.Add(new Priority{Id = 3, Name = "Low"});
    }
    context.SaveChanges();
}

log.Info("Beethoven API started");
app.Run();