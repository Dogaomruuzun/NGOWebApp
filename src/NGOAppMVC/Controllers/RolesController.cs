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
    public class RolesController : Controller
    {
        private readonly DCodeNGOdataNGOsqliteContext _context;

        public RolesController(DCodeNGOdataNGOsqliteContext context)
        {
            _context = context;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
              return View(await _context.LkpRole.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.LkpRole == null)
            {
                return NotFound();
            }

            var lkpRole = await _context.LkpRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkpRole == null)
            {
                return NotFound();
            }

            return View(lkpRole);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleName")] LkpRole lkpRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lkpRole);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lkpRole);
        }

        // GET: Roles/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.LkpRole == null)
            {
                return NotFound();
            }

            var lkpRole = await _context.LkpRole.FindAsync(id);
            if (lkpRole == null)
            {
                return NotFound();
            }
            return View(lkpRole);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,RoleName")] LkpRole lkpRole)
        {
            if (id != lkpRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lkpRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LkpRoleExists(lkpRole.Id))
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
            return View(lkpRole);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.LkpRole == null)
            {
                return NotFound();
            }

            var lkpRole = await _context.LkpRole
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lkpRole == null)
            {
                return NotFound();
            }

            return View(lkpRole);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.LkpRole == null)
            {
                return Problem("Entity set 'DCodeNGOdataNGOsqliteContext.LkpRole'  is null.");
            }
            var lkpRole = await _context.LkpRole.FindAsync(id);
            if (lkpRole != null)
            {
                _context.LkpRole.Remove(lkpRole);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LkpRoleExists(long id)
        {
          return _context.LkpRole.Any(e => e.Id == id);
        }
    }
}
