using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MassageStudioSolarisProject.Data;
using Microsoft.IdentityModel.Tokens;

namespace MassageStudioSolarisProject.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index(string searchString)
        {
            if (searchString.IsNullOrEmpty())
            {
                return View(await _context.Services.ToListAsync());
            }
            if (_context.Services == null)
            {
                return Problem("Context is empty");
            }
            var services = from m in _context.Services select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                services = services.Where(s => s.Name.Contains(searchString));
            }
            return View(services.ToList());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceTypes)
                .Include(s => s.Specialists)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["ServiceTypesId"] = new SelectList(_context.ServicesTypes, "Id", "Name");
            ViewData["SpecialistsId"] = new SelectList(_context.Specialists, "Id", "FirstName");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ServiceTypesId,Decription,SpecialistsId,ImageUrl,Price,DateAdded")] Service service)
        {
            service.DateAdded = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceTypesId"] = new SelectList(_context.ServicesTypes, "Id", "Name", service.ServiceTypesId);
            ViewData["SpecialistsId"] = new SelectList(_context.Specialists, "Id", "FirstName", service.SpecialistsId);
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["ServiceTypesId"] = new SelectList(_context.ServicesTypes, "Id", "Name", service.ServiceTypesId);
            ViewData["SpecialistsId"] = new SelectList(_context.Specialists, "Id", "FirstName", service.SpecialistsId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ServiceTypesId,Decription,SpecialistsId,ImageUrl,Price,DateAdded")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.DateAdded = DateTime.Now;
                    _context.Services.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            ViewData["ServiceTypesId"] = new SelectList(_context.ServicesTypes, "Id", "Name", service.ServiceTypesId);
            ViewData["SpecialistsId"] = new SelectList(_context.Specialists, "Id", "FirstName", service.SpecialistsId);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.ServiceTypes)
                .Include(s => s.Specialists)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
