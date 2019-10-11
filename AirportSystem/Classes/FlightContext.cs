using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using AirportSystem.Models;

namespace AirportSystem
{
    public static class Extensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> col)
        {
            return new ObservableCollection<T>(col);
        }
    }

    public class FlightContext : DbContext
    {
        public FlightContext() : base("DbConnection")
        {

        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<FlightOnDate> FlightOnDates { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
    }
}
