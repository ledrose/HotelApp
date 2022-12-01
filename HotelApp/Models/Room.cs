using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
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
        [Range(1,3,ErrorMessage = "Needs to be between 1 and 3")]
        public int SpotNumber { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "Needs to be between 1 and 3")]
        public int Type { get; set; }
        public String Description { get; set; }

        private IFormFile image;
        [NotMapped]
        public IFormFile Image {
            get
            {
                return image;
            }
            set
            {
                if (value == null)
                {
                    image = null;
                }
                else
                {
                    using (var target = new MemoryStream())
                    {

                        value.CopyTo(target);
                        ByteImage = Convert.ToBase64String(target.ToArray());

                    }
                    ContentType = value.ContentType;
                    SourceFileName = Path.GetFileName(value.FileName);
                    image = value;
                }
            }
        }


        public String ByteImage { get; set; }
        public String SourceFileName { get; set; }
        public String ContentType { get; set; }


        public List<Reservation> Reservations { get; set; }

    }
}