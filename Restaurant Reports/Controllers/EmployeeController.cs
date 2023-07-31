using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant_Reports.Data;
using Restaurant_Reports.Models;
using Restaurant_Reports.Models.Domain;

namespace Restaurant_Reports.Controllers
{
    public class EmployeeController : Controller
    {
        private RedDBContext redDBContext;

        public EmployeeController(RedDBContext redDBContext)
        {
            this.redDBContext = redDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await redDBContext.Employees.ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployee)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployee.Name,
                Department = addEmployee.Department,
                DateOfHire = addEmployee.DateOfHire,
                Email = addEmployee.Email,
                Salary = addEmployee.Salary
            };
            await redDBContext.Employees.AddAsync(employee);
            await redDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await redDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {
                var empModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Department = employee.Department,
                    DateOfHire = employee.DateOfHire,
                    Email = employee.Email,
                    Salary = employee.Salary
                };
                return await Task.Run(() => View("View", empModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await redDBContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Department = model.Department;
                employee.DateOfHire = model.DateOfHire;
                employee.Email = model.Email;
                employee.Salary = model.Salary;

                await redDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await redDBContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                redDBContext.Employees.Remove(employee);
                await redDBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
