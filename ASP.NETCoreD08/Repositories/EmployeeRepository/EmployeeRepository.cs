using ASP.NETCoreD08.Data.Context;
using ASP.NETCoreD08.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NETCoreD08.Repositories.EmployeeRepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
        /*------------------------------------------------------------------*/
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.Include(e => e.Department).ToList();
        }
        /*------------------------------------------------------------------*/
        public Employee? GetById(int EmployeeID)
        {
            return _context.Employees.Include(e => e.Department).FirstOrDefault(e => e.Id == EmployeeID);
        }
        /*------------------------------------------------------------------*/
        public void Insert(Employee employee)
        {
            _context.Employees.Add(employee);
        }
        /*------------------------------------------------------------------*/
        public void Update(Employee employee)
        {

        }
        /*------------------------------------------------------------------*/
        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
        }
        /*------------------------------------------------------------------*/
        public int Save()
        {
            return _context.SaveChanges();
        }
        /*------------------------------------------------------------------*/
    }
}
