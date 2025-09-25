using StudentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Domain.Repositories;

public interface IStudentRepository
{
    Task<Student> InsertAsync(Student user, CancellationToken cancellationToken);
    Task<Student> UpdateAsync(Student user, CancellationToken cancellationToken);
    Task<Student> DeleteAsync(Student user, CancellationToken cancellationToken);
    Task<Student?> SelectAsync(Expression<Func<Student, bool>>? predicate = null);
    Task<IEnumerable<Student>> SelectAllAsync(Expression<Func<Student, bool>>? predicate = null);
    IQueryable<Student> SelectAllAsQueryable(Expression<Func<Student, bool>>? predicate = null);
}
