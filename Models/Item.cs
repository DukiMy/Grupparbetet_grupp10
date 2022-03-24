using HomeFinder.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Fastighetstyp")]
        public string ItemType { get; set; }

        [DisplayName("Upplåtelseform")]
        public string FormOfLease { get; set; }

        [Required]
        [DisplayName("Adress")]
        public string Address { get; set; }

        [DisplayName("Postnummer")]
        public string ZipCode { get; set; }

        //[Required]
        [DisplayName("Stad")]
        public string City { get; set; }

        [Required]
        [DisplayName("Pris")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        [Required]
        [DisplayName("Antal rum")]
        public int NrOfRoom { get; set; }

        [Required]
        [DisplayName("Boarea")]
        public double LivingArea { get; set; }

        [DisplayName("Biarea")]
        public double? GrossFloorArea { get; set; }

        [DisplayName("Tomtarea")]
        public double? PlotArea { get; set; }

        [Required]
        [DisplayName("Byggår")]
        [DataType(DataType.Date)]
        public DateTime ConstructionYear { get; set; }

        [Required]
        [DisplayName("Visningsdatum")]
        [DataType(DataType.Date)]
        public DateTime ListingDate { get; set; }

        public ICollection<Image> itemGallery { get; set; }
        public string MainImageUrl { get; set; }

        public ApplicationUser Broker { get; set; }
        public ICollection<InterestRegistration> InterestRegistrations { get; set; }
    }
}
