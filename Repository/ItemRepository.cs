using HomeFinder.Data;
using HomeFinder.Models;
using HomeFinder.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Repository
{
    public class ItemRepository
    {
        private readonly HomeFinderContext _context = null;
        public ItemRepository(HomeFinderContext context)
        {
            _context = context;
        }
        public int AddNewItem(Item model)
        {
            var newItem = new Item()
            {
                ItemType = model.ItemType,
                Address = model.Address,
                Price = model.Price,
                NrOfRoom = model.NrOfRoom,
                Description = model.Description,
                LivingArea = model.LivingArea,
                GrossFloorArea = model.GrossFloorArea,
                PlotArea = model.PlotArea,
                ConstructionYear = model.ConstructionYear,
                ListingDate = model.ListingDate,
                MainImageUrl = model.MainImageUrl,
                //Broker = model.Broker
            };

            newItem.itemGallery = new List<ItemGallery>();

            foreach (var file in model.Gallery)
            {
                newItem.itemGallery.Add(new ItemGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }
            _context.Item.Add(newItem);
            _context.SaveChanges();

            return newItem.Id;
        }

        public async Task<Item> GetItemById(int id)
        {
            return await _context.Item.Where(x => x.Id == id)
                 .Select(item => new Item()
                 {
                     ItemType = item.ItemType,
                     Address = item.Address,
                     Price = item.Price,
                     NrOfRoom = item.NrOfRoom,
                     Description = item.Description,
                     LivingArea = item.LivingArea,
                     GrossFloorArea = item.GrossFloorArea,
                     PlotArea = item.PlotArea,
                     ConstructionYear = item.ConstructionYear,
                     ListingDate = item.ListingDate,
                     MainImageUrl = item.MainImageUrl,
                     //Broker = item.Broker,
                     Gallery = item.itemGallery.Select(g => new GalleryModel()
                     {
                         Id = g.Id,
                         Name = g.Name,
                         URL = g.URL
                     }).ToList(),               
                 }).FirstOrDefaultAsync();
        }

        public IQueryable<Item> GetAllItems()
        {
            return _context.Item;
                  //.Select(item => new Item()
                  // {
                  //     ItemType = item.ItemType,
                  //     Address = item.Address,
                  //     Price = item.Price,
                  //     NrOfRoom = item.NrOfRoom,
                  //     Description = item.Description,
                  //     LivingArea = item.LivingArea,
                  //     GrossFloorArea = item.GrossFloorArea,
                  //     PlotArea = item.PlotArea,
                  //     ConstructionYear = item.ConstructionYear,
                  //     ListingDate = item.ListingDate,
                  //     MainImageUrl = item.MainImageUrl
                  // });
        }

        //public IQueryable<Item> GetAllItemsAsModel()
        //{
        //    return _context.Item
        //    .Select(item => new CreateItemVM()
        //     {
        //         Id = item.Id,
        //         ItemType = item.ItemType,
        //         Address = item.Address,
        //         Price = item.Price,
        //         NrOfRoom = item.NrOfRoom,
        //         Description = item.Description,
        //         LivingArea = item.LivingArea,
        //         GrossFloorArea = item.GrossFloorArea,
        //         PlotArea = item.PlotArea,
        //         ConstructionYear = item.ConstructionYear,
        //         ListingDate = item.ListingDate,
        //        Images = item.itemGallery.Where(i => i.Item.Id == item.Id).ToList()
        //    });
        //}
    }
}
