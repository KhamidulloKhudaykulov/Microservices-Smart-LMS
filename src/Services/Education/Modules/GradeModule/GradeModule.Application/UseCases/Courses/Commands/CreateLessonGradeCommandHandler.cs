﻿using GradeModule.Domain.Enitites;
using GradeModule.Domain.Repositories;
using Integration.Logic.Abstractions;
using MediatR;
using SharedKernel.Application.Abstractions.Messaging;
using StudentIntegration.Application.InterfaceBridges;

namespace GradeModule.Application.UseCases.Courses.Commands;

public record CreateLessonGradeCommand(
    Guid courseId,
    Guid studentId,
    Guid lessonId,
    Guid assignedBy,
    decimal score,
    string? feedback) : ICommand<Unit>;

public class CreateLessonGradeCommandHandler(
    IGradeRepository _gradeRepository,
    IGradeUnitOfWork _unitOfWork,
    IStudentServiceClient _studentServiceClient,
    ILessonIntegration _lessonintegration)
    : ICommandHandler<CreateLessonGradeCommand, Unit>
{
    public async Task<Result<Unit>> Handle(CreateLessonGradeCommand request, CancellationToken cancellationToken)
    {
        // must check course exist

        if (!await _studentServiceClient.VerifyExistStudentById(request.studentId))
            return Result.Failure<Unit>(new Error(
                code: "User.NotFound",
                message: "This user is not found"));

        if (!await _lessonintegration.ChechExistLessonByIdAsync(request.lessonId))
            return Result.Failure<Unit>(new Error(
                code: "Lesson.NotFound",
                message: "This lesson is not found"));

        var entity = GradeEntity.Create(
            courseId: request.courseId,
            studentId: request.studentId,
            lessonId: request.lessonId,
            examId: null,
            score: request.score,
            assignedBy: request.assignedBy,
            feedback: request.feedback);

        if (entity.IsFailure)
            return Result.Failure<Unit>(entity.Error);

        await _gradeRepository.InsertAsync(entity.Value);
        await _unitOfWork.SaveChangesAsync();

        return Result.Success(Unit.Value);
    }
}
