using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelProject.ViewModels
{
    public class ClientStatusModel
    {
        
        
       



        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Sex { get; set; }
        public string DocumentForm { get; set; }
        public int DocumentsSeries { get; set; }
        public int DocumentsNumber { get; set; }
        public string Phone { get; set; }

       
        public int? RoomId { get; set; }
        public Room Room { get; set; }
        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public DateTime Arrival { get; set; }
        public DateTime Departure { get; set; }
        public decimal Price { get; set; }
        
        

    }
}
