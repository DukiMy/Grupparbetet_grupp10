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

        
        public async Task<IActionResult> Index(string searchString, string itemType, int nrOfRooms, string minNrOfRooms, string maxNrOfRooms, string minPrice, string maxPrice, string minArea, string maxArea)
        {


            var items = from i in _context.Item
                        select i;
            // Use LINQ to get list of genres.
            IQueryable<string> itemTypeQuery = from i in items
                                               orderby i.ItemType
                                               select i.ItemType;



            IQueryable<int> nrOfRoomsQuery = from i in items
                                             orderby i.NrOfRoom
                                             select i.NrOfRoom;

            IQueryable<double> areaQuery = from i in items
                                             orderby i.LivingArea
                                             select i.LivingArea;

            IQueryable<decimal> priceQuery = from i in items
                                           orderby i.Price
                                           select i.Price;

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
                minPrice = RemoveChar(minPrice);
                var min = decimal.Parse(minPrice);
                items = items.Where(i => i.Price >= min);
            }

            if (!string.IsNullOrEmpty(maxPrice))
            {
                maxPrice = RemoveChar(maxPrice);
                var max = decimal.Parse(maxPrice);
                items = items.Where(i => i.Price <= max);
            }

            if (!string.IsNullOrEmpty(minArea))
            {
                minArea = RemoveChar(minArea);
                var min = double.Parse(minArea);
                items = items.Where(i => i.LivingArea >= min);
            }

            if (!string.IsNullOrEmpty(maxArea))
            {
                maxArea = RemoveChar(maxArea);
                var max = double.Parse(maxArea);
                items = items.Where(i => i.LivingArea <= max);
            }
            
           
            var itemVM = new ItemViewModel
            {

                ItemTypesVM = new SelectList(await itemTypeQuery.Distinct().ToListAsync()),
                NrOfRoomsVM = new SelectList(await nrOfRoomsQuery.Distinct().ToListAsync()),
                AreaVM = new SelectList(await areaQuery.Distinct().ToListAsync()),
               
                PriceVM = new SelectList(await priceQuery.Distinct().ToListAsync()),


                Items = await items.ToListAsync()
            };


            //(itemVM.LowerAreaSpan, itemVM.HigherAreaSpan) = SetAreaSpan((IQueryable<int>)areaQuery, 25);
            //(itemVM.LowerPriceSpan, itemVM.HigherPriceSpan) = SetPriceSpan((IQueryable<int>)priceQuery, 250000);

            (itemVM.LowerAreaSpan, itemVM.HigherAreaSpan) = SetAreaSpan(areaQuery, 25);
            (itemVM.LowerPriceSpan, itemVM.HigherPriceSpan) = SetPriceSpan(priceQuery, 250000);


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

        //====================================================

        public string RemoveChar(string numString)
        {
            if (numString.Contains("<") || numString.Contains(">"))
            {
                numString = numString[1..];
            }

            return numString;
        }


        //public (SelectList, SelectList) SetSpan(IQueryable<int> areaQuery, int step)
        //{
        //    List<string> lowerAreaSpanQuery = new();
        //    List<string> higherAreaSpanQuery = new();

        //    foreach (int area in areaQuery)
        //    {
        //        for (int i = 0; i < area; i += step)
        //        {
        //            if (area <= i + step)
        //            {
        //                lowerAreaSpanQuery.Add(i.ToString());
        //                higherAreaSpanQuery.Add((i + step).ToString());
        //            }
        //        }
        //    }

        //    return (new SelectList(lowerAreaSpanQuery.Distinct()), new SelectList(higherAreaSpanQuery.Distinct()));
        //}
        public (SelectList, SelectList) SetAreaSpan(IQueryable<double> areaQuery, int step)
        {
            List<string> lowerAreaSpanQuery = new();
            List<string> higherAreaSpanQuery = new();

            foreach (int area in areaQuery)
            {
                for (int i = 0; i < area; i += step)
                {
                    if (area <= i + step)
                    {
                        lowerAreaSpanQuery.Add(i.ToString());
                        higherAreaSpanQuery.Add((i + step).ToString());
                    }
                }
            }

            return (new SelectList(lowerAreaSpanQuery.Distinct()), new SelectList(higherAreaSpanQuery.Distinct()));
        }
        public (SelectList, SelectList) SetPriceSpan(IQueryable<decimal> areaQuery, int step)
        {
            List<string> lowerAreaSpanQuery = new();
            List<string> higherAreaSpanQuery = new();

            foreach (int area in areaQuery)
            {
                for (int i = 0; i < area; i += step)
                {
                    if (area <= i + step)
                    {
                        lowerAreaSpanQuery.Add(i.ToString());
                        higherAreaSpanQuery.Add((i + step).ToString());
                    }
                }
            }

            return (new SelectList(lowerAreaSpanQuery.Distinct()), new SelectList(higherAreaSpanQuery.Distinct()));
        }

        
    }
}
