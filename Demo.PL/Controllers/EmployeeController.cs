using Demo.DAL.Models;
using Demo.PLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class EmployeeController(IEmployeeRepository employeeRepo) : Controller
    {
        private readonly IEmployeeRepository _employeeRepo = employeeRepo;

        [HttpGet ]
        public IActionResult Index([FromQuery(Name ="Name")]string emp)
        {
            
            if(emp is not null) {
                var employees = _employeeRepo.GetEmployeeByName(emp);
                return View(employees);

            }
            var employee = _employeeRepo.GetAll();
         return View (employee);

            
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }
        [HttpPost]
     //  [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var count = _employeeRepo.Add(employee);
                if (count > 0)
                    return RedirectToAction(nameof(Index));

            }

            return View(employee);

        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            }
            var Department = _employeeRepo.Get(id.Value);
            if (Department == null)
                return NotFound();
            return View(Department);
        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeRepo.Get(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id!=employee.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepo.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeRepo.Get(id.Value);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (id!=employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepo.Delete(employee);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(employee);

        }
    }
}