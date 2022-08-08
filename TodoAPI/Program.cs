using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Extensions;
using TodoAPI.Middlewares;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddSwagger(builder.Configuration);
builder.Services.AddHttpContextAccessor();

WebApplication? app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(policy =>
                policy
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200")
            );

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapFallbackToController("Index", "Fallback");

// make sure db is created
using IServiceScope scope = app.Services.CreateScope();
IServiceProvider services = scope.ServiceProvider;
DataContext context = services.GetRequiredService<DataContext>();
await context.Database.MigrateAsync();

app.Run();
