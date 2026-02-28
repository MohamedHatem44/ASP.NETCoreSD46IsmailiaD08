using ASP.NETCoreD08.Models;
using ASP.NETCoreD08.Repositories.DepartmentRespository;
using ASP.NETCoreD08.Repositories.EmployeeRepository;
using ASP.NETCoreD08.ViewModels.Employee;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NETCoreD08.Controllers
{
    public class EmployeeController : Controller
    {
        /*------------------------------------------------------------------*/
        // Context => DB => Data Access
        //private readonly AppDbContext db = new AppDbContext();
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        /*------------------------------------------------------------------*/
        public EmployeeController(IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        /*------------------------------------------------------------------*/
        // Index => List All => Main Action => Landing Page
        [HttpGet]
        public IActionResult Index()
        {
            // Get All Employees
            // Map From Domain Model To VM
            var employeesReadVM = _employeeRepository.GetAll()
                .Select(e => new EmployeeReadVM
                {
                    Id = e.Id,
                    Name = e.Name,
                    Age = e.Age,
                    Salary = e.Salary,
                    Department = e.Department.Name
                }).ToList();

            return View(employeesReadVM);
        }
        /*------------------------------------------------------------------*/
        // View Details
        [HttpGet]
        public IActionResult Details(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }

            // Map FromDomain Model To View Model
            var employeeReadVM = new EmployeeReadVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                Department = employee.Department.Name
            };

            return View(employeeReadVM);
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Create()
        {
            var employeeCreateVM = new EmployeeCreateVM
            {
                Departments = GetDepartmentsForDropDown()
            };
            return View(employeeCreateVM);
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeCreateVM employeeCreateVM)
        {
            if (!ModelState.IsValid)
            {
                employeeCreateVM.Departments = GetDepartmentsForDropDown();
                return View(employeeCreateVM);
            }

            var employee = new Employee
            {
                Name = employeeCreateVM.Name!,
                Age = employeeCreateVM.Age,
                Salary = employeeCreateVM.Salary,
                DepartmentId = employeeCreateVM.DepartmentId
            };

            _employeeRepository.Insert(employee);
            _employeeRepository.Save();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------*/
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }

            // Map Domain Model To VM
            var employeeEditVM = new EmployeeEditVM
            {
                Id = employee.Id,
                Name = employee.Name,
                Age = employee.Age,
                Salary = employee.Salary,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department.Name,
                Departments = GetDepartmentsForDropDown()
            };
            return View(employeeEditVM);
        }
        /*------------------------------------------------------------------*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeEditVM employeeEditVM)
        {
            var employeeInDb = _employeeRepository.GetById(employeeEditVM.Id);
            if (employeeInDb == null)
            {
                return RedirectToAction("Index");
            }

            // Map From VM To Domain Model
            employeeInDb.Name = employeeEditVM.Name;
            employeeInDb.Age = employeeEditVM.Age;
            employeeInDb.Salary = employeeEditVM.Salary;
            employeeInDb.DepartmentId = employeeEditVM.DepartmentId;
            _employeeRepository.Save();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------*/
        public IActionResult Delete(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee == null)
            {
                return RedirectToAction("Index");
            }
            _employeeRepository.Delete(employee);
            _employeeRepository.Save();
            return RedirectToAction("Index");
        }
        /*------------------------------------------------------------------*/
        // Helper Method
        // DRY => Reusable Code => Don't Repeat Yourself
        private List<SelectListItem> GetDepartmentsForDropDown()
        {
            return _departmentRepository.GetAll()
             .Select(d => new SelectListItem
             {
                 Value = d.Id.ToString(),
                 Text = d.Name
             }).ToList();
        }
        /*------------------------------------------------------------------*/
    }
}
