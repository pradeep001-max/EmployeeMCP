namespace EmployeeApi.DTOs;

public record EmployeeDto(
    int Id,
    string Name,
    string Email,
    string Designation,
    string DepartmentName,
    decimal Salary,
    DateOnly JoinDate,
    int? ManagerId
);