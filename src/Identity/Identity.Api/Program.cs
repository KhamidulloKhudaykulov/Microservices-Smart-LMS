using Identity.Api.Middlewares;
using Identity.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
   
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseSkipAuthPath();

app.MapControllers();

app.UseCors("AllowAll");

app.Run();
