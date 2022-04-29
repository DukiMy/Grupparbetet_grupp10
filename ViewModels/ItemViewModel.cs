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

        [Required(ErrorMessage = "Obligatoriskt fält")]
        [DisplayName("Fastighetstyp")]
        public string ItemType { get; set; }
        
        public SelectList ItemTypeList { get; set; }

        [DisplayName("Upplåtelseform")]
        [Required(ErrorMessage = "Obligatoriskt fält")]
        public string FormOfLease { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält")]
        [DisplayName("Adress")]
        [RegularExpression(@"^[#.0-9a-öA-Ö\s,-]+$", ErrorMessage ="Felaktigt format. Exempelvis: Drottninggatan 55")]
        public string Address { get; set; }

        [DisplayName("Postnummer")]
        [Required(ErrorMessage = "Obligatoriskt fält")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "välj 5 siffror")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält")]
        [DisplayName("Stad")]
        [RegularExpression(@"^([a-öA-Ö]+|[a-öA-Ö]+\s[a-öA-Ö]+)$", ErrorMessage ="Vänligen kontrollera stavning")]
        public string City { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält")]
        [DisplayName("Pris")]
        [RegularExpression(@"^\d{0,9}$", ErrorMessage ="Ange i siffror")] //0 går fortf bra
        public int Price { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält")]
        [DisplayName("Antal rum")]
        [RegularExpression(@"^\d{0,2}$", ErrorMessage = "Ange i siffror")] 
        public int NrOfRoom { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält")]
        [StringLength(200)]
        [DisplayName("Beskrivning")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Obligatoriskt fält")]
        [DisplayName("Boarea")]
        [RegularExpression(@"^\d{0,4}$", ErrorMessage = "Ange i siffror")] //funkar med 0
        //[RegularExpression(@"[^0]+\d{0,3}$", ErrorMessage = "Ange i siffror")]
        public double LivingArea { get; set; }

        [DisplayName("Biarea")]
        //[RegularExpression(@"^\d{0,4}$", ErrorMessage = "Ange i siffror")] //funkar med 0
        public double? GrossFloorArea { get; set; }

        [DisplayName("Tomtarea")]
        [RegularExpression(@"^\d{0,6}$", ErrorMessage = "Ange i siffror")]
        public double? PlotArea { get; set; }

        [Required]
        [DisplayName("Byggår")]
        [DataType(DataType.Date)]
        public DateTime ConstructionYear { get; set; }

        [DisplayName("Utlagd")]
        [DataType(DataType.Date)]
        public DateTime ListingDate { get; set; }

        [DisplayName("Visningsdatum")]
        [DataType(DataType.Date)]
        public DateTime ShowingDate { get; set; }

        [Display(Name = "Välj profilfoto till ditt objekt")]
        //[Required(ErrorMessage = "Obligatoriskt fält")]
        public IFormFile MainPhoto { get; set; }

        public string MainImageUrl { get; set; }

        [Display(Name = "Välj bilder till ditt objekt")]
        //[Required(ErrorMessage = "Obligatoriskt fält")]
        public IFormFileCollection ImageFiles { get; set; }

        public List<Image> Images { get; set; }

        //public List<InterestRegistrationViewModel> InterestRegistrations { get; set; }

        public string BrokerFirstName { get; set; }
        public string BrokerLastName { get; set; }
        public string BrokerEmail { get; set; }
    
    }
}
