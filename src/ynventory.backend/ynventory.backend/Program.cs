using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using System.Reflection;
using System.Text.Json;
using Ynventory.Backend.ServiceImplementations.Authentication;
using Ynventory.Backend.ServiceImplementations.Data;
using Ynventory.Backend.ServiceImplementations.Identity;
using Ynventory.Backend.ServiceImplementations.Infrastructure;
using Ynventory.Backend.Services.Authentication;
using Ynventory.Backend.Services.Data;
using Ynventory.Backend.Services.Identity;
using Ynventory.Backend.Services.Infrastructure;
using Ynventory.Backend.Util;
using Ynventory.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.Converters.Add(new JsonEnumConverterFactory());
});


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

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                });

builder.Services.AddDbContext<YnventoryDbContext>(options =>
{
    var section = builder.Configuration.GetSection("EnvironmentVariables");

    var connectionStringBuilder = new NpgsqlConnectionStringBuilder
    {
        Host = Environment.GetEnvironmentVariable(section.GetValue<string>("POSTGRES_HOST")!),
        Database = Environment.GetEnvironmentVariable(section.GetValue<string>("POSTGRES_DB")!),
        Username = Environment.GetEnvironmentVariable(section.GetValue<string>("POSTGRES_USER")!),
        Password = Environment.GetEnvironmentVariable(section.GetValue<string>("POSTGRES_PASSWORD")!)
    };

    options.UseNpgsql(connectionStringBuilder.ConnectionString,
                      x => x.MigrationsAssembly(typeof(YnventoryDbContext).Assembly.FullName));
    options.UseLazyLoadingProxies();
});

//Add application services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAuthenticateService, AuthenticateService>();
builder.Services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
builder.Services.AddTransient<ICollectionService, CollectionService>();
builder.Services.AddTransient<IScryfallClient, ScryfallClient>();
builder.Services.AddTransient<ICardMetadataService, CardMetadataService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

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
