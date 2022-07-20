using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public float priceWorkday { get; set; }
        public float priceWeekends { get; set; }
        public int type { get; set; }


        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}
