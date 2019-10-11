using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirportSystem.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public int PassportNum { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MidName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Seat { get; set; }
        public int FlightOnDateId { get; set; }
        [ForeignKey("FlightOnDateId")]
        public virtual FlightOnDate FlightOnDate { get; set; }
    }
}
