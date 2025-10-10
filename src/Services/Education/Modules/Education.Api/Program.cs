using CourseModule.Application.Extensions;
using CourseModule.Infrastructure.Extensions;
using CourseModule.Orchestration.Extensions;
using Education.Api.Extensions;
using GradeModule.Application.Extensions;
using GradeModule.Infrastructure.Extensions;
using GradeModule.Orchestration.Extensions;
using HomeworkModule.Application.Extensions;
using HomeworkModule.Infrastructure.Extensions;
using LessonModule.Application.Extensions;
using LessonModule.Infrastructure.Extensions;
using ScheduleModule.Application.Extensions;
using ScheduleModule.Infrastructure.Extensions;
using ScheduleModule.Orchestration.Extensions;
using StudentIntegration.Infrastructure;

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

builder.Services.AddHomeworkModuleApplication();
builder.Services.AddHomeworkModuleInfrastructure(builder.Configuration);

builder.Services.AddScheduleModuleApplication();
builder.Services.AddScheduleModuleInfrastructure(builder.Configuration);
builder.Services.AddScheduleModuleOrchestration();

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