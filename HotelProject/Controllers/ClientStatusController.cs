using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelProject.Models;
using Microsoft.EntityFrameworkCore;
using HotelProject.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelProject.Controllers
{
    public class ClientStatusController : Controller
    {

        public async Task<IActionResult> Index()
        {
            var client =  _context.Clients.Include(p => p.Status).ToList();
            foreach (var item in client)
            {
                item.Status.Employee = _context.Employees.Find(item.Status.EmployeeId);
            }
            return  View(client);
        }

        private readonly HotelContext _context;

        public ClientStatusController(HotelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var room = _context.Rooms.Include(p => p.Category);
            var emp = _context.Employees.Include(p => p.Position);
            ViewData["Employee"] = emp.ToList();
            ViewData["Room"] = room.ToList();
            return View();
        }


        // GET: /<controller>/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,RoomId,EmployeeId,Reservation,Arrival,Departure,Price")] Status status, [Bind("ClientId,StatusId,FirstName,LastName,Birthday,Sex,DocumentForm,DocumentsSeries,DocumentsNumber,Phone")] Client client)
        {
            _context.RoomStatuses.Add(status);
            _context.SaveChanges();
            
            Status s = await _context.RoomStatuses.LastOrDefaultAsync(p =>p.Arrival== status.Arrival);
            client.StatusId = s.StatusId;
            _context.Clients.Add(client);
            _context.SaveChanges();
            return RedirectToAction("Index");
           
        }

        public async Task<IActionResult> Details(int id)
        {
            Client client = _context.Clients.Find(id);
            client.Status = _context.RoomStatuses.Find(client.StatusId);
            client.Status.Room = _context.Rooms.Find(client.Status.RoomId);
            client.Status.Room.Category = _context.RoomsCateroties.Find(client.Status.Room.CategoryId);
            client.Status.Employee = _context.Employees.Find(client.Status.EmployeeId);
            client.Status.Employee.Position = _context.EmpPosition.Find(client.Status.Employee.PositionId);
            return View(client);

        }


        


        // GET: Clients/Edit/5
        public async Task<IActionResult> EditClient(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.SingleOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.RoomStatuses, "StatusId", "StatusId", client.StatusId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditClient(int id, [Bind("ClientId,StatusId,FirstName,LastName,Birthday,Sex,DocumentForm,DocumentsSeries,DocumentsNumber,Phone")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            ViewData["StatusId"] = new SelectList(_context.RoomStatuses, "StatusId", "StatusId", client.StatusId);
            return View(client);
        }


        // GET: Status/Edit/5
        public async Task<IActionResult> EditStatus(int? id)
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
        public async Task<IActionResult> EditStatus(int id, [Bind("StatusId,RoomId,EmployeeId,Reservation,Arrival,Departure,Price")] Status status)
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



        private bool ClientExists(int id)
        {
            return _context.Clients.Any(e => e.ClientId == id);
        }

        private bool StatusExists(int id)
        {
            return _context.RoomStatuses.Any(e => e.StatusId == id);
        }
    }
}
