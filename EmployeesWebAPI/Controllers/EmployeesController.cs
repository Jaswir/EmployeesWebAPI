using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeesWebAPI.Data;
using EmployeesWebAPI.Models;
using EmployeesWebAPI.Models.Domain;
using EmployeesWebAPI.Models.Employees;

namespace EmployeesWebAPI.Controllers
{
    public class EmployeesController : Controller
    {
        public ApplicationDbContext ApplicationDbContext { get; }

        public EmployeesController(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        //Read
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await ApplicationDbContext.Employees.ToListAsync();
            return View(employees);
        }

        //Create
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Department = addEmployeeRequest.Department,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
            };

            await ApplicationDbContext.Employees.AddAsync(employee);
            await ApplicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //Update

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var employee = await ApplicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null) return RedirectToAction("Index");

            var viewModel = new UpdateEmployeeViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateEmployeeViewModel model)
        {
            var employee = await ApplicationDbContext.Employees.FindAsync(model.Id);

            if (employee == null) return RedirectToAction("Index");

            if (employee != null)
            {

                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;

                await ApplicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var employee = await ApplicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee == null) return RedirectToAction("Index");

            ApplicationDbContext.Employees.Remove(employee);
            await ApplicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
