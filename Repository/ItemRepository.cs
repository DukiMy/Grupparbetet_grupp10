using HomeFinder.Data;
using HomeFinder.Models;
using HomeFinder.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // ---- Konverterar datan från databasen till en struktur och ett format som sedan accepterars av views och controllers

        public int AddNewItemFromModel(ItemViewModel model, ApplicationUser broker)
        {
            var itemType = _context.ItemTypes.FirstOrDefault(it => it.Name == model.ItemType);

            var newItem = new Item()
            {
                ItemType = itemType,
                FormOfLease = model.FormOfLease,
                Address = model.Address,
                ZipCode = model.ZipCode,
                City = model.City,
                Lat = model.Lat,
                Lng = model.Lng,
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
            _context.Items.Add(newItem);
            _context.SaveChanges();

            return newItem.Id;
        }

        public async Task<ItemViewModel> GetItemById(int? id)
        {
            return await _context.Items.Include(i => i.ItemType).Where(x => x.Id == id)
                 .Select(item => new ItemViewModel()
                 {
                     ItemType = item.ItemType.Name,
                     Address = item.Address,
                     ZipCode = item.ZipCode,
                     Price = item.Price,
                     City = item.City,
                     Lat = item.Lat,
                     Lng = item.Lng,
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
            return _context.Items
                .Include(item => item.itemGallery)
                .Include(item => item.Broker)
                .Include(item => item.ItemType)
            .Select(item => new ItemViewModel()
            {
                Id = item.Id,
                ItemType = item.ItemType.Name,
                Address = item.Address,
                Price = item.Price,
                City = item.City,
                ZipCode = item.ZipCode,
                Lat = item.Lat,
                Lng = item.Lng,
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
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task Update(ItemViewModel itemModel, ApplicationUser broker)
        {
            var itemType = _context.ItemTypes.FirstOrDefault(it => it.Name == itemModel.ItemType);

            var item = new Item()
            {
                Id = itemModel.Id,
                ItemType = itemType,
                FormOfLease = itemModel.FormOfLease,
                City = itemModel.City,
                Address = itemModel.Address,
                ZipCode = itemModel.ZipCode,
                Lat = itemModel.Lat,
                Lng = itemModel.Lng,
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

        public SelectList GetItemTypeSelectList()
        {
            IQueryable<string> itemTypeQuery = _context.ItemTypes.OrderBy(s => s.Name).Select(s => s.Name);
            return new SelectList(itemTypeQuery.Distinct().ToList());
        }

        public bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
