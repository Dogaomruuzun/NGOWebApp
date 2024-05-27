using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NGOAppMVC.DBModels;
using NGOAppMVC.Models;

namespace NGOAppMVC.Controllers
{
    public class WarehouseController : Controller
    {
        private readonly DCodeNGOdataNGOsqliteContext _context;

        public WarehouseController(DCodeNGOdataNGOsqliteContext context)
        {
            _context = context;
        }

        // GET: Warehouse
        public async Task<IActionResult> Index()
        {
            var dCodeNGOdataNGOsqliteContext = _context.WarehouseAssets.Include(w => w.Donation);
            return View(await dCodeNGOdataNGOsqliteContext.ToListAsync());
        }

        public async Task<IActionResult> DepoyaAlma(long id)
        {
            if (_context.WarehouseAssets == null)
            {
                return NotFound();
            }
            var model = new DTOWarehouseAssets();
            model.DonationId = id;
            var donationsDB = _context.Donation.FirstOrDefault(d => d.Id == id);
            var user = _context.Ngouser.Where(x => x.Id == donationsDB.NgouserId).FirstOrDefault();

            model.Donation = new DTODonations
            {
                Id = id,
                NGOUserName = user.FirstName + " " + user.LastName,
                DonationDate = donationsDB.DonationDate,
                DonableItemId = donationsDB.DonableItemId,
                DonableItemAmount = donationsDB.DonableItemAmount,
                RegionId = donationsDB.RegionId,
                DonableItem = _context.LkpDonableItem.Where(x => x.Id == donationsDB.DonableItemId).FirstOrDefault().DonableTypeName,
                Region = _context.LkpRegions.Where(x => x.Id == donationsDB.RegionId).FirstOrDefault().City + " - " + _context.LkpRegions.Where(x => x.Id == donationsDB.RegionId).FirstOrDefault().District + " - " + _context.LkpRegions.Where(x => x.Id == donationsDB.RegionId).FirstOrDefault().Neighborhood,
                Status = "Depoya Alınmadı" 
            };
            var volunteers = await _context.Volunteer.Include(w=>w.Ngouser).ToListAsync();
            model.VolunteerList = new();
            model.ImportDate = DateTime.Now.ToString("dd-MM-yyyy");
            foreach (var item in volunteers)
            {
                model.VolunteerList.Add(new SelectListItem
                {
                    Text = item.Ngouser.FirstName + " " + item.Ngouser.LastName,
                    Value = item.NgouserId.ToString()
                });
            }
            return View(model);
        }


        // GET: Warehouse/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.WarehouseAssets == null)
            {
                return NotFound();
            }

            var warehouseAssets = await _context.WarehouseAssets
                .Include(w => w.Donation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouseAssets == null)
            {
                return NotFound();
            }

            return View(warehouseAssets);
        }

        // GET: Warehouse/Create
        public IActionResult Create()
        {
            ViewData["DonationId"] = new SelectList(_context.Donation, "Id", "DonationDate");
            return View();
        }

        // POST: Warehouse/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DonationId,ImportedVolunteerId,ImportDate,ExportedVolunteerId,ExportDate,IndigentId")] WarehouseAssets warehouseAssets)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouseAssets);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonationId"] = new SelectList(_context.Donation, "Id", "DonationDate", warehouseAssets.DonationId);
            return View(warehouseAssets);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Olustur(DTOWarehouseAssets warehouseAssets)
        {
            if (ModelState.IsValid)
            {
                var warehouseAssetId = _context.WarehouseAssets.Max(e => e.Id) + 1;
                var depoyaAlinan = new WarehouseAssets
                {
                    ImportedVolunteerId = warehouseAssets.ImportedVolunteerId,
                    ImportDate = warehouseAssets.ImportDate,
                    DonationId = warehouseAssets.DonationId,
                    Id = warehouseAssetId
                };

                _context.Add(depoyaAlinan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouseAssets);
        }


        // GET: Warehouse/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.WarehouseAssets == null)
            {
                return NotFound();
            }

            var warehouseAssets = await _context.WarehouseAssets.FindAsync(id);
            if (warehouseAssets == null)
            {
                return NotFound();
            }
            ViewData["DonationId"] = new SelectList(_context.Donation, "Id", "DonationDate", warehouseAssets.DonationId);
            return View(warehouseAssets);
        }

        // POST: Warehouse/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long? id, [Bind("Id,DonationId,ImportedVolunteerId,ImportDate,ExportedVolunteerId,ExportDate,IndigentId")] WarehouseAssets warehouseAssets)
        {
            if (id != warehouseAssets.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouseAssets);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseAssetsExists(warehouseAssets.Id))
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
            ViewData["DonationId"] = new SelectList(_context.Donation, "Id", "DonationDate", warehouseAssets.DonationId);
            return View(warehouseAssets);
        }

        // GET: Warehouse/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.WarehouseAssets == null)
            {
                return NotFound();
            }

            var warehouseAssets = await _context.WarehouseAssets
                .Include(w => w.Donation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouseAssets == null)
            {
                return NotFound();
            }

            return View(warehouseAssets);
        }

        // POST: Warehouse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long? id)
        {
            if (_context.WarehouseAssets == null)
            {
                return Problem("Entity set 'DCodeNGOdataNGOsqliteContext.WarehouseAssets'  is null.");
            }
            var warehouseAssets = await _context.WarehouseAssets.FindAsync(id);
            if (warehouseAssets != null)
            {
                _context.WarehouseAssets.Remove(warehouseAssets);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseAssetsExists(long? id)
        {
          return _context.WarehouseAssets.Any(e => e.Id == id);
        }
    }
}
