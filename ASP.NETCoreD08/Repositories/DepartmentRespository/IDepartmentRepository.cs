using ASP.NETCoreD08.Models;

namespace ASP.NETCoreD08.Repositories.DepartmentRespository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetAll();
        Department? GetById(int departmentID);
        void Insert(Department department);
        void Update(Department department);
        void Delete(Department department);
        int Save();
    }
}