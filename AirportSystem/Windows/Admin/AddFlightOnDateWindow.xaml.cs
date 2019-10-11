using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using AirportSystem.Models;

namespace AirportSystem
{
    public partial class AddFlightOnDateWindow : Window
    {
        public AddFlightOnDateWindow()
        {
            InitializeComponent();
        }

        private List<Flight> flights;

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
                    bool success = DateTime.TryParse(DateText.Text, out DateTime date);
                    if (success && date > DateTime.Now && date < new DateTime(2020, 1, 1))
                    {
                        success = int.TryParse(SeatsText.Text, out int seats);
                        if (success && seats > 0 && seats <= 300)
                        {
                            var flight = flights[FlightsBox.SelectedIndex];
                            var flightChk = db.FlightOnDates.FirstOrDefault(f => f.Date == date && f.Flight.FlightNum == flight.FlightNum);
                            if (flightChk == null)
                            {
                                var flightOnDate = new FlightOnDate
                                {
                                    FlightId = flight.Id,
                                    Date = date,
                                    Seats = seats,
                                    FreeSeats = seats,
                                };
                                db.FlightOnDates.Add(flightOnDate);
                                db.SaveChanges();
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("На указанную дату уже есть рейс!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        } else
                        {
                            MessageBox.Show("Неверно указано кол-во мест! Кол-во мест должно быть целым положительным числом и н превышать 300!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверно указана! Дата должна указываться в формате:\"дд.мм.гггг\" и быть в диапазоне от текущего момента до 01.01.2020!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show($"Произошла неизвестная ошибка! {exp.Message}", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (FlightContext db = new FlightContext())
            {
                flights = db.Flights.ToList();
                FlightsBox.ItemsSource = flights;
            }
        }
    }
}
