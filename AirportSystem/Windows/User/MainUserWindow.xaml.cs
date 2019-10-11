using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AirportSystem.Models;

namespace AirportSystem
{
    public partial class MainUserWindow : Window
    {
        public MainUserWindow()
        {
            InitializeComponent();
        }

        private ObservableCollection<FlightOnDate> flights;
        private ObservableCollection<DateTime> DatesList = new ObservableCollection<DateTime>();
        private ObservableCollection<string> flightNums = new ObservableCollection<string>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (FlightContext db = new FlightContext())
            {
                var image = new Image();
                flights = db.FlightOnDates.Include("Flight").ToList().ToObservableCollection();
                foreach (FlightOnDate flight in flights)
                {
                    DatesList.Add(flight.Date);
                }
                Dates.DataContext = DatesList;
                var flights1 = db.Flights.ToList().ToObservableCollection();
                foreach (var f in flights1)
                {
                    flightNums.Add(f.FlightNum);
                }
                Flights.DataContext = flightNums;
                Table.DataContext = flights;
            }
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (Table.SelectedIndex > -1 && Table.SelectedIndex < flights.Count)
            {
                using (FlightContext db = new FlightContext())
                {
                    if (flights[Table.SelectedIndex].FreeSeats > 0)
                    {
                        BookWindow bookWindow = new BookWindow(flights[Table.SelectedIndex]);
                        bookWindow.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("На выбранный рейс нет мест!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите рейс!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Table.SelectedIndex > -1 && Table.SelectedIndex < flights.Count)
            {
                InfoPanel.DataContext = flights[Table.SelectedIndex];
            }
        }

        private void Dates_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Dates.SelectedIndex > -1)
            {
                using (FlightContext db = new FlightContext())
                {
                    string num = "";
                    DateTime date = DatesList[Dates.SelectedIndex];
                    if (Flights.SelectedIndex > -1)
                    {
                        num = flightNums[Flights.SelectedIndex];
                    }
                    if (num != "")
                    {
                        flights = db.FlightOnDates
                            .Include("Flight")
                            .Where(f => f.Date == date && f.Flight.FlightNum == num)
                            .ToList()
                            .ToObservableCollection();
                    }
                    else
                    {
                        flights = db.FlightOnDates
                            .Include("Flight")
                            .Where(f => f.Date == date)
                            .ToList()
                            .ToObservableCollection();
                    }
                    Table.DataContext = flights;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (FlightContext db = new FlightContext())
            {
                Flights.SelectedIndex = -1;
                Dates.SelectedIndex = -1;
                flights = db.FlightOnDates.Include("Flight").ToList().ToObservableCollection();
                Table.DataContext = flights;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            Hide();
            loginWindow.Show();
            Close();
        }

        private void Flights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Flights.SelectedIndex > -1)
            {
                using (FlightContext db = new FlightContext())
                {
                    string num = flightNums[Flights.SelectedIndex];
                    flights = db.FlightOnDates
                            .Include("Flight")
                            .Where(f => f.Flight.FlightNum == num)
                            .ToList()
                            .ToObservableCollection();
                    DatesList.Clear();
                    foreach (FlightOnDate flight in flights)
                    {
                        DatesList.Add(flight.Date);
                    }
                    Dates.DataContext = DatesList;
                    Table.DataContext = flights;
                }
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var window = new PassportWindow();
            window.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var window = new PassportWindow()
            {
                IsDelete = true
            };
            window.ShowDialog();
        }
    }
}
