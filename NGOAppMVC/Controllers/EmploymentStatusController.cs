using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NGOAppMVC.DBModels;

namespace NGOAppMVC.Controllers
{
    public class EmploymentStatusController : Controller
    {
        private readonly DCodeNGOdataNGOsqliteContext _context;

        public EmploymentStatusController(DCodeNGOdataNGOsqliteContext context)
        {
            _context = context;
        }

        // GET: EmploymentStatus
        public async Task<IActionResult> Index()
        {
            return _context.LkpEmploymentStatus != null ?
                        View(await _context.LkpEmploymentStatus.ToListAsync()) :
                        Problem("Entity set 'DCodeNGOdataNGOsqliteContext.LkpEmploymentStatus'  is null.");
        }

        // GET: EmploymentStatus/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.LkpEmploymentStatus == null)
            {
                return NotFound();
            }

            var lkpEmploymentStatus = await _context.LkpEmploymentStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkpEmploymentStatus == null)
            {
                return NotFound();
            }

            return View(lkpEmploymentStatus);
        }

        // GET: EmploymentStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmploymentStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] LkpEmploymentStatus lkpEmploymentStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lkpEmploymentStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lkpEmploymentStatus);
        }

        // GET: EmploymentStatus/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.LkpEmploymentStatus == null)
            {
                return NotFound();
            }

            var lkpEmploymentStatus = await _context.LkpEmploymentStatus.FindAsync(id);
            if (lkpEmploymentStatus == null)
            {
                return NotFound();
            }
            return View(lkpEmploymentStatus);
        }

        // POST: EmploymentStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] LkpEmploymentStatus lkpEmploymentStatus)
        {
            if (id != lkpEmploymentStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lkpEmploymentStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LkpEmploymentStatusExists(lkpEmploymentStatus.Id))
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
            return View(lkpEmploymentStatus);
        }

        // GET: EmploymentStatus/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.LkpEmploymentStatus == null)
            {
                return NotFound();
            }

            var lkpEmploymentStatus = await _context.LkpEmploymentStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkpEmploymentStatus == null)
            {
                return NotFound();
            }

            return View(lkpEmploymentStatus);
        }

        // POST: EmploymentStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.LkpEmploymentStatus == null)
            {
                return Problem("Entity set 'DCodeNGOdataNGOsqliteContext.LkpEmploymentStatus'  is null.");
            }
            var lkpEmploymentStatus = await _context.LkpEmploymentStatus.FindAsync(id);
            if (lkpEmploymentStatus != null)
            {
                _context.LkpEmploymentStatus.Remove(lkpEmploymentStatus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LkpEmploymentStatusExists(long id)
        {
            return (_context.LkpEmploymentStatus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
