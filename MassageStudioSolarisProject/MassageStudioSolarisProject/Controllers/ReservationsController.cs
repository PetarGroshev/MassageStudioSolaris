using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MassageStudioSolarisProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MassageStudioSolarisProject.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Client> _userManager;

        public ReservationsController(ApplicationDbContext context, UserManager<Client> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservations.Include(r => r.Clients).Include(r => r.Services);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Clients)
                .Include(r => r.Services)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }
        public async Task<IActionResult> CreateWithServiceId(int serviceId)
        {
            var currentService = await _context.Services.FirstOrDefaultAsync(z => z.Id == serviceId);
            Reservation reservation = new Reservation();
            //order.ProductsId = productId;
            // productId = order.ProductsId;
            reservation.ServicesId = serviceId;
            reservation.ClientsId = _userManager.GetUserId(User);
            var price =  currentService.Price;
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Reservations/Create
        public IActionResult Create()
        {
            //ViewData["ClientsId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicesId,ReservationStartDate,ReservationEndDate,DateModified")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.DateModified = DateTime.Now;
                reservation.ClientsId = _userManager.GetUserId(User);
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["ClientsId"] = new SelectList(_context.Users, "Id", "Id", reservation.ClientsId);
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name", reservation.ServicesId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            //ViewData["ClientsId"] = new SelectList(_context.Users, "Id", "Id", reservation.ClientsId);
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name", reservation.ServicesId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServicesId,ReservationStartDate,ReservationEndDate,DateModified")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    reservation.DateModified = DateTime.Now;
                    reservation.ClientsId = _userManager.GetUserId(User);
                    _context.Reservations.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            //ViewData["ClientsId"] = new SelectList(_context.Users, "Id", "Id", reservation.ClientsId);
            ViewData["ServicesId"] = new SelectList(_context.Services, "Id", "Name", reservation.ServicesId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Clients)
                .Include(r => r.Services)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
