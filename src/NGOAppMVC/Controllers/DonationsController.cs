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
    public class DonationsController : Controller
    {
        private readonly DCodeNGOdataNGOsqliteContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DonationsController(DCodeNGOdataNGOsqliteContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Donations
        public async Task<IActionResult> Index()
        {
            var dCodeNGOdataNGOsqliteContext = _context.Donation.Include(d => d.Ngouser).Include(d => d.Region);
            return View(await dCodeNGOdataNGOsqliteContext.ToListAsync());
        }

        // GET: Donations/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Donation == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation
                .Include(d => d.Ngouser)
                .Include(d => d.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // GET: Donations/Create
        public IActionResult Create()
        {
            ViewData["NgouserId"] = new SelectList(_context.Ngouser, "Id", "Email");
            ViewData["RegionId"] = new SelectList(_context.LkpRegions, "Id", "Id");

            var DonableItemsDB = _context.LkpDonableItem.ToList();
            List<SelectListItem> donableItemList = new();
            if (DonableItemsDB?.Count > 0)
            {
                donableItemList.Add(new SelectListItem { Text = "Donable", Value = "0" });
                foreach (var item in DonableItemsDB)
                {
                    donableItemList.Add(new SelectListItem { Text = item.DonableTypeName, Value = item.Id.ToString() });
                }
            }
            var regionsDB = _context.LkpRegions.ToList();
            List<SelectListItem> regionList = new();
            if (regionsDB?.Count > 0)
            {
                regionList.Add(new SelectListItem { Text = "Region", Value = "0" });
                foreach (var item in regionsDB)
                {
                    var geoName = item.City + " - " + item.District + " - " + item.Neighborhood;
                    regionList.Add(new SelectListItem { Text = geoName, Value = item.Id.ToString() });
                }
            }

            ViewData["DonableItems"] = donableItemList;
            ViewData["Regions"] = regionList;
            return View();
        }

        // POST: Donations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NgouserId,DonationDate,DonableItemId,DonableItemAmount,RegionId")] Donation donation)
        {
            var userMail = _httpContextAccessor.HttpContext.User.Identity.Name;
            var userId = _context.Ngouser.Where(x => x.Email == userMail).FirstOrDefault().Id;
            if (ModelState.IsValid)
            {
                donation.NgouserId = userId;
                _context.Add(donation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NgouserId"] = new SelectList(_context.Ngouser, "Id", "Email", donation.NgouserId);
            ViewData["RegionId"] = new SelectList(_context.LkpRegions, "Id", "Id", donation.RegionId);
            return View(donation);
        }

        // GET: Donations/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Donation == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }
            ViewData["NgouserId"] = new SelectList(_context.Ngouser, "Id", "Email", donation.NgouserId);
            ViewData["RegionId"] = new SelectList(_context.LkpRegions, "Id", "Id", donation.RegionId);
            return View(donation);
        }

        // POST: Donations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,NgouserId,DonationDate,DonableItemId,DonableItemAmount,RegionId")] Donation donation)
        {
            if (id != donation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonationExists(donation.Id))
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
            ViewData["NgouserId"] = new SelectList(_context.Ngouser, "Id", "Email", donation.NgouserId);
            ViewData["RegionId"] = new SelectList(_context.LkpRegions, "Id", "Id", donation.RegionId);
            return View(donation);
        }

        // GET: Donations/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Donation == null)
            {
                return NotFound();
            }

            var donation = await _context.Donation
                .Include(d => d.Ngouser)
                .Include(d => d.Region)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (donation == null)
            {
                return NotFound();
            }

            return View(donation);
        }

        // POST: Donations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Donation == null)
            {
                return Problem("Entity set 'DCodeNGOdataNGOsqliteContext.Donation'  is null.");
            }
            var donation = await _context.Donation.FindAsync(id);
            if (donation != null)
            {
                _context.Donation.Remove(donation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DonationExists(long id)
        {
          return _context.Donation.Any(e => e.Id == id);
        }
    }
}
