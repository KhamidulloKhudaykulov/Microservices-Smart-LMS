using StudentService.Persistence.Extensions;
using StudentService.Application.Extensions;
using StudentService.Infrastructure.Extensions;
using StudentService.Infrastructure.Grpc;
using StudentService.Infrastructure.Grpc.Students.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.MapGrpcService<StudentGrpcServiceHandler>();

app.UseHttpsRedirection();
app.Run();