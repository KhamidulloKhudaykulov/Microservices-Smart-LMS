using Microsoft.EntityFrameworkCore;
using StudentService.Domain.Entities;
using StudentService.Domain.Repositories;
using System.Linq.Expressions;

namespace StudentService.Persistence.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<Student> _students;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
        _students = context.Set<Student>();
    }

    public async Task<Student> InsertAsync(Student student, CancellationToken cancellationToken)
    {
        await _students.AddAsync(student, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return student;
    }

    public async Task<Student> UpdateAsync(Student student, CancellationToken cancellationToken)
    {
        _students.Update(student);
        await _context.SaveChangesAsync(cancellationToken);
        return student;
    }

    public async Task<Student> DeleteAsync(Student student, CancellationToken cancellationToken)
    {
        _students.Remove(student);
        await _context.SaveChangesAsync(cancellationToken);
        return student;
    }

    public IQueryable<Student> SelectAllAsQueryable(Expression<Func<Student, bool>>? predicate = null)
    {
        return predicate is null
            ? _students.AsQueryable()
            : _students.Where(predicate).AsQueryable();
    }

    public async Task<IEnumerable<Student>> SelectAllAsync(Expression<Func<Student, bool>>? predicate = null)
    {
        return predicate is null
            ? await _students.ToListAsync()
            : await _students.Where(predicate).ToListAsync();
    }

    public async Task<Student?> SelectAsync(Expression<Func<Student, bool>>? predicate = null)
    {
        return predicate is null
            ? await _students.FirstOrDefaultAsync()
            : await _students.FirstOrDefaultAsync(predicate);
    }
}