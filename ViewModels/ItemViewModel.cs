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
        public SelectList NrOfRoomsVM { get; set; }
        public SelectList MinNrOfRooms { get; set; }
        public SelectList MaxNrOfRooms { get; set; }
        public int Min { get; set; }  
        public int Max { get; set; }
        public string ItemType { get; set; }
        public string SearchString { get; set; }
    }
}
