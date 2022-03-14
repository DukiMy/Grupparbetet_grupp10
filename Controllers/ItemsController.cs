using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeFinder.Data;
using HomeFinder.Models;
using HomeFinder.ViewModels;

namespace HomeFinder.Controllers
{
    public class ItemsController : Controller
    {
        private readonly HomeFinderContext _context;

        public ItemsController(HomeFinderContext context)
        {
            _context = context;
        }

        //public async Task<IActionResult> Index(string minNrOfRooms, string maxNrOfRooms, string searchString)
        //{
        //    //var items = _context.Item.Select(i => i);
        //    var items = from i in _context.Item select i;

        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        items = items.Where(i => i.Address.Contains(searchString) || i.Description.Contains(searchString) || i.City.Contains(searchString));
        //    }

        //    if (!string.IsNullOrEmpty(minNrOfRooms))
        //    {
        //        var min = int.Parse(minNrOfRooms);
        //        items = items.Where(i => i.NrOfRoom >= min);
        //    }

        //    if (!string.IsNullOrEmpty(maxNrOfRooms))
        //    {
        //        var max = int.Parse(maxNrOfRooms);
        //        items = items.Where(i => i.NrOfRoom <= max);
        //    }

        //    var itemVM = new ItemViewModel
        //    {

        //        Items = await items.ToListAsync()
        //    };


        //    // Use LINQ to get list of genres.
        //    //IQueryable<string> itmeTypeQuery = from i in _context.Item
        //    //                                   orderby i.ItemType
        //    //                                   select i.ItemType;

        //    //var items = from i in _context.Item
        //    //            select i;

        //    //if (!string.IsNullOrEmpty(searchString))
        //    //{
        //    //    items = items.Where(i => i.Address.Contains(searchString));
        //    //}

        //    //if (!string.IsNullOrEmpty(itemType))
        //    //{
        //    //    items = items.Where(i => i.ItemType == itemType);
        //    //}

        //    //var itemVM = new ItemViewModel
        //    //{
        //    //    ItemTypesVM = new SelectList(await itmeTypeQuery.Distinct().ToListAsync()),
        //    //    Items = await items.ToListAsync()
        //    //};



        //    //IQueryable<int> nrOfRoomsQuery = from i in _context.Item
        //    //                                 orderby i.NrOfRoom
        //    //                                 select i.NrOfRoom;



        //    //items = items.Where(i => i.NrOfRoom == nrOfRooms);

        //    //itemVM.NrOfRoomsVM = new SelectList(await nrOfRoomsQuery.Distinct().ToListAsync());
        //    //itemVM.Items = await items.ToListAsync();



        //    return View(itemVM);
        //}


        public async Task<IActionResult> Index(string searchString, string itemType, string minNrOfRooms, string maxNrOfRooms, string minPrice, string maxPrice, string minArea, string maxArea)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> itmeTypeQuery = from i in _context.Item
                                               orderby i.ItemType
                                               select i.ItemType;

            var items = from i in _context.Item
                        select i;

            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Address.Contains(searchString) || i.City.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(itemType))
            {
                items = items.Where(i => i.ItemType == itemType);
            }

            if (!string.IsNullOrEmpty(minNrOfRooms))
            {
                var min = int.Parse(minNrOfRooms);
                items = items.Where(i => i.NrOfRoom >= min);
            }

            if (!string.IsNullOrEmpty(maxNrOfRooms))
            {
                var max = int.Parse(maxNrOfRooms);
                items = items.Where(i => i.NrOfRoom <= max);
            }

            if (!string.IsNullOrEmpty(minPrice))
            {
                var min = decimal.Parse(minPrice);
                items = items.Where(i => i.Price >= min);
            }

            if (!string.IsNullOrEmpty(maxPrice))
            {
                var max = decimal.Parse(maxPrice);
                items = items.Where(i => i.Price <= max);
            }

            if (!string.IsNullOrEmpty(minArea))
            {
                var min = double.Parse(minArea);
                items = items.Where(i => i.LivingArea >= min);
            }

            if (!string.IsNullOrEmpty(maxArea))
            {
                var max = double.Parse(maxArea);
                items = items.Where(i => i.LivingArea <= max);
            }


            var itemVM = new ItemViewModel
            {
                ItemTypesVM = new SelectList(await itmeTypeQuery.Distinct().ToListAsync()),
                Items = await items.ToListAsync()
            };




            //IQueryable<int> nrOfRoomsQuery = from i in _context.Item
            //                                 orderby i.NrOfRoom
            //                                 select i.NrOfRoom;



            //items = items.Where(i => i.NrOfRoom == nrOfRooms);

            //itemVM.NrOfRoomsVM = new SelectList(await nrOfRoomsQuery.Distinct().ToListAsync());
            //itemVM.Items = await items.ToListAsync();



            return View(itemVM);
        }




        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItemType,Address,Price,NrOfRoom,Description,LivingArea,GrossFloorArea,PlotArea,ConstructionYear,ListingDate")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ItemType,Address,Price,NrOfRoom,Description,LivingArea,GrossFloorArea,PlotArea,ConstructionYear,ListingDate")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }
}
