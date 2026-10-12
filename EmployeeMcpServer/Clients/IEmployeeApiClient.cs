using EmployeeMcpServer.Models;

namespace EmployeeMcpServer.Clients;

public interface IEmployeeApiClient
{
    Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<EmployeeDto>> SearchAsync(
        string? name,
        string? department,
        string? designation,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<EmployeeDto>> GetByDepartmentAsync(
        string department,
        CancellationToken cancellationToken = default);
}