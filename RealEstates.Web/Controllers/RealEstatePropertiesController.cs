using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services;

namespace RealEstates.Web.Controllers
{
    public class RealEstatePropertiesController : Controller
    {
        private readonly RealEstateDbContext _context;
        private readonly IPropertiesService propertiService;

        public RealEstatePropertiesController(RealEstateDbContext context,IPropertiesService ps)
        {
            _context = context;
            this.propertiService = ps;
        }
        public async Task<IActionResult> Search()
        {
              return View();
        }

        public async Task<IActionResult> DoSearch(int minPrice,int maxPrice)
        {
            var properties=this.propertiService.SearchByPrice(minPrice,maxPrice);
            return View(properties);
        }


        // GET: RealEstateProperties
        public async Task<IActionResult> Index()
        {
            var realEstateDbContext = _context.RealEstateProperties.Include(r => r.District).Include(r => r.PropertyType).Include(r => r.TypeOfBuilding);
            return View(await realEstateDbContext.ToListAsync());
        }


        // GET: RealEstateProperties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstateProperty = await _context.RealEstateProperties
                .Include(r => r.District)
                .Include(r => r.PropertyType)
                .Include(r => r.TypeOfBuilding)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realEstateProperty == null)
            {
                return NotFound();
            }

            return View(realEstateProperty);
        }

        // GET: RealEstateProperties/Create
        public IActionResult Create()
        {
            ViewData["DistrictId"] = new SelectList(_context.Districtss, "Id", "Name");
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyTypes, "Id", "Name");
            ViewData["TypeOfBuildingId"] = new SelectList(_context.BuildingsTypes, "Id", "Name");
            return View();
        }

        // POST: RealEstateProperties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Size,Floor,TotalNumbersOfFloors,DistrictId,Year,TypeOfBuildingId,PropertyTypeId,Price")] RealEstateProperty realEstateProperty)
        {
            if (ModelState.IsValid)
            {
                _context.Add(realEstateProperty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DistrictId"] = new SelectList(_context.Districtss, "Id", "Name", realEstateProperty.DistrictId);
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyTypes, "Id", "Name", realEstateProperty.PropertyTypeId);
            ViewData["TypeOfBuildingId"] = new SelectList(_context.BuildingsTypes, "Id", "Name", realEstateProperty.TypeOfBuildingId);
            return View(realEstateProperty);
        }

        // GET: RealEstateProperties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstateProperty = await _context.RealEstateProperties.FindAsync(id);
            if (realEstateProperty == null)
            {
                return NotFound();
            }
            ViewData["DistrictId"] = new SelectList(_context.Districtss, "Id", "Name", realEstateProperty.DistrictId);
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyTypes, "Id", "Name", realEstateProperty.PropertyTypeId);
            ViewData["TypeOfBuildingId"] = new SelectList(_context.BuildingsTypes, "Id", "Name", realEstateProperty.TypeOfBuildingId);
            return View(realEstateProperty);
        }

        // POST: RealEstateProperties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Size,Floor,TotalNumbersOfFloors,DistrictId,Year,TypeOfBuildingId,PropertyTypeId,Price")] RealEstateProperty realEstateProperty)
        {
            if (id != realEstateProperty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realEstateProperty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealEstatePropertyExists(realEstateProperty.Id))
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
            ViewData["DistrictId"] = new SelectList(_context.Districtss, "Id", "Name", realEstateProperty.DistrictId);
            ViewData["PropertyTypeId"] = new SelectList(_context.PropertyTypes, "Id", "Name", realEstateProperty.PropertyTypeId);
            ViewData["TypeOfBuildingId"] = new SelectList(_context.BuildingsTypes, "Id", "Name", realEstateProperty.TypeOfBuildingId);
            return View(realEstateProperty);
        }

        // GET: RealEstateProperties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstateProperty = await _context.RealEstateProperties
                .Include(r => r.District)
                .Include(r => r.PropertyType)
                .Include(r => r.TypeOfBuilding)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realEstateProperty == null)
            {
                return NotFound();
            }

            return View(realEstateProperty);
        }

        // POST: RealEstateProperties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var realEstateProperty = await _context.RealEstateProperties.FindAsync(id);
            if (realEstateProperty != null)
            {
                _context.RealEstateProperties.Remove(realEstateProperty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealEstatePropertyExists(int id)
        {
            return _context.RealEstateProperties.Any(e => e.Id == id);
        }
    }
}
