using Demo.DAL.Models;
using Demo.PLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepo;


        public DepartmentController(IDepartmentRepository departmentRepo)
        {
            _departmentRepo = departmentRepo;
        }


        public IActionResult Index()
        {


            var departments = _departmentRepo.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {


            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) { 
            var count=_departmentRepo.Add(department);
                if (count > 0) 
                    return RedirectToAction(nameof(Index));
            
            }

            return View(department);

        }
        public IActionResult Details (int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest();
            } 
            var Department=_departmentRepo.Get(id.Value);
            if (Department == null)
                return NotFound();
            return View(Department);
        }



        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentRepo.Get(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id!=department.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepo.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();
            var department = _departmentRepo.Get(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }

        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Department department)
        {
            if (id!=department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _departmentRepo.Delete(department);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(department);

        }
    }
}