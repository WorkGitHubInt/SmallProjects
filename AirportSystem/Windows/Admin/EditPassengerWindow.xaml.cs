using AirportSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AirportSystem
{
    public partial class EditPassengerWindow : Window
    {
        public EditPassengerWindow()
        {
            InitializeComponent();
        }

        private List<Flight> flights;
        private List<FlightOnDate> flightOnDates;
        public Passenger Passenger { get; set; }

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
                            var chkPassenger = db.Passengers.FirstOrDefault(p => p.PassportNum == passport && p.PassportNum != Passenger.PassportNum);
                            if (chkPassenger == null)
                            {
                                var flight = flightOnDates[DateBox.SelectedIndex];
                                var passenger = db.Passengers.Find(Passenger.Id);
                                passenger.PassportNum = passport;
                                passenger.FirstName = NameText.Text.Split(' ')[1];
                                passenger.LastName = NameText.Text.Split(' ')[0];
                                passenger.MidName = NameText.Text.Split(' ')[2];
                                passenger.BirthDate = date;
                                passenger.FlightOnDateId = flight.Id;
                                passenger.Seat = SeatText.Text;
                                db.Entry(passenger).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Close();
                            } else
                            {
                                MessageBox.Show("Пассажир с таким номером пасспорта уже есть!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        } else
                        {
                            MessageBox.Show("Неверно указана дата рождения! Дата должна указываться в формате:\"дд.мм.гггг\" и быть в диапазоне от 01.01.1930 до текущего момента!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    } else
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FlightContext db = new FlightContext())
                {
                    flights = db.Flights.ToList();
                    FlightsBox.ItemsSource = flights;
                    Table.DataContext = Passenger;
                    for (int i = 0; i < flights.Count; i++)
                    {
                        if (flights[i].FlightNum == Passenger.FlightOnDate.Flight.FlightNum)
                        {
                            FlightsBox.SelectedIndex = i;
                        }
                    }
                    for (int i = 0; i < flightOnDates.Count; i++)
                    {
                        if (flightOnDates[i].Date == Passenger.FlightOnDate.Date)
                        {
                            DateBox.SelectedIndex = i;
                        }
                    }
                }
            } catch (Exception exp)
            {
                MessageBox.Show($"Произошла неизвестная ошибка! {exp.Message}", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadDates(int i)
        {
            try
            {
                using (FlightContext db = new FlightContext())
                {
                    var flight = flights[i];
                    flightOnDates = db.FlightOnDates.Where(f => f.Flight.FlightNum == flight.FlightNum).ToList();
                    DateBox.ItemsSource = flightOnDates;
                }
            } catch (Exception exp)
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
