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
    public class DepartmentsController : Controller
    {
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(ILogger<DepartmentsController> logger)
        {
            _logger = logger;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            using (var db = new DataContext())
            {
                return View(db.Department.ToList());
            }
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var db = new DataContext())
            {
                Department department = db.Department.Find(id);
                if (department == null)
                {
                    return NotFound();
                }
                return View(department);
            }
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartmentName,Description,IsActive")] Department department)
        {
            if (ModelState.IsValid)
            {
                using (var db = new DataContext())
                {
                    department.IsActive = Boolean.Parse(Request.Form["IsActive"]);
                    db.Department.Add(department);
                    db.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var db = new DataContext())
            {
                Department department = db.Department.Find(id);
                if (department == null)
                {
                    return NotFound();
                }
                return View(department);
            }
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName,Description,IsActive")] Department department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }
            department.IsActive = Boolean.Parse(Request.Form["IsActive"]);
            if (ModelState.IsValid)
            {
                using (var db = new DataContext())
                {
                    
                    db.Entry(department).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            using (var db = new DataContext())
            {
                Department department = db.Department.Find(id);
                if (department == null)
                {
                    return NotFound();
                }
                return View(department);
            }
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var db = new DataContext())
            {
                Department department = db.Department.Find(id);
                db.Department.Remove(department);
                db.SaveChanges();
                var employeeList = db.Employees.ToList().FindAll(x => x.DepID == id);
                foreach (var item in employeeList)
                {
                    db.Employees.Remove(item);
                    db.SaveChanges();
                }

                //db.Employees.Remove(employee);
                //db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
        }

        //private bool DepartmentExists(int id)
        //{
        //  return (_context.Department?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
