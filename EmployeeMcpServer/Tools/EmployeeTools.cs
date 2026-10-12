using System.ComponentModel;
using System.Text.Json;
using EmployeeMcpServer.Clients;
using EmployeeMcpServer.Models;
using ModelContextProtocol.Server;

namespace EmployeeMcpServer.Tools;

[McpServerToolType]
public class EmployeeTools
{
    private const string UnavailableMessage =
        "The employee service is unavailable. Check that EmployeeApi is running.";

    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    private readonly IEmployeeApiClient _client;

    public EmployeeTools(IEmployeeApiClient client)
    {
        _client = client;
    }

    [McpServerTool(Name = "get_employee_by_id")]
    [Description("Gets the full details of one employee (name, email, designation, department, salary, join date and manager id) by employee id. Use it when the user gives an employee id, or to look up a manager using a manager id.")]
    public async Task<string> GetEmployeeById(
        [Description("The numeric employee id, for example 101")] int id,
        CancellationToken cancellationToken)
    {
        try
        {
            var employee = await _client.GetByIdAsync(id, cancellationToken);

            return employee is null
                ? $"No employee found with id {id}."
                : JsonSerializer.Serialize(employee, JsonOptions);
        }
        catch (HttpRequestException)
        {
            return UnavailableMessage;
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return UnavailableMessage;
        }
    }

    [McpServerTool(Name = "search_employees")]
    [Description("Searches employees by partial name, department or designation. All filters are optional and can be combined. Use it when the user asks to find someone by name or role.")]
    public async Task<string> SearchEmployees(
        [Description("Part of the employee name, for example 'ananya'")] string? name,
        [Description("Part of the department name, for example 'eng'")] string? department,
        [Description("Part of the job title, for example 'manager'")] string? designation,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name) &&
            string.IsNullOrWhiteSpace(department) &&
            string.IsNullOrWhiteSpace(designation))
        {
            return "Provide at least one of name, department or designation to search.";
        }

        try
        {
            var employees = await _client.SearchAsync(name, department, designation, cancellationToken);
            return Format(employees, "No employees matched the search.");
        }
        catch (HttpRequestException)
        {
            return UnavailableMessage;
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return UnavailableMessage;
        }
    }

    [McpServerTool(Name = "list_employees_by_department")]
    [Description("Lists all employees in one department. Departments are Engineering, Human Resources, Finance, Sales and Marketing. Use it when the user asks who works in a department.")]
    public async Task<string> ListEmployeesByDepartment(
        [Description("The exact department name, for example 'Sales'")] string department,
        CancellationToken cancellationToken)
    {
        try
        {
            var employees = await _client.GetByDepartmentAsync(department, cancellationToken);
            return Format(employees, $"No employees found in the '{department}' department.");
        }
        catch (HttpRequestException)
        {
            return UnavailableMessage;
        }
        catch (TaskCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return UnavailableMessage;
        }
    }

    private static string Format(IReadOnlyList<EmployeeDto> employees, string emptyMessage)
    {
        return employees.Count == 0
            ? emptyMessage
            : JsonSerializer.Serialize(employees, JsonOptions);
    }
}