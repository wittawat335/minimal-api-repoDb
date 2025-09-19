using minimal_api_repoDb.Data.Models;

namespace minimal_api_repoDb.Data.Respositories;

public class EmployeeRepository(EntityDatabaseContext _context)
{
    public List<Employee> GetAllEmployees()
    {
        return [.. _context.Employees];
    }
    //public Employee? GetEmployeeById(int id)
    //{
    //    return _context.EmployeeEntity.Include(f => f.Reviews).Where(d => d.Id == id).FirstOrDefault();
    //}
    //public Employee AddEmployee(Employee employee)
    //{
    //    _context.EmployeeEntity.Add(employee);
    //    _context.SaveChanges();
    //    return employee;
    //}
    //public Employee? UpdateEmployee(int id, Employee employee)
    //{
    //    var _employee = _context.EmployeeEntity.Where(d => d.Id == id).FirstOrDefault();
    //    if (_employee != null)
    //    {
    //        _employee.Email = employee.Email;
    //        _employee.FirstName = employee.FirstName;
    //        _employee.LastName = employee.LastName;
    //    }
    //    _context.SaveChanges();
    //    return _employee;
    //}
    //public void DeleteEmployee(int id)
    //{
    //    var _employee = _context.EmployeeEntity.Where(d => d.Id == id).FirstOrDefault();
    //    if (_employee != null)
    //    {
    //        _context.EmployeeEntity.Remove(_employee);
    //        _context.SaveChanges();
    //    }
    //}
}
