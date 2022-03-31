using HomeFinder.Models;
using HomeFinder.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeFinder.Repository
{
    public static class ViewModelToModelConverter
    {
        public static Item CreateItem(ItemViewModel model, ItemType itemType, ApplicationUser broker)
        {
            var newItem = new Item()
            {
                ItemType = itemType,
                Price = model.Price,
                FormOfLease = model.FormOfLease,

                Address = model.Address,
                City = model.City,
                ZipCode = model.ZipCode,

                Description = model.Description,

                NrOfRoom = model.NrOfRoom,
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

            return newItem;
        }
    }
}
