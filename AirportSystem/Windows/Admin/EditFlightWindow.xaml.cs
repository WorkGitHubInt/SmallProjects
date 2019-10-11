using System;
using System.Linq;
using System.Windows;
using AirportSystem.Models;

namespace AirportSystem
{
    public partial class EditFlightWindow : Window
    {
        public EditFlightWindow()
        {
            InitializeComponent();
        }

        public Flight Flight { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (FlightContext db = new FlightContext())
            {
                try
                {
                    var flightCheck = db.Flights.FirstOrDefault(f => f.FlightNum == FlightNumText.Text && f.FlightNum != Flight.FlightNum);
                    if (flightCheck == null)
                    {
                        var flight = db.Flights.Find(Flight.Id);
                        flight.FlightNum = FlightNumText.Text;
                        flight.Destination = DestinationText.Text;
                        flight.DepartureTime = new TimeSpan(Convert.ToInt32(DepartureTimeText.Text.Split(':')[0]), Convert.ToInt32(DepartureTimeText.Text.Split(':')[1]), 0);
                        flight.ArrivalTime = new TimeSpan(Convert.ToInt32(ArrivalTimeText.Text.Split(':')[0]), Convert.ToInt32(ArrivalTimeText.Text.Split(':')[1]), 0);
                        db.SaveChanges();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Номера рейсов не могут повторяться!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Неверно указано время! Время должно указываться в формате:\"чч:мм\"", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Неверно указано время! Время должно указываться в формате:\"чч:мм\"", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception exp)
                {
                    MessageBox.Show($"Произошла неизвестная ошибка! {exp.Message}", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Table.DataContext = Flight;
        }
    }
}
