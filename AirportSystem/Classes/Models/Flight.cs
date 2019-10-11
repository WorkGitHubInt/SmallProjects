using System;

namespace AirportSystem.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public string FlightNum { get; set; }
        public string Destination { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ArrivalTime { get; set; }
    }
}
