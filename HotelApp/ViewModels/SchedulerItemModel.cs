using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.ViewModels
{
    public class SchedulerItemModel
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Editable { get; set; }
        public int Group { get; set; }
        public String Content { get; set; }
        public int UserId { get; set; }
        public String ClassName { get; set; }
    }
}
