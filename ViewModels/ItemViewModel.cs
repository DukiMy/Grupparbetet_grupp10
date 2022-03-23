using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeFinder.Models;
using Microsoft.CodeAnalysis;

namespace HomeFinder.ViewModels
{
    public class ItemViewModel
    {
        public List<Item> Items { get; set; }
        //public IEnumerable<Item> Items { get; set; }
        public string SearchString { get; set; }

        public SelectList DisplayOrderVM { get; set; } 
        public string DisplayOrder { get; set; }

        public SelectList ItemTypesVM { get; set; }
        public string ItemType { get; set; }

        public SelectList NrOfRoomsVM { get; set; }
        public int MinNrOfRooms { get; set; }
        public int MaxNrOfRooms { get; set; }

        public SelectList AreaVM { get; set; }
        public string MinArea { get; set; }
        public string MaxArea { get; set; }
        public SelectList LowerAreaSpan { get; set; }
        public SelectList HigherAreaSpan { get; set; }

        public SelectList PriceVM { get; set; }
        public string MinPrice { get; set; }
        public string MaxPrice { get; set; }
        public SelectList LowerPriceSpan { get; set; }
        public SelectList HigherPriceSpan { get; set; }

       


    }
}
