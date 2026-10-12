using AutoMapper;
using EmployeeApi.DTOs;
using EmployeeApi.Models;

namespace EmployeeApi.Mappings;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>();
    }
}