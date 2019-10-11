using AirportSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace AirportSystem
{
    public partial class EditFlightOnDateWindow : Window
    {
        public EditFlightOnDateWindow()
        {
            InitializeComponent();
        }

        private List<Flight> flights;
        public FlightOnDate FlightOnDate { get; set; }

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
                            var flightOnDate = db.FlightOnDates.Find(FlightOnDate.Id);
                            var flightChk = db.FlightOnDates.FirstOrDefault(f => f.Date == date && f.Date != FlightOnDate.Date);
                            if (flightChk == null)
                            {
                                flightOnDate.FlightId = flight.Id;
                                flightOnDate.Flight = flight;
                                flightOnDate.Date = date;
                                flightOnDate.Seats = seats;
                                db.Entry(flightOnDate).State = System.Data.Entity.EntityState.Modified;
                                db.SaveChanges();
                                Close();
                            }
                            else
                            {
                                MessageBox.Show("На указанную дату уже есть рейс!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
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
                try
                {
                    flights = db.Flights.ToList();
                    FlightsBox.ItemsSource = flights;
                    for (int i = 0; i < flights.Count; i++)
                    {
                        if (flights[i].FlightNum == FlightOnDate.Flight.FlightNum)
                        {
                            FlightsBox.SelectedIndex = i;
                        }
                    }
                    Table.DataContext = FlightOnDate;
                }
                catch
                {
                    MessageBox.Show("Произошла неизвестная ошибка!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
