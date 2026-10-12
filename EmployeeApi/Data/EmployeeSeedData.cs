using EmployeeApi.Models;

namespace EmployeeApi.Data;

public static class EmployeeSeedData
{
    public static void Initialize(AppDbContext context)
    {
        // Seed only once: if data already exists, do nothing
        if (context.Employees.Any())
        {
            return;
        }

        var departments = new List<Department>
        {
            new() { Id = 1, Name = "Engineering" },
            new() { Id = 2, Name = "Human Resources" },
            new() { Id = 3, Name = "Finance" },
            new() { Id = 4, Name = "Sales" },
            new() { Id = 5, Name = "Marketing" }
        };

        context.Departments.AddRange(departments);
        context.SaveChanges();

        var employees = new List<Employee>
        {
            // Engineering
            new() { Id = 101, Name = "Ananya Rao", Email = "ananya.rao@company.com", Designation = "Engineering Director", Salary = 2400000m, JoinDate = new DateOnly(2018, 3, 12), DepartmentId = 1, ManagerId = null },
            new() { Id = 102, Name = "Karthik Subramanian", Email = "karthik.s@company.com", Designation = "Senior Software Engineer", Salary = 1500000m, JoinDate = new DateOnly(2020, 7, 6), DepartmentId = 1, ManagerId = 101 },
            new() { Id = 103, Name = "Divya Menon", Email = "divya.menon@company.com", Designation = "Software Engineer", Salary = 900000m, JoinDate = new DateOnly(2023, 1, 16), DepartmentId = 1, ManagerId = 102 },
            new() { Id = 104, Name = "Rahul Verma", Email = "rahul.verma@company.com", Designation = "QA Engineer", Salary = 750000m, JoinDate = new DateOnly(2022, 9, 1), DepartmentId = 1, ManagerId = 102 },

            // Human Resources
            new() { Id = 105, Name = "Meera Nair", Email = "meera.nair@company.com", Designation = "HR Manager", Salary = 1300000m, JoinDate = new DateOnly(2019, 5, 20), DepartmentId = 2, ManagerId = null },
            new() { Id = 106, Name = "Anil Kumar", Email = "anil.kumar@company.com", Designation = "HR Executive", Salary = 600000m, JoinDate = new DateOnly(2024, 2, 5), DepartmentId = 2, ManagerId = 105 },

            // Finance
            new() { Id = 107, Name = "Suresh Iyer", Email = "suresh.iyer@company.com", Designation = "Finance Manager", Salary = 1600000m, JoinDate = new DateOnly(2017, 11, 27), DepartmentId = 3, ManagerId = null },
            new() { Id = 108, Name = "Lakshmi Pillai", Email = "lakshmi.pillai@company.com", Designation = "Accountant", Salary = 700000m, JoinDate = new DateOnly(2021, 4, 12), DepartmentId = 3, ManagerId = 107 },

            // Sales
            new() { Id = 109, Name = "Arjun Kapoor", Email = "arjun.kapoor@company.com", Designation = "Sales Manager", Salary = 1400000m, JoinDate = new DateOnly(2019, 8, 19), DepartmentId = 4, ManagerId = null },
            new() { Id = 110, Name = "Sneha Reddy", Email = "sneha.reddy@company.com", Designation = "Sales Executive", Salary = 650000m, JoinDate = new DateOnly(2023, 6, 14), DepartmentId = 4, ManagerId = 109 },
            new() { Id = 111, Name = "Imran Sheikh", Email = "imran.sheikh@company.com", Designation = "Sales Executive", Salary = 620000m, JoinDate = new DateOnly(2024, 10, 3), DepartmentId = 4, ManagerId = 109 },

            // Marketing
            new() { Id = 112, Name = "Pooja Desai", Email = "pooja.desai@company.com", Designation = "Marketing Manager", Salary = 1250000m, JoinDate = new DateOnly(2020, 1, 8), DepartmentId = 5, ManagerId = null },
            new() { Id = 113, Name = "Vikram Singh", Email = "vikram.singh@company.com", Designation = "Content Strategist", Salary = 680000m, JoinDate = new DateOnly(2022, 12, 5), DepartmentId = 5, ManagerId = 112 }
        };

        context.Employees.AddRange(employees);
        context.SaveChanges();
    }
}