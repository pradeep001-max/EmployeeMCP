using EmployeeApi.Data;
using EmployeeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<Employee>> SearchAsync(string? name, string? department, string? designation)
    {
        var query = _context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            var term = name.Trim().ToLower();
            query = query.Where(e => e.Name.ToLower().Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(department))
        {
            var term = department.Trim().ToLower();
            query = query.Where(e => e.Department!.Name.ToLower().Contains(term));
        }

        if (!string.IsNullOrWhiteSpace(designation))
        {
            var term = designation.Trim().ToLower();
            query = query.Where(e => e.Designation.ToLower().Contains(term));
        }

        return await query
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public async Task<List<Employee>> GetByDepartmentAsync(string department)
    {
        var term = department.Trim().ToLower();

        return await _context.Employees
            .AsNoTracking()
            .Include(e => e.Department)
            .Where(e => e.Department!.Name.ToLower() == term)
            .OrderBy(e => e.Id)
            .ToListAsync();
    }
}