using ASP.NETCoreD08.Data.Context;
using ASP.NETCoreD08.Models;

namespace ASP.NETCoreD08.Repositories.DepartmentRespository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        /*------------------------------------------------------------------*/
        private readonly AppDbContext _context;
        /*------------------------------------------------------------------*/
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }
        /*------------------------------------------------------------------*/
        public IEnumerable<Department> GetAll()
        {
            return _context.Departments.ToList();
        }
        /*------------------------------------------------------------------*/
        public Department? GetById(int departmentID)
        {
            return _context.Departments.Find(departmentID);
        }
        /*------------------------------------------------------------------*/
        public void Insert(Department department)
        {
            _context.Departments.Add(department);
        }
        /*------------------------------------------------------------------*/
        public void Delete(Department department)
        {
            _context.Departments.Remove(department);
        }
        /*------------------------------------------------------------------*/
        public void Update(Department department)
        {
        }
        /*------------------------------------------------------------------*/
        public int Save()
        {
            return _context.SaveChanges();
        }
        /*------------------------------------------------------------------*/
    }
}
