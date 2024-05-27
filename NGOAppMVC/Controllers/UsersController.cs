using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NGOAppMVC.Data;
using NGOAppMVC.DBModels;
using static System.Formats.Asn1.AsnWriter;

namespace NGOAppMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly DCodeNGOdataNGOsqliteContext _context;

        public UsersController(DCodeNGOdataNGOsqliteContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var dCodeNGOdataNGOsqliteContext = _context.Ngouser.Include(n => n.ApprovementStatus);
            return View(await dCodeNGOdataNGOsqliteContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Ngouser == null)
            {
                return NotFound();
            }

            var ngouser = await _context.Ngouser
                .Include(n => n.ApprovementStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ngouser == null)
            {
                return NotFound();
            }

            return View(ngouser);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["ApprovementStatusId"] = new SelectList(_context.LkpApprovementStatus, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Email,Age,Gender,Password,RegisteredDate,ApprovementStatusId,PhoneNumber")] Ngouser ngouser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ngouser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApprovementStatusId"] = new SelectList(_context.LkpApprovementStatus, "Id", "Id", ngouser.ApprovementStatusId);
            return View(ngouser);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Ngouser == null)
            {
                return NotFound();
            }

            var ngouser = await _context.Ngouser.FindAsync(id);
            if (ngouser == null)
            {
                return NotFound();
            }
            var ApprovementStatusDB = _context.LkpApprovementStatus.ToList();
            List<SelectListItem> educationList = new();
            
           if (ApprovementStatusDB?.Count > 0)
            {
                foreach (var item in ApprovementStatusDB)
                {
                    educationList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                }
            }


            ViewData["ApprovementStatusList"] = educationList;
            return View(ngouser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,FirstName,LastName,Email,Age,Gender,PasswordHash, RegisteredDate,ApprovementStatusId,PhoneNumber")] Ngouser ngouser)
        {
            if (id != ngouser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ngouser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NgouserExists(ngouser.Id))
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
            ViewData["ApprovementStatusId"] = new SelectList(_context.LkpApprovementStatus, "Id", "Id", ngouser.ApprovementStatusId);
            return View(ngouser);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Ngouser == null)
            {
                return NotFound();
            }

            var ngouser = await _context.Ngouser
                .Include(n => n.ApprovementStatus)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ngouser == null)
            {
                return NotFound();
            }

            return View(ngouser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Ngouser == null)
            {
                return Problem("Entity set 'DCodeNGOdataNGOsqliteContext.Ngouser'  is null.");
            }
            var ngouser = await _context.Ngouser.FindAsync(id);
            if (ngouser != null)
            {
                _context.Ngouser.Remove(ngouser);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NgouserExists(long id)
        {
          return (_context.Ngouser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
