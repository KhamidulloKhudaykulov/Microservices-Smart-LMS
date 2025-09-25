using PostService.Infrastructure.Extensions;
using PostService.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();
builder.Services.AddApplication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
