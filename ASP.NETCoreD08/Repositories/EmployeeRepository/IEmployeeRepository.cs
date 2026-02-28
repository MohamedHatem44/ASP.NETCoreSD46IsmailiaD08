using ASP.NETCoreD08.Models;

namespace ASP.NETCoreD08.Repositories.EmployeeRepository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAll();
        Employee? GetById(int EmployeeID);
        void Insert(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        int Save();
    }
}
