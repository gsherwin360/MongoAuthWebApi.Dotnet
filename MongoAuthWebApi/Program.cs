using MongoAuthWebApi.MongoDb.Authentication;
using MongoAuthWebApi.MongoDb.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMyMongoDbService(builder.Configuration);
builder.Services.AddMyJwtAuthenticationService(builder.Configuration);
builder.Services.AddScoped<IJwtAuthenticationService, MongoAuthenticationService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddControllers();
builder.Services.AddAuthorization();

builder.Services.AddApiSwaggerServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
