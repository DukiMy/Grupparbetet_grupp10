using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeFinder.Models
{
    public class Recommendation
    {
        public int? Id { get; set; }
        public int Cue { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }

   
}
