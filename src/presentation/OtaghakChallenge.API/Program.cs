
using OtaghakChallenge.Persentation;
using OtaghakChallenge.Persistence.Infrastructure;

using static OtaghakChallenge.Application.ServiceInstaller;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddAuthenticate(builder.Configuration);
builder.Services.AddSwaggerConfig(builder.Configuration);

//InfraStructure
builder.Services.AddAppDBContext(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);


builder.Services.AddApiVersion(builder.Configuration);







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
