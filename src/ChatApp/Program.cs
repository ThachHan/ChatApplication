using ChatApp.Configuration;
using ChatApp.Data;
using ChatApp.Domain.Entities;
using ChatApp.Hubs;
using ChatApp.Persistence.DbManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .Services
    .Scan(
        selector => selector
            .FromAssemblies(
                ChatApp.Domain.AssemblyReference.Assembly,
                ChatApp.Persistence.AssemblyReference.Assembly,
                ChatApp.Core.AssemblyReference.Assembly)
            .AddClasses(false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'IdentityContextConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services
    .AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddUserStore<UserStore<AppUser, AppRole, AppDbContext, Guid>>()
    .AddRoleStore<RoleStore<AppRole, AppDbContext, Guid>>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddAuthentication();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 2;
});

builder.Services.AddSignalR();
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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseExceptionHandler();
app.MigrateDatabase();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoint =>
{
    endpoint.MapControllers();
    endpoint.MapRazorPages();
    endpoint.MapHub<ChatHub>("/chatHub");
});

await app.RunAsync();
