using System.Linq;
using System.Windows;
using System;
using System.Collections.Generic;
using AirportSystem.Models;

namespace AirportSystem
{
    public partial class PassportWindow : Window
    {
        public PassportWindow()
        {
            InitializeComponent();
        }

        private List<Passenger> Passengers;
        public bool IsDelete = false;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {   
                if (IsDelete)
                {
                    if (MessageBox.Show("Вы действительно хотите отменить бронирование?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        using(var db = new FlightContext())
                        {
                            Passenger pass = Passengers[FlightsGrid.SelectedIndex];
                            var passenger = db.Passengers.FirstOrDefault(p => p.PassportNum == pass.PassportNum && p.FlightOnDate.Flight.FlightNum == pass.FlightOnDate.Flight.FlightNum && p.FlightOnDate.Date == pass.FlightOnDate.Date);
                            db.Passengers.Remove(passenger);
                            db.SaveChanges();
                            Close();
                        }
                    }
                }
                else
                {
                    var window = new BookInfoWindow
                    {
                        Passenger = Passengers[FlightsGrid.SelectedIndex]
                    };
                    Hide();
                    window.Show();
                    Close();
                }
            }
            catch
            {
                MessageBox.Show("Неверно указан номер паспорта", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PassportText_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            using (var db = new FlightContext())
            {
                if (int.TryParse(PassportText.Text, out int num))
                {
                    Passengers = db.Passengers.Include("FlightOnDate").Include("FlightOnDate.Flight").Where(p => p.PassportNum == num).ToList();
                    FlightsGrid.DataContext = Passengers;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsDelete)
            {
                ConfirmBtn.Content = "Отменить";
            }
        }
    }
}
