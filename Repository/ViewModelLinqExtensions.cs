using HomeFinder.Models;
using HomeFinder.ViewModels;
using System.Collections.Generic;

namespace System.Linq
{
    public static class ViewModelLinqExtensions
    {
        public static IQueryable<ItemViewModel> SelectItemAsViewModel(this IQueryable<Item> sequence)
        {
            var itemViewModels = sequence.Select(item => new ItemViewModel()
            {
                Id = item.Id,
                ItemType = item.ItemType.Name,
                FormOfLease = item.FormOfLease,
                Price = item.Price,

                Address = item.Address,
                City = item.City,
                ZipCode = item.ZipCode,

                Description = item.Description,

                NrOfRoom = item.NrOfRoom,
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

            return itemViewModels;
        }
    }
}
