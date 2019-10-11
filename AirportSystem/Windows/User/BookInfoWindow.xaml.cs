using System.Windows;
using AirportSystem.Models;

namespace AirportSystem
{
    public partial class BookInfoWindow : Window
    {
        public BookInfoWindow()
        {
            InitializeComponent();
        }

        public Passenger Passenger;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PassengerTable.DataContext = Passenger;
            TicketTable.DataContext = Passenger.FlightOnDate;
        }
    }
}
