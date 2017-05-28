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
    public class ReservationsStatusController : Controller
    {
        private readonly HotelContext _context;

        public ReservationsStatusController(HotelContext context)
        {
            _context = context;    
        }

        // GET: ReservationsStatus
        public async Task<IActionResult> Index()
        {
            var hotelContext = _context.Reservations.Include(r => r.StatusRef);
           

            
            foreach (var item in hotelContext)
            {
                item.StatusRef.Employee = _context.Employees.Find(item.StatusRef.EmployeeId);
            }
            return View(await hotelContext.ToListAsync());


        }

        // GET: ReservationsStatus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.StatusRef)
                .SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: ReservationsStatus/Create
        public IActionResult Create()
        {
            var roombuf = _context.Rooms.Include(p => p.Category);
            var emp = _context.Employees.Include(p => p.Position);
            ViewData["Employee"] = emp.ToList();

            List<Room> room = new List<Room>();

            ViewData["Room"] = roombuf.ToList();
            return View();
        }

        // POST: ReservationsStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReservationId,StatusId,ClientName,Phone,PersonNumber")] Reservation reservation, [Bind("StatusId,RoomId,EmployeeId,Reservation,Arrival,Departure,Price")] Status status)
        {
            if (ModelState.IsValid)
            {

                status.Reservation = true;
                _context.RoomStatuses.Add(status);
                _context.SaveChanges();

                Status s = await _context.RoomStatuses.LastOrDefaultAsync(p => p.Arrival == status.Arrival);

                reservation.StatusId = s.StatusId;
                

                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["StatusId"] = new SelectList(_context.RoomStatuses, "StatusId", "StatusId", reservation.StatusId);
            return View(reservation);
        }

        // GET: ReservationsStatus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.RoomStatuses, "StatusId", "StatusId", reservation.StatusId);
            return View(reservation);
        }

        // POST: ReservationsStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReservationId,StatusId,ClientName,Phone,PersonNumber")] Reservation reservation)
        {
            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.ReservationId))
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
            ViewData["StatusId"] = new SelectList(_context.RoomStatuses, "StatusId", "StatusId", reservation.StatusId);
            return View(reservation);
        }

        // GET: ReservationsStatus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.StatusRef)
                .SingleOrDefaultAsync(m => m.ReservationId == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: ReservationsStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.SingleOrDefaultAsync(m => m.ReservationId == id);
            _context.Reservations.Remove(reservation);
            var stat = await _context.RoomStatuses.SingleOrDefaultAsync(m => m.StatusId == reservation.StatusId);
            _context.RoomStatuses.Remove(stat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.ReservationId == id);
        }
    }
}
