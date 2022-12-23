using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ynventory.Backend.ServiceImplementations.Authentication;
using Ynventory.Backend.ServiceImplementations.Identity;
using Ynventory.Backend.ServiceImplementations.Infrastructure;
using Ynventory.Backend.Services.Authentication;
using Ynventory.Backend.Services.Identity;
using Ynventory.Backend.Services.Infrastructure;
using Ynventory.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Ynventory API",
        Description = "Backend for the Ynventory Magic the Gathering Deck builder application",
        Contact = new OpenApiContact
        {
            Name = "repo",
            Url = new Uri("https://github.com/melvin-suter/Ynventory")
        }
    });
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                });

builder.Services.AddDbContext<YnventoryDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Application"), 
                      x => x.MigrationsAssembly(typeof(YnventoryDbContext).Assembly.FullName));
    options.UseLazyLoadingProxies();
});

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthenticateService, AuthenticateService>();
builder.Services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    initializer.SeedAsync().Wait();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
