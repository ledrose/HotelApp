using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.ViewModels
{
    public class SchedulerGroupModel
    {
        public int Id { set; get; }
        public int Capacity { get; set; }
        public String Type { get; set; }
        public float PriceWeekends { get; set; }
        public float PriceWorkday { get; set; }
        public bool SubgroupStack { get; set; }
    }

}
