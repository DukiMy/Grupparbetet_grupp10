using HomeFinder.Models;
using HomeFinder.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Repository
{
    public interface IItemRepository
    {
        public int AddNewItemFromModel(ItemViewModel model, ApplicationUser Broker);

        public Task<ItemViewModel> GetItemById(int? id);

        public IQueryable<ItemViewModel> GetAllItemsAsModel();

        public Task DeleteById(int id);
        public bool ItemExists(int id);

        public Task Update(ItemViewModel itemModel, ApplicationUser broker);

    }
}
