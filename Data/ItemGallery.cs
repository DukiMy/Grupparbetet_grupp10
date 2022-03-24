using HomeFinder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace HomeFinder.Data
{
    public class ItemGallery
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public Item Item { get; set; }
    }
}
