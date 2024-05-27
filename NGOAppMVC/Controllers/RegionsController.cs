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
    public class RegionsController : Controller
    {
        private readonly DCodeNGOdataNGOsqliteContext _context;

        public RegionsController(DCodeNGOdataNGOsqliteContext context)
        {
            _context = context;
        }

        // GET: Regions
        public async Task<IActionResult> Index()
        {
              return View(await _context.LkpRegions.ToListAsync());
        }

        // GET: Regions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.LkpRegions == null)
            {
                return NotFound();
            }

            var lkpRegions = await _context.LkpRegions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkpRegions == null)
            {
                return NotFound();
            }

            return View(lkpRegions);
        }

        // GET: Regions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Regions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,District,Neighborhood,GeoLocationUrl")] LkpRegions lkpRegions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lkpRegions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lkpRegions);
        }

        // GET: Regions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.LkpRegions == null)
            {
                return NotFound();
            }

            var lkpRegions = await _context.LkpRegions.FindAsync(id);
            if (lkpRegions == null)
            {
                return NotFound();
            }
            return View(lkpRegions);
        }

        // POST: Regions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,City,District,Neighborhood,GeoLocationUrl")] LkpRegions lkpRegions)
        {
            if (id != lkpRegions.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lkpRegions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LkpRegionsExists(lkpRegions.Id))
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
            return View(lkpRegions);
        }

        // GET: Regions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.LkpRegions == null)
            {
                return NotFound();
            }

            var lkpRegions = await _context.LkpRegions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkpRegions == null)
            {
                return NotFound();
            }

            return View(lkpRegions);
        }

        // POST: Regions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.LkpRegions == null)
            {
                return Problem("Entity set 'DCodeNGOdataNGOsqliteContext.LkpRegions'  is null.");
            }
            var lkpRegions = await _context.LkpRegions.FindAsync(id);
            if (lkpRegions != null)
            {
                _context.LkpRegions.Remove(lkpRegions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LkpRegionsExists(long id)
        {
          return _context.LkpRegions.Any(e => e.Id == id);
        }
    }
}
