using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportSystem.Models
{
    public class FlightOnDate
    {
        public int Id { get; set; }
        public int? FlightId { get; set; }
        [ForeignKey("FlightId")]
        public virtual Flight Flight { get; set; }
        public DateTime Date { get; set; }
        public int Seats { get; set; }
        public int FreeSeats { get; set; }
    }
}
