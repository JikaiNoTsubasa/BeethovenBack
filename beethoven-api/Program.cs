using beethoven_api.Database;
using beethoven_api.Database.DBModels;
using beethoven_api.Global;
using log4net;

var builder = WebApplication.CreateBuilder(args);

ILog log = LogManager.GetLogger(typeof(Program));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDbContext<BeeDBContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllersWithViews(
    o=>o.ModelBinderProviders.Insert(0, new FromJsonBinderProvider())
);

// Disables the string conversions from empty to null
builder.Services.AddMvc().AddMvcOptions(options => options.ModelMetadataDetailsProviders.Add(new CustomMetadataProvider()));

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
}

log.Info("Beethoven API started");
app.Run();