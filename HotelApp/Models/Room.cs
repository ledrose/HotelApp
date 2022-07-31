using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public float PriceWorkday { get; set; }
        [Required]
        public float PriceWeekends { get; set; }
        [Required]
        [Range(0,2,ErrorMessage = "Needs to be between 0 and 2")]
        public int SpotNumber { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "Needs to be between 1 and 3")]
        public int Type { get; set; }
        public String Description { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }


        public String ByteImage { get; set; }
        public String SourceFileName { get; set; }
        public String ContentType { get; set; }


        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
