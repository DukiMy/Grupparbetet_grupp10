using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeFinder.Models
{
    public class ItemType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        ICollection<Item> Items { get; set; }
    }
}
