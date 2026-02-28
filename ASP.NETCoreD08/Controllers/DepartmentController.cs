using ASP.NETCoreD08.ViewModels.Department;
using ASP.NETCoreD08.Models;
using Microsoft.AspNetCore.Mvc;
using ASP.NETCoreD08.Repositories.DepartmentRespository;

namespace ASP.NETCoreD08.Controllers
{
    public class DepartmentController : Controller
    {
        /*------------------------------------------------------------------*/
        // Context => DB => Data Access
        //private readonly AppDbContext db = new AppDbContext();
        private readonly IDepartmentRepository _departmentRepository;
        /*------------------------------------------------------------------*/
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        /*------------------------------------------------------------------*/
        // Get All Departments
        [HttpGet]
        public IActionResult Index()
        {
            var departmentsReadVM = _departmentRepository.GetAll().Select(d => new DepartmentReadVM
            {
                Id = d.Id,
                Name = d.Name
            });
            return View(departmentsReadVM);
        }
        /*------------------------------------------------------------------*/
        // View Details 
        [HttpGet]
        public IActionResult Details(int id)
        {
            var department = _departmentRepository.GetById(id);

            if (department == null)
            {
                return RedirectToAction("Index");
            }

            // Map From Domain Model To View Model
            var departmentReadVM = new DepartmentReadVM
            {
                Id = department.Id,
                Name = department.Name,
            };

            return View(departmentReadVM);
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        public IActionResult Create(DepartmentCreateVM departmentCreateVM)
        {
            var newDepartment = new Department
            {
                Name = departmentCreateVM.Name
            };

            _departmentRepository.Insert(newDepartment);
            _departmentRepository.Save();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null)
            {
                return RedirectToAction("Index");
            }

            // Map Domain Model To VM
            var departmentEditVM = new DepartmentEditVM
            {
                Id = department.Id,
                Name = department.Name,
            };

            return View(departmentEditVM);
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        public IActionResult Edit(DepartmentEditVM departmentEditVM)
        {
            var departmentInDb = _departmentRepository.GetById(departmentEditVM.Id);
            if (departmentInDb == null)
            {
                return RedirectToAction("Index");
            }

            // Map From VM To Domain Model
            departmentInDb.Name = departmentEditVM.Name;
            _departmentRepository.Save();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------*/
        public IActionResult Delete(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department == null)
            {
                return RedirectToAction("Index");
            }
            _departmentRepository.Delete(department);
            _departmentRepository.Save();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------*/
    }
}
