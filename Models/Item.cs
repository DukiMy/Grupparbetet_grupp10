using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]

        public string Address { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public int NrOfRoom { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double LivingArea { get; set; }

        public double? GrossFloorArea { get; set; }
        public double? PlotArea { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ConstructionYear { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ListingDate { get; set; }

        //public List<User> Intressee { get; set; }

        //public IMG MyProperty { get; set; }

        //public ItemType MyProperty { get; set; }

        // public string FormOfLease { get; set; }
    }
}
