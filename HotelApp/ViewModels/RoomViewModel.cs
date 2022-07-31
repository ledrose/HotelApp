using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.ViewModels
{ 
    public class RoomViewModel
    {
        public int Id { get; set; }
        [Required]
        public float PriceWorkday { get; set; }
        [Required]
        public float PriceWeekends { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "Needs to be between 0 and 2")]
        public int SpotNumber { get; set; }
        [Required]
        [Range(0, 2, ErrorMessage = "Needs to be between 1 and 3")]
        public int Type { get; set; }
        public String Description { get; set; }
        
        public IFormFile Image { get; set; }
    }
}
