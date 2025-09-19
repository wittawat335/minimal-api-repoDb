using Microsoft.EntityFrameworkCore;
using minimal_api_repoDb.Data.Models;

namespace minimal_api_repoDb.Data.Respositories;

public class EmployeeRepository(EntityDatabaseContext _context)
{
    public List<Employee> GetAllEmployees()
    {
        return [.. _context.Employees.Include(f => f.Reviews)];
    }
    public Employee? GetEmployeeById(int id)
    {
        return _context.Employees.Include(f => f.Reviews).Where(d => d.Id == id).FirstOrDefault();
    }
    public Employee AddEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
        return employee;
    }
    public Employee? UpdateEmployee(int id, Employee employee)
    {
        var _employee = _context.Employees.Where(d => d.Id == id).FirstOrDefault();
        if (_employee != null)
        {
            _employee.Email = employee.Email;
            _employee.FirstName = employee.FirstName;
            _employee.LastName = employee.LastName;
        }
        _context.SaveChanges();
        return _employee;
    }
    public void DeleteEmployee(int id)
    {
        var _employee = _context.Employees.Where(d => d.Id == id).FirstOrDefault();
        if (_employee != null)
        {
            _context.Employees.Remove(_employee);
            _context.SaveChanges();
        }
    }
}
