using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompassMeetingApp.Data;
using CompassMeetingApp.Models;

namespace CompassMeetingApp.Controllers
{
    public class MeetingRoomsController : Controller
    {
        private readonly DataContext _context;

        public MeetingRoomsController(DataContext context)
        {
            _context = context;
        }

        // GET: MeetingRooms
        public async Task<IActionResult> Index()
        {
              return _context.MeetingRooms != null ? 
                          View(await _context.MeetingRooms.ToListAsync()) :
                          Problem("Entity set 'DataContext.MeetingRooms'  is null.");
        }

        // GET: MeetingRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MeetingRooms == null)
            {
                return NotFound();
            }

            var meetingRoom = await _context.MeetingRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetingRoom == null)
            {
                return NotFound();
            }

            return View(meetingRoom);
        }

        // GET: MeetingRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeetingRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location,DayBooked,TimeBooked,isBooked")] MeetingRoom meetingRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meetingRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meetingRoom);
        }

        // GET: MeetingRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MeetingRooms == null)
            {
                return NotFound();
            }

            var meetingRoom = await _context.MeetingRooms.FindAsync(id);
            if (meetingRoom == null)
            {
                return NotFound();
            }
            return View(meetingRoom);
        }

        // POST: MeetingRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location,DayBooked,TimeBooked,isBooked")] MeetingRoom meetingRoom)
        {
            if (id != meetingRoom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meetingRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeetingRoomExists(meetingRoom.Id))
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
            return View(meetingRoom);
        }

        // GET: MeetingRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MeetingRooms == null)
            {
                return NotFound();
            }

            var meetingRoom = await _context.MeetingRooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (meetingRoom == null)
            {
                return NotFound();
            }

            return View(meetingRoom);
        }

        // POST: MeetingRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MeetingRooms == null)
            {
                return Problem("Entity set 'DataContext.MeetingRooms'  is null.");
            }
            var meetingRoom = await _context.MeetingRooms.FindAsync(id);
            if (meetingRoom != null)
            {
                _context.MeetingRooms.Remove(meetingRoom);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MeetingRoomExists(int id)
        {
          return (_context.MeetingRooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
