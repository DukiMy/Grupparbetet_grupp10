using HomeFinder.Data;
using HomeFinder.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HomeFinder.ViewModels
{
   
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Fastighetstyp")]
        public string ItemType { get; set; }
        public SelectList ItemTypeList { get; set; }

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

        public string Lat { get; set; }
        public string Lng { get; set; }

        [Required]
        [DisplayName("Pris")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        //[Required]
        [DisplayName("Antal rum")]
        public int NrOfRoom { get; set; }

        //[Required]
        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        //[Required]
        [DisplayName("Boarea")]
        public double LivingArea { get; set; }

        [DisplayName("Biarea")]
        public double? GrossFloorArea { get; set; }

        [DisplayName("Tomtarea")]
        public double? PlotArea { get; set; }

        //[Required]
        [DisplayName("Byggår")]
        [DataType(DataType.Date)]
        public DateTime ConstructionYear { get; set; }

        //[Required]
        [DisplayName("Visnings datum")]
        [DataType(DataType.Date)]
        public DateTime ListingDate { get; set; }

        public IFormFile MainPhoto { get; set; }

        public string MainImageUrl { get; set; }

        [Display(Name = "Välj bilder till ditt objekt")]
        //[Required]
        public IFormFileCollection ImageFiles { get; set; }

        public List<Image> Images { get; set; }

        //public List<InterestRegistrationViewModel> InterestRegistrations { get; set; }

        public string BrokerFirstName { get; set; }
        public string BrokerLastName { get; set; }
        public string BrokerEmail { get; set; }
    }
}
