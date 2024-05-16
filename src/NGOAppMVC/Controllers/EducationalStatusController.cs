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
    public class EducationalStatusController : Controller
    {
        private readonly DCodeNGOdataNGOsqliteContext _context;

        public EducationalStatusController(DCodeNGOdataNGOsqliteContext context)
        {
            _context = context;
        }

        // GET: EducationalStatus
        public async Task<IActionResult> Index()
        {
              return _context.LkpEducationalStatus != null ? 
                          View(await _context.LkpEducationalStatus.ToListAsync()) :
                          Problem("Entity set 'DCodeNGOdataNGOsqliteContext.LkpEducationalStatus'  is null.");
        }

        // GET: EducationalStatus/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.LkpEducationalStatus == null)
            {
                return NotFound();
            }

            var lkpEducationalStatus = await _context.LkpEducationalStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkpEducationalStatus == null)
            {
                return NotFound();
            }

            return View(lkpEducationalStatus);
        }

        // GET: EducationalStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducationalStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] LkpEducationalStatus lkpEducationalStatus)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lkpEducationalStatus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lkpEducationalStatus);
        }

        // GET: EducationalStatus/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.LkpEducationalStatus == null)
            {
                return NotFound();
            }

            var lkpEducationalStatus = await _context.LkpEducationalStatus.FindAsync(id);
            if (lkpEducationalStatus == null)
            {
                return NotFound();
            }
            return View(lkpEducationalStatus);
        }

        // POST: EducationalStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] LkpEducationalStatus lkpEducationalStatus)
        {
            if (id != lkpEducationalStatus.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lkpEducationalStatus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LkpEducationalStatusExists(lkpEducationalStatus.Id))
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
            return View(lkpEducationalStatus);
        }

        // GET: EducationalStatus/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.LkpEducationalStatus == null)
            {
                return NotFound();
            }

            var lkpEducationalStatus = await _context.LkpEducationalStatus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkpEducationalStatus == null)
            {
                return NotFound();
            }

            return View(lkpEducationalStatus);
        }

        // POST: EducationalStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.LkpEducationalStatus == null)
            {
                return Problem("Entity set 'DCodeNGOdataNGOsqliteContext.LkpEducationalStatus'  is null.");
            }
            var lkpEducationalStatus = await _context.LkpEducationalStatus.FindAsync(id);
            if (lkpEducationalStatus != null)
            {
                _context.LkpEducationalStatus.Remove(lkpEducationalStatus);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LkpEducationalStatusExists(long id)
        {
          return (_context.LkpEducationalStatus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
