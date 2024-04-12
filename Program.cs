using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using UserJwt.Context;
using UserJwt.Middlewares;
using UserJwt.Repositories;
using UserJwt.Services.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MySqlContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version("8.0.36"))
    )
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(document =>
{
    document.EnableAnnotations();
    document.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseWhen(
    ctx => !ctx.Request.Path.StartsWithSegments("/api/v1/auth"),
    appBuilder =>
    {
        appBuilder.UseMiddleware<AuthMiddleware>();
    }
);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


