using System.Net;
using System.Net.Http.Json;
using EmployeeMcpServer.Models;

namespace EmployeeMcpServer.Clients;

public class EmployeeApiClient : IEmployeeApiClient
{
    private readonly HttpClient _http;

    public EmployeeApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<EmployeeDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await _http.GetAsync($"api/employees/{id}", cancellationToken);

        // The API answers 404 when the employee doesn't exist: that is a normal "not found", not an error
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EmployeeDto>(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyList<EmployeeDto>> SearchAsync(
        string? name,
        string? department,
        string? designation,
        CancellationToken cancellationToken = default)
    {
        var filters = new List<string>();

        if (!string.IsNullOrWhiteSpace(name))
            filters.Add($"name={Uri.EscapeDataString(name)}");

        if (!string.IsNullOrWhiteSpace(department))
            filters.Add($"department={Uri.EscapeDataString(department)}");

        if (!string.IsNullOrWhiteSpace(designation))
            filters.Add($"designation={Uri.EscapeDataString(designation)}");

        var url = filters.Count > 0
            ? $"api/employees/search?{string.Join("&", filters)}"
            : "api/employees/search";

        var result = await _http.GetFromJsonAsync<List<EmployeeDto>>(url, cancellationToken);
        return result ?? new List<EmployeeDto>();
    }

    public async Task<IReadOnlyList<EmployeeDto>> GetByDepartmentAsync(
        string department,
        CancellationToken cancellationToken = default)
    {
        var url = $"api/employees/department/{Uri.EscapeDataString(department)}";

        var result = await _http.GetFromJsonAsync<List<EmployeeDto>>(url, cancellationToken);
        return result ?? new List<EmployeeDto>();
    }
}