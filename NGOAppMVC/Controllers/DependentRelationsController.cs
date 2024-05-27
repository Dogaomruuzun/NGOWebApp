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
    public class DependentRelationsController : Controller
    {
        private readonly DCodeNGOdataNGOsqliteContext _context;

        public DependentRelationsController(DCodeNGOdataNGOsqliteContext context)
        {
            _context = context;
        }

        // GET: DependentRelations
        public async Task<IActionResult> Index()
        {
              return View(await _context.LkbDependentRelation.ToListAsync());
        }

        // GET: DependentRelations/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.LkbDependentRelation == null)
            {
                return NotFound();
            }

            var lkbDependentRelation = await _context.LkbDependentRelation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkbDependentRelation == null)
            {
                return NotFound();
            }

            return View(lkbDependentRelation);
        }

        // GET: DependentRelations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DependentRelations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] LkbDependentRelation lkbDependentRelation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lkbDependentRelation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lkbDependentRelation);
        }

        // GET: DependentRelations/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.LkbDependentRelation == null)
            {
                return NotFound();
            }

            var lkbDependentRelation = await _context.LkbDependentRelation.FindAsync(id);
            if (lkbDependentRelation == null)
            {
                return NotFound();
            }
            return View(lkbDependentRelation);
        }

        // POST: DependentRelations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name")] LkbDependentRelation lkbDependentRelation)
        {
            if (id != lkbDependentRelation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lkbDependentRelation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LkbDependentRelationExists(lkbDependentRelation.Id))
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
            return View(lkbDependentRelation);
        }

        // GET: DependentRelations/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.LkbDependentRelation == null)
            {
                return NotFound();
            }

            var lkbDependentRelation = await _context.LkbDependentRelation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkbDependentRelation == null)
            {
                return NotFound();
            }

            return View(lkbDependentRelation);
        }

        // POST: DependentRelations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.LkbDependentRelation == null)
            {
                return Problem("Entity set 'DCodeNGOdataNGOsqliteContext.LkbDependentRelation'  is null.");
            }
            var lkbDependentRelation = await _context.LkbDependentRelation.FindAsync(id);
            if (lkbDependentRelation != null)
            {
                _context.LkbDependentRelation.Remove(lkbDependentRelation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LkbDependentRelationExists(long id)
        {
          return _context.LkbDependentRelation.Any(e => e.Id == id);
        }
    }
}
