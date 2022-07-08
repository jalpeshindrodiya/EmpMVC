using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Emp.Database;

namespace Emp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        // GET: Employee
        public async Task<IActionResult> Index()
        {
            using (var db = new DataContext())
            {
                // return db.Tasks.ToList();
                var emplo = db.Employees.ToList();
                return View(emplo);

            }
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var db = new DataContext())
            {
                EmployeeDTO employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }

        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            using (var db = new DataContext())
            {
                var items = db.Department.ToList().FindAll(x => x.IsActive == true);
                if (items != null)
                {
                    ViewBag.data = items;
                }
            }
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,PhoneNumber,Salary,DepID,IsActive")] EmployeeDTO employee, IFormFile file)
        {
            //if (ModelState.IsValid)
            //{
            if (file != null)
            {
                employee.ImageName = string.Format("{0:yyyyMMddhhmmss}.png", DateTime.Now);
                var path = Path.Combine(
              Directory.GetCurrentDirectory(), "wwwroot/UploadFiles",
              employee.ImageName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            using (var db = new DataContext())
            {
                employee.DepID = int.Parse(Request.Form["DepartmentName"]);
                employee.IsActive = Boolean.Parse(Request.Form["IsActive"]);
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //}
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var db = new DataContext())
            {
                EmployeeDTO employee = db.Employees.Find(id);
                var items = db.Department.ToList().FindAll(x => x.IsActive == true);
                if (items != null)
                {
                    ViewBag.data = items;
                }
                if (employee == null)
                {
                    return NotFound();
                }
                if (employee.ImageName != null)
                {
                    // employee.ImageName = Path.Combine(Server.MapPath("~/UploadFiles"), employee.ImageName);
                }
                return View(employee);
            }
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Email,PhoneNumber,Salary,ImageName,DepID,IsActive")] EmployeeDTO employee, IFormFile file)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (file != null)
            {
                employee.ImageName = string.Format("{0:yyyyMMddhhmmss}.png", DateTime.Now);
                var path = Path.Combine(
              Directory.GetCurrentDirectory(), "wwwroot/UploadFiles",
              employee.ImageName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            {
                employee.ImageName = String.Empty;
            }

            using (var db = new DataContext())
            {
                employee.DepID = int.Parse(Request.Form["DepartmentName"]);
                employee.IsActive = Boolean.Parse(Request.Form["IsActive"]);
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var db = new DataContext())
            {
                EmployeeDTO employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var db = new DataContext())
            {
                EmployeeDTO employee = db.Employees.Find(id);
                db.Employees.Remove(employee);
                db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
        }

        //private bool EmployeeDTOExists(int id)
        //{
        //    return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
