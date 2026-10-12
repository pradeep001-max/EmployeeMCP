using EmployeeApi.DTOs;

namespace EmployeeApi.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto?> GetByIdAsync(int id);
    Task<IEnumerable<EmployeeDto>> SearchAsync(string? name, string? department, string? designation);
    Task<IEnumerable<EmployeeDto>> GetByDepartmentAsync(string department);
}