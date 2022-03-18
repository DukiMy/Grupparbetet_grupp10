using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeFinder.Models;

namespace HomeFinder.ViewModels
{
    public class ItemViewModel
    {
        public List<Item> Items { get; set; }

        public SelectList ItemTypesVM { get; set; }
        public string ItemType { get; set; }

        public SelectList NrOfRoomsVM { get; set; }
        public int minNrOfRooms { get; set; }
        public int maxNrOfRooms { get; set; }

        public SelectList AreaVM { get; set; }
        public string minArea { get; set; }
        public string maxArea { get; set; }
        public SelectList LowerAreaSpan { get; set; }
        public SelectList HigherAreaSpan { get; set; }

        public SelectList PriceVM { get; set; }
        public string minPrice { get; set; }
        public string maxPrice { get; set; }
        public SelectList LowerPriceSpan { get; set; }
        public SelectList HigherPriceSpan { get; set; }




        public string SearchString { get; set; }
       
    }
}
