using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.ViewModels
{
    public class ReservationsStatusModel
    {

       
        public int? StatusId { get; set; }
        public Status StatusRef { get; set; }
        public string ClientName { get; set; }
        public string Phone { get; set; }
        public string PersonNumber { get; set; }


        public int? RoomId { get; set; }
        public Room Room { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public decimal Price { get; set; }

    }
}
