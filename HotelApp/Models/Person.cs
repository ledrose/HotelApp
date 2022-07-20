using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public int age { get; set; }

        public virtual IEnumerable<Reservation> Reservations { get; set; }
    }
}
