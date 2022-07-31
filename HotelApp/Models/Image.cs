using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public byte[] ByteImage { get; set; }
        public String SourceFileName { get; set; }
        public String ContentType { get; set; }

        public virtual Room Room { get; set; }
    }
}
