using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelProject.Models;

namespace HotelProject.Controllers
{
    public class StatusController : Controller
    {
        private readonly HotelContext _context;

        public StatusController(HotelContext context)
        {
            _context = context;    
        }

        // GET: Status
        public async Task<IActionResult> Index()
        {
            var hotelContext = _context.RoomStatuses.Include(s => s.Employee).Include(s => s.Room);
            return View(await hotelContext.ToListAsync());
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.RoomStatuses
                .Include(s => s.Employee)
                .Include(s => s.Room)
                .SingleOrDefaultAsync(m => m.StatusId == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId");
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,RoomId,EmployeeId,Reservation,Arrival,Departure,Price")] Status status)
        {
            if (ModelState.IsValid)
            {
                _context.Add(status);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", status.EmployeeId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", status.RoomId);
            return View(status);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.RoomStatuses.SingleOrDefaultAsync(m => m.StatusId == id);
            if (status == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", status.EmployeeId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", status.RoomId);
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,RoomId,EmployeeId,Reservation,Arrival,Departure,Price")] Status status)
        {
            if (id != status.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(status);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.StatusId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", status.EmployeeId);
            ViewData["RoomId"] = new SelectList(_context.Rooms, "RoomId", "RoomId", status.RoomId);
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.RoomStatuses
                .Include(s => s.Employee)
                .Include(s => s.Room)
                .SingleOrDefaultAsync(m => m.StatusId == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var status = await _context.RoomStatuses.SingleOrDefaultAsync(m => m.StatusId == id);
            _context.RoomStatuses.Remove(status);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StatusExists(int id)
        {
            return _context.RoomStatuses.Any(e => e.StatusId == id);
        }
    }
}
