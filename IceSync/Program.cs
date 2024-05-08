using IceSync.BL;
using IceSync.BL.Configurations;
using IceSync.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.Configure<AuthorizationSettings>(builder.Configuration.GetSection(nameof(AuthorizationSettings)));
builder.Services.Configure<WorkerSettings>(builder.Configuration.GetSection(nameof(WorkerSettings)));
var databaseSettings = builder.Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>() ?? 
                       throw new ArgumentNullException(nameof(DatabaseSettings), "Database settings are not present");

builder.Services
    .AddBlServices()
    .AddInfrastructure(databaseSettings);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Workflows}/{action=Index}/{id?}");

app.Run();