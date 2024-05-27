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
    public class DonableItemsController : Controller
    {
        private readonly DCodeNGOdataNGOsqliteContext _context;

        public DonableItemsController(DCodeNGOdataNGOsqliteContext context)
        {
            _context = context;
        }

        // GET: DonableItems
        public async Task<IActionResult> Index()
        {
              return View(await _context.LkpDonableItem.ToListAsync());
        }

        // GET: DonableItems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.LkpDonableItem == null)
            {
                return NotFound();
            }

            var lkpDonableItem = await _context.LkpDonableItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkpDonableItem == null)
            {
                return NotFound();
            }

            return View(lkpDonableItem);
        }

        // GET: DonableItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DonableItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DonableTypeName,DonableAmountType")] LkpDonableItem lkpDonableItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lkpDonableItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lkpDonableItem);
        }

        // GET: DonableItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.LkpDonableItem == null)
            {
                return NotFound();
            }

            var lkpDonableItem = await _context.LkpDonableItem.FindAsync(id);
            if (lkpDonableItem == null)
            {
                return NotFound();
            }
            return View(lkpDonableItem);
        }

        // POST: DonableItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,DonableTypeName,DonableAmountType")] LkpDonableItem lkpDonableItem)
        {
            if (id != lkpDonableItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lkpDonableItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LkpDonableItemExists(lkpDonableItem.Id))
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
            return View(lkpDonableItem);
        }

        // GET: DonableItems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.LkpDonableItem == null)
            {
                return NotFound();
            }

            var lkpDonableItem = await _context.LkpDonableItem
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkpDonableItem == null)
            {
                return NotFound();
            }

            return View(lkpDonableItem);
        }

        // POST: DonableItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.LkpDonableItem == null)
            {
                return Problem("Entity set 'DCodeNGOdataNGOsqliteContext.LkpDonableItem'  is null.");
            }
            var lkpDonableItem = await _context.LkpDonableItem.FindAsync(id);
            if (lkpDonableItem != null)
            {
                _context.LkpDonableItem.Remove(lkpDonableItem);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LkpDonableItemExists(long id)
        {
          return _context.LkpDonableItem.Any(e => e.Id == id);
        }
    }
}
