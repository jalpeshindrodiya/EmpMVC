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
    public class TaskDTOesController : Controller
    {
        private readonly DataContext _context;

        public TaskDTOesController(DataContext context)
        {
            _context = context;
        }

        // GET: TaskDTOes
        public async Task<IActionResult> Index()
        {
              return _context.TaskDTO != null ? 
                          View(await _context.TaskDTO.ToListAsync()) :
                          Problem("Entity set 'DataContext.TaskDTO'  is null.");
        }

        // GET: TaskDTOes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaskDTO == null)
            {
                return NotFound();
            }

            var taskDTO = await _context.TaskDTO
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskDTO == null)
            {
                return NotFound();
            }

            return View(taskDTO);
        }

        // GET: TaskDTOes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskDTOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TaskType,Task,TaskDescription,DueAsOn,TaskCurrentStatus")] TaskDTO taskDTO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskDTO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskDTO);
        }

        // GET: TaskDTOes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaskDTO == null)
            {
                return NotFound();
            }

            var taskDTO = await _context.TaskDTO.FindAsync(id);
            if (taskDTO == null)
            {
                return NotFound();
            }
            return View(taskDTO);
        }

        // POST: TaskDTOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TaskType,Task,TaskDescription,DueAsOn,TaskCurrentStatus")] TaskDTO taskDTO)
        {
            if (id != taskDTO.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskDTO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskDTOExists(taskDTO.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(taskDTO);
        }

        // GET: TaskDTOes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaskDTO == null)
            {
                return NotFound();
            }

            var taskDTO = await _context.TaskDTO
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskDTO == null)
            {
                return NotFound();
            }

            return View(taskDTO);
        }

        // POST: TaskDTOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaskDTO == null)
            {
                return Problem("Entity set 'DataContext.TaskDTO'  is null.");
            }
            var taskDTO = await _context.TaskDTO.FindAsync(id);
            if (taskDTO != null)
            {
                _context.TaskDTO.Remove(taskDTO);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskDTOExists(int id)
        {
          return (_context.TaskDTO?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
