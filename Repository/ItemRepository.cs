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

        // ---- Konverterar datan från databasen till en struktur och ett format som sedan accepteras av views och controllers
        public async Task<ItemViewModel> GetItemById(int? id)
        {
            return await _context.Items
                .Include(item => item.ItemType)
                .Include(item => item.itemGallery)
                .Include(item => item.Broker)
                .Where(item => item.Id == id)
                .SelectItemAsViewModel()
                .FirstOrDefaultAsync();
        }

        public IQueryable<ItemViewModel> GetAllItemsAsModel()
        {
            return _context.Items
                .Include(item => item.itemGallery)
                .Include(item => item.Broker)
                .Include(item => item.ItemType)
                .SelectItemAsViewModel();
        }

        public int AddNewItemFromModel(ItemViewModel model, ApplicationUser broker)
        {
            var itemType = GetItemType(model.ItemType);
            Item item = ViewModelToModelConverter.CreateItem(model, itemType, broker); 
            _context.Items.Add(item);
            _context.SaveChanges();

            return item.Id;
        }

        public async Task Update(ItemViewModel model, ApplicationUser broker)
        {
            var itemType = GetItemType(model.ItemType);
            Item item = ViewModelToModelConverter.CreateItem(model, itemType, broker);

            _context.Update(item);
            
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
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

        private ItemType GetItemType(string itemType)
        {
            return _context.ItemTypes.FirstOrDefault(it => it.Name == itemType);
        }
    }
}
