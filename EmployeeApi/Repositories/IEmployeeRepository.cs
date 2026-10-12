using EmployeeApi.Models;

namespace EmployeeApi.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<List<Employee>> SearchAsync(string? name, string? department, string? designation);
    Task<List<Employee>> GetByDepartmentAsync(string department);
}