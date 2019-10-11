using System;
using System.Windows;
using AirportSystem.Models;
using System.Linq;

namespace AirportSystem
{
    public partial class AddFlightWindow : Window
    {
        public AddFlightWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (FlightContext db = new FlightContext())
            {
                string[] iata = new string[] { "SU", "UT", "U6", "ZF", "B2", "BT" };
                bool chk = false;
                foreach (string code in iata)
                {
                    if (code == FlightNumText.Text.Substring(0,2))
                    {
                        chk = true;
                    }
                }
                if (chk)
                {
                    if (int.TryParse(FlightNumText.Text.Substring(2), out int num))
                    {
                        try
                        {
                            var flightCheck = db.Flights.FirstOrDefault(f => f.FlightNum == FlightNumText.Text);
                            if (flightCheck == null)
                            {
                                var flight = new Flight
                                {
                                    FlightNum = FlightNumText.Text,
                                    Destination = DestinationText.Text,
                                    DepartureTime = new TimeSpan(Convert.ToInt32(DepartureTimeText.Text.Split(':')[0]), Convert.ToInt32(DepartureTimeText.Text.Split(':')[1]), 0),
                                    ArrivalTime = new TimeSpan(Convert.ToInt32(ArrivalTimeText.Text.Split(':')[0]), Convert.ToInt32(ArrivalTimeText.Text.Split(':')[1]), 0),
                                };
                                db.Flights.Add(flight);
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
                    } else
                    {
                        MessageBox.Show("Неверно указан номер рейса!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                } else
                {
                    MessageBox.Show("Неверно указан IATA код!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new IATAWindow();
            window.Show();
        }
    }
}
