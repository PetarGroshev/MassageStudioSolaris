using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudioSolaris.Data;

namespace StudioSolaris.Controllers
{
    public class SpecialistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SpecialistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Specialists
        public async Task<IActionResult> Index()
        {
            return View(await _context.Specialists.ToListAsync());
        }

        // GET: Specialists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialist == null)
            {
                return NotFound();
            }

            return View(specialist);
        }

        // GET: Specialists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,DateModified")] Specialist specialist)
        {
            specialist.DateModified = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View(specialist);
            }
            _context.Add(specialist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Specialists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists.FindAsync(id);
            if (specialist == null)
            {
                return NotFound();
            }
            return View(specialist);
        }

        // POST: Specialists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,DateModified")] Specialist specialist)
        {
            if (id != specialist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecialistExists(specialist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        specialist.DateModified = DateTime.Now;
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(specialist);
        }

        // GET: Specialists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialist == null)
            {
                return NotFound();
            }

            return View(specialist);
        }

        // POST: Specialists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialist = await _context.Specialists.FindAsync(id);
            if (specialist != null)
            {
                _context.Specialists.Remove(specialist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialistExists(int id)
        {
            return _context.Specialists.Any(e => e.Id == id);
        }
    }
}
