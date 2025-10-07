using Education.Api.Extensions;
using CourseModule.Application.Extensions;
using CourseModule.Infrastructure.Extensions;
using StudentIntegration.Infrastructure;
using CourseModule.Orchestration.Extensions;
using GradeModule.Infrastructure.Extensions;
using GradeModule.Orchestration.Extensions;
using LessonModule.Infrastructure.Extensions;
using LessonModule.Application.Extensions;
using GradeModule.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStudentIntegrationInfrastructure(builder.Configuration);

builder.Services.AddCourseModuleApplication();
builder.Services.AddCourseModuleOrchestration();
builder.Services.AddCourseModuleInfrastructure(builder.Configuration);

builder.Services.AddGradeModuleApplication();
builder.Services.AddGradeModuleInfrastructure(builder.Configuration);
builder.Services.AddGradeModuleOrchestration();

builder.Services.AddLessonModuleApplication();
builder.Services.AddLessonModuleInfrastructure(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddModules(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();