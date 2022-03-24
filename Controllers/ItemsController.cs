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
using Microsoft.AspNetCore.Identity;
using HomeFinder.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace HomeFinder.Controllers
{
    public class ItemsController : Controller
    {
        //private readonly HomeFinderContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ItemRepository _itemRepository = null;


        public ItemsController(UserManager<ApplicationUser> userManager, ItemRepository itemRepository, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _itemRepository = itemRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> Index(string searchString, string itemType, int nrOfRooms, string minNrOfRooms,
                                               string maxNrOfRooms, string minPrice, string maxPrice, string minArea,
                                               string maxArea, string displayOrder)
        {


            var items = _itemRepository.GetAllItems();
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

            itemVM.Items = SortList(itemVM.Items, displayOrder);


            return View(itemVM);
        }



        // GET: Items/Create


        public IActionResult Create()
        {
            return View();
        }



        //// POST: Items/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,ItemType,Address,Price,NrOfRoom,Description,LivingArea,GrossFloorArea,PlotArea,ConstructionYear,ListingDate")] Item item)
        //{
        //    item.Broker = await _userManager.GetUserAsync(User);

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(item);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    //return BadRequest("Fel vid skapande av item.");
        //    return View(item);
        //}



        //// GET: Items/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var item = await _context.Item.FindAsync(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(item);
        //}




        //// POST: Items/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,ItemType,Address,Price,NrOfRoom,Description,LivingArea,GrossFloorArea,PlotArea,ConstructionYear,ListingDate")] Item item)
        //{
        //    if (id != item.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(item);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ItemExists(item.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(item);
        //}



        //// GET: Items/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var item = await _context.Item
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(item);
        //}



        //// POST: Items/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var item = await _context.Item.FindAsync(id);
        //    _context.Item.Remove(item);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ItemExists(int id)
        //{
        //    return _context.Item.Any(e => e.Id == id);
        //}




        //====================================================

        public List<Item> SortList(List<Item> itemList, string displayOrder)
        {
           // List<Item> sortedList = new();

            if (displayOrder == "Pris_Stigande")
            {
               return itemList.OrderBy(i => i.Price).ToList();

            }

            else if (displayOrder == "Pris_Fallande")
            {
                return itemList.OrderBy(i => i.Price).Reverse().ToList();
            }

            else if (displayOrder == "Datum_Nyast")
            {
               
                return itemList.OrderBy(i => i.ListingDate).Reverse().ToList();

            }

            else if (displayOrder == "Datum_Äldst")
            {
                return itemList.OrderBy(i => i.ListingDate).ToList();
            }



            return itemList;
        }
        public string RemoveChar(string numString)
        {
            if (numString.Contains("<") || numString.Contains(">"))
            {
                numString = numString[1..];
            }

            return numString;
        }


        
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

        //--------------


        public ViewResult AddNewItem(bool isSuccess = false, int itemId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.ItemId = itemId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewItem(Item item)
        {

            if (ModelState.IsValid)
            {
                if (item.MainPhoto != null)
                {
                    string folder = "img/";
                    item.MainImageUrl = await UploadImage(folder, item.MainPhoto);
                }

                if (item.GalleryFiles != null)
                {
                    string folder = "img/";

                    item.Gallery = new List<GalleryModel>();

                    foreach (var file in item.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        item.Gallery.Add(gallery);
                    }
                }

                int id = _itemRepository.AddNewItem(item);
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewItem), new { isSuccess = true, itemId = id });
                }       

            }
            return View();
        }

        [Route("all-items")]
        public ViewResult GetAllItems()
        {
            var data = _itemRepository.GetAllItems();

            return View(data);
        }

        [Route("item-details/{id:int:min(1)}", Name = "itemDetailsRoute")]
        public async Task<ViewResult> GetItem(int id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            var data = await _itemRepository.GetItemById(id);

            return View(data);
        }


        //// GET: Items/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    var item = await _context.Item.Include(i => i.Broker)
        //.FirstOrDefaultAsync(m => m.Id == id);
           
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }



        //    return View(item);
        //}

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }


    }
}
