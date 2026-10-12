using EmployeeApi.DTOs;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    // GET api/employees
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAll()
    {
        var employees = await _service.GetAllAsync();
        return Ok(employees);
    }

    // GET api/employees/101
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(EmployeeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeDto>> GetById(int id)
    {
        var employee = await _service.GetByIdAsync(id);

        if (employee is null)
        {
            return NotFound(new { message = $"No employee found with id {id}" });
        }

        return Ok(employee);
    }

    // GET api/employees/search?name=ananya&department=eng&designation=director
    [HttpGet("search")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Search(
        [FromQuery] string? name,
        [FromQuery] string? department,
        [FromQuery] string? designation)
    {
        var employees = await _service.SearchAsync(name, department, designation);
        return Ok(employees);
    }

    // GET api/employees/department/Engineering
    [HttpGet("department/{department}")]
    [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetByDepartment(string department)
    {
        var employees = await _service.GetByDepartmentAsync(department);
        return Ok(employees);
    }
}