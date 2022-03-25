using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DisplayName("Bild-titel")]
        public string URL { get; set; }
        public Item Item { get; set; }

    }
}
