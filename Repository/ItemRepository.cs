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
    public class ItemRepository : IItemRepository
    {
        private readonly HomeFinderContext _context = null;
        public ItemRepository(HomeFinderContext context)
        {
            _context = context;
        }

        public int AddNewItemFromModel(ItemViewModel model, ApplicationUser broker)
        {
            var newItem = new Item()
            {
                ItemType = model.ItemType,
                FormOfLease = model.FormOfLease,
                Address = model.Address,
                ZipCode = model.ZipCode,
                Price = model.Price,
                NrOfRoom = model.NrOfRoom,
                Description = model.Description,
                LivingArea = model.LivingArea,
                GrossFloorArea = model.GrossFloorArea,
                PlotArea = model.PlotArea,
                ConstructionYear = model.ConstructionYear,
                ListingDate = model.ListingDate,
                MainImageUrl = model.MainImageUrl,
                Broker = broker
            };

            newItem.itemGallery = new List<Image>();

            foreach (var file in model.Images)
            {
                newItem.itemGallery.Add(new Image()
                {
                    Title = file.Title,
                    URL = file.URL
                });
            }
            _context.Item.Add(newItem);
            _context.SaveChanges();

            return newItem.Id;
        }

        public async Task<ItemViewModel> GetItemById(int? id)
        {
            return await _context.Item.Where(x => x.Id == id)
                 .Select(item => new ItemViewModel()
                 {
                     ItemType = item.ItemType,
                     Address = item.Address,
                     Price = item.Price,
                     City = item.City,
                     NrOfRoom = item.NrOfRoom,
                     Description = item.Description,
                     LivingArea = item.LivingArea,
                     GrossFloorArea = item.GrossFloorArea,
                     PlotArea = item.PlotArea,
                     ConstructionYear = item.ConstructionYear,
                     ListingDate = item.ListingDate,
                     MainImageUrl = item.MainImageUrl,
                     BrokerFirstName = item.Broker.FirstName,
                     BrokerLastName = item.Broker.LastName,
                     BrokerEmail = item.Broker.Email,
                     Images = item.itemGallery.ToList(),               
                 }).FirstOrDefaultAsync();
        }

        public IQueryable<ItemViewModel> GetAllItemsAsModel()
        {
            return _context.Item.Include(item => item.itemGallery).Include(item => item.Broker)
            .Select(item => new ItemViewModel()
            {
                Id = item.Id,
                ItemType = item.ItemType,
                Address = item.Address,
                Price = item.Price,
                City = item.City,
                NrOfRoom = item.NrOfRoom,
                Description = item.Description,
                LivingArea = item.LivingArea,
                GrossFloorArea = item.GrossFloorArea,
                PlotArea = item.PlotArea,
                ConstructionYear = item.ConstructionYear,
                ListingDate = item.ListingDate,
                BrokerFirstName = item.Broker.FirstName,
                BrokerLastName = item.Broker.LastName,
                BrokerEmail = item.Broker.Email,
                MainImageUrl = item.MainImageUrl,
                Images = item.itemGallery.ToList()
            }); 
        }

        public async Task DeleteById(int id)
        {
            var item = await _context.Item.FindAsync(id);
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ItemViewModel itemModel, ApplicationUser broker)
        {
            var item = new Item()
            {
                Id = itemModel.Id,
                ItemType = itemModel.ItemType,
                FormOfLease = itemModel.FormOfLease,
                City = itemModel.City,
                Address = itemModel.Address,
                ZipCode = itemModel.ZipCode,
                Price = itemModel.Price,
                NrOfRoom = itemModel.NrOfRoom,
                Description = itemModel.Description,
                LivingArea = itemModel.LivingArea,
                GrossFloorArea = itemModel.GrossFloorArea,
                PlotArea = itemModel.PlotArea,
                ConstructionYear = itemModel.ConstructionYear,
                ListingDate = itemModel.ListingDate,
                MainImageUrl = itemModel.MainImageUrl,
                Broker = broker
            };

            item.itemGallery = new List<Image>();

            if (itemModel.Images != null)
            {
                foreach (var file in itemModel.Images)
                {
                    item.itemGallery.Add(new Image()
                    {
                        Title = file.Title,
                        URL = file.URL
                    });
                }
            }

            _context.Update(item);
            await _context.SaveChangesAsync();
        }

        public bool ItemExists(int id)
        {
            return _context.Item.Any(e => e.Id == id);
        }
    }
}
