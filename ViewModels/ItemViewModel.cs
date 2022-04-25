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

        [Required(ErrorMessage = "Du måste välja fastighetstyp")]
        [DisplayName("Fastighetstyp")]
        public string ItemType { get; set; }
        
        public SelectList ItemTypeList { get; set; }

        [DisplayName("Upplåtelseform")]
        [Required(ErrorMessage = "Du måste välja upplåtelseform")]
        public string FormOfLease { get; set; }

        [Required(ErrorMessage = "Du måste välja adress")]
        [DisplayName("Adress")]
        //[RegularExpression(@"^[0-9]+\s+([a-öA-Ö]+|[a-öA-Ö]+\s[a-öA-Ö]+)$")]
        //[RegularExpression(@"^([a-öA-Ö]+|[a-öA-Ö]+\s[a-öA-Ö]+)+\s[0-9]$")] funkar med adress 1
        public string Address { get; set; }

        [DisplayName("Postnummer")]
        [Required(ErrorMessage = "Du måste välja postnummer")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "välj 5 siffror")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Du måste välja stad")]
        [DisplayName("Stad")]
        [RegularExpression(@"^([a-öA-Ö]+|[a-öA-Ö]+\s[a-öA-Ö]+)$")]
        public string City { get; set; }

        [Required(ErrorMessage = "Du måste välja pris")]
        [DisplayName("Pris")]
        [RegularExpression(@"^\d{0,9}$")] //0 går fortf bra
        public int Price { get; set; }

        [Required(ErrorMessage = "Du måste välja antal rum")]
        [DisplayName("Antal rum")]
        [RegularExpression(@"^\d{0,2}$")] 
        public int NrOfRoom { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Du måste välja boarea")]
        [DisplayName("Boarea")]
        [RegularExpression(@"^\d{0,3}$")]
        public double LivingArea { get; set; }

        [DisplayName("Biarea")]
        [RegularExpression(@"^\d{0,2}$")]
        public double? GrossFloorArea { get; set; }

        [DisplayName("Tomtarea")]
        [RegularExpression(@"^\d{0,3}$")]
        public double? PlotArea { get; set; }

        [Required]
        [DisplayName("Byggår")]
        [DataType(DataType.Date)]
        public DateTime ConstructionYear { get; set; }

        [Required]
        [DisplayName("Utlagd")]
        [DataType(DataType.Date)]
        public DateTime ListingDate { get; set; }

        [DisplayName("Visningsdatum")]
        [DataType(DataType.Date)]
        public DateTime ShowingDate { get; set; }

        [Display(Name = "Välj profilfoto till ditt objekt")]
        [Required]
        public IFormFile MainPhoto { get; set; }

        public string MainImageUrl { get; set; }

        [Display(Name = "Välj bilder till ditt objekt")]
        [Required]
        public IFormFileCollection ImageFiles { get; set; }

        public List<Image> Images { get; set; }

        //public List<InterestRegistrationViewModel> InterestRegistrations { get; set; }

        public string BrokerFirstName { get; set; }
        public string BrokerLastName { get; set; }
        public string BrokerEmail { get; set; }
    
    }
}
