using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TeacherService.Domain.Aggregates;
using TeacherService.Domain.Repositories;

namespace TeacherService.Persistence.Repositories;

public class TeacherRepository : ITecherRepository
{
    private readonly TeacherServiceDbContext _context;
    private readonly DbSet<Teacher> _teachers;

    public TeacherRepository(TeacherServiceDbContext context)
    {
        _context = context;
        _teachers = _context.Set<Teacher>();
    }

    public async Task<Teacher> InsertAsync(Teacher teacher)
    {
        await _teachers.AddAsync(teacher);
        return teacher;
    }

    public Task<Teacher> UpdateAsync(Teacher teacher)
    {
        _teachers.Update(teacher);
        return Task.FromResult(teacher);
    }

    public Task<Teacher> DeleteAsync(Teacher teacher)
    {
        _teachers.Remove(teacher);
        return Task.FromResult(teacher);
    }

    public async Task<Teacher?> SelectByIdAsync(Guid id)
    {
        return await _teachers.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Teacher?> SelectAsync(Expression<Func<Teacher, bool>> predicate)
    {
        return await _teachers.FirstOrDefaultAsync(predicate);
    }

    public async Task<IEnumerable<Teacher>> SelectAllAsync(Expression<Func<Teacher, bool>>? predicate = null)
    {
        if (predicate is null)
            return await _teachers.ToListAsync();

        return await _teachers.Where(predicate).ToListAsync();
    }

    public IQueryable<Teacher> SelectAsQueryable(Expression<Func<Teacher, bool>>? predicate = null)
    {
        if (predicate is null)
            return _teachers.AsQueryable();

        return _teachers.Where(predicate).AsQueryable();
    }
}
