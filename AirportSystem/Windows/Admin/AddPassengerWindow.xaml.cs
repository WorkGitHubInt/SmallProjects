using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AirportSystem.Models;

namespace AirportSystem
{
    public partial class AddPassengerWindow : Window
    {
        public AddPassengerWindow()
        {
            InitializeComponent();
        }

        private List<Flight> flights;
        private List<FlightOnDate> flightOnDates;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (FlightContext db = new FlightContext())
            {
                flights = db.Flights.ToList();
                FlightsBox.ItemsSource = flights;
            }
        }

        private void LoadDates(int i)
        {
            using (FlightContext db = new FlightContext())
            {
                var flight = flights[i];
                flightOnDates = db.FlightOnDates.Where(f => f.Flight.FlightNum == flight.FlightNum).ToList();
                DateBox.ItemsSource = flightOnDates;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FlightContext db = new FlightContext())
                {
                    bool success = int.TryParse(PassportNumText.Text, out int passport);
                    if (success && passport < 10000)
                    {
                        success = DateTime.TryParse(DateText.Text, out DateTime date);
                        if (success && date > new DateTime(1930, 1, 1) && date < DateTime.Now)
                        {
                            var chkPassenger = db.Passengers.FirstOrDefault(p => p.PassportNum == passport);
                            if (chkPassenger == null)
                            {
                                var flight = flightOnDates[DateBox.SelectedIndex];
                                var passenger = new Passenger
                                {
                                    PassportNum = passport,
                                    FirstName = NameText.Text.Split(' ')[1],
                                    LastName = NameText.Text.Split(' ')[0],
                                    MidName = NameText.Text.Split(' ')[2],
                                    BirthDate = date,
                                    FlightOnDateId = flight.Id,
                                    FlightOnDate = flight,
                                    Seat = SeatText.Text
                                };
                                db.Passengers.Add(passenger);
                                var flight2 = db.FlightOnDates.Find(flight.Id);
                                flight2.FreeSeats = flight2.Seats - (db.Passengers.Where(p => p.FlightOnDate.Id == flight2.Id).ToList().Count + 1);
                                db.Entry(flight2).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Close();
                            } else
                            {
                                MessageBox.Show("Пассажир с таким номером пасспорта уже есть!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверно указана дата рождения! Дата должна указываться в формате:\"дд.мм.гггг\" и быть в диапазоне от 01.01.1930 до текущего момента!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверно указан номер пасспорта! Номер паспорта должен быть целым числом от 1 до 9999", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                MessageBox.Show("Неверно указано ФИО! ФИО должны быть указаны через пробел!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Произошла неизвестная ошибка! {exp.Message}", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FlightsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FlightsBox.SelectedIndex > -1)
            {
                LoadDates(FlightsBox.SelectedIndex);
            }
        }
    }
}
