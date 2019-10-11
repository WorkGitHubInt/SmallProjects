using System;
using System.Linq;
using System.Windows;
using AirportSystem.Models;
using System.Diagnostics;
using System.Collections.Generic;

namespace AirportSystem
{
    public partial class MainAdminWindow : Window
    {
        public MainAdminWindow()
        {
            InitializeComponent();
        }

        private List<Flight> FlightsList;
        private List<FlightOnDate> FlightsOnDateList;
        private List<Passenger> PassengersList;

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var window = new LoginWindow();
            Hide();
            window.Show();
            Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var authorWindow = new AuthorWindow();
            authorWindow.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDb();
        }

        private void TestInfo()
        {
            using (FlightContext db = new FlightContext())
            {
                var flight = new Flight
                {
                    FlightNum = "SU1001",
                    Destination = "Москва",
                    DepartureTime = new TimeSpan(10, 30, 0),
                    ArrivalTime = new TimeSpan(12, 30, 0),
                };
                var flight2 = new Flight
                {
                    FlightNum = "SU1002",
                    Destination = "Минск",
                    DepartureTime = new TimeSpan(8, 00, 0),
                    ArrivalTime = new TimeSpan(10, 00, 0),
                };
                var flight3 = new Flight
                {
                    FlightNum = "SU6324",
                    Destination = "Санкт-петербург",
                    DepartureTime = new TimeSpan(15, 40, 0),
                    ArrivalTime = new TimeSpan(17, 10, 0),
                };
                var flight4 = new Flight
                {
                    FlightNum = "UT390",
                    Destination = "Москва",
                    DepartureTime = new TimeSpan(20, 10, 0),
                    ArrivalTime = new TimeSpan(22, 10, 0),
                };
                var flight5 = new Flight
                {
                    FlightNum = "UT394",
                    Destination = "Омск",
                    DepartureTime = new TimeSpan(6, 25, 0),
                    ArrivalTime = new TimeSpan(9, 15, 0),
                };
                var flight6 = new Flight
                {
                    FlightNum = "U6360",
                    Destination = "Гродно",
                    DepartureTime = new TimeSpan(16, 45, 0),
                    ArrivalTime = new TimeSpan(18, 50, 0),
                };
                db.Flights.Add(flight);
                db.Flights.Add(flight2);
                db.Flights.Add(flight3);
                db.Flights.Add(flight4);
                db.Flights.Add(flight5);
                db.Flights.Add(flight6);

                var flightOnDate = new FlightOnDate
                {
                    Flight = flight,
                    Date = new DateTime(2019, 4, 1),
                    Seats = 150,
                    FreeSeats = 150
                };
                var flightOnDate1 = new FlightOnDate
                {
                    Flight = flight,
                    Date = new DateTime(2019, 4, 3),
                    Seats = 150,
                    FreeSeats = 149
                };
                var flightOnDate2 = new FlightOnDate
                {
                    Flight = flight,
                    Date = new DateTime(2019, 4, 5),
                    Seats = 150,
                    FreeSeats = 149
                };

                var flightOnDate3 = new FlightOnDate
                {
                    Flight = flight2,
                    Date = new DateTime(2019, 4, 3),
                    Seats = 200,
                    FreeSeats = 200
                };
                var flightOnDate4 = new FlightOnDate
                {
                    Flight = flight2,
                    Date = new DateTime(2019, 4, 6),
                    Seats = 50,
                    FreeSeats = 50
                };

                var flightOnDate5 = new FlightOnDate
                {
                    Flight = flight3,
                    Date = new DateTime(2019, 4, 1),
                    Seats = 100,
                    FreeSeats = 100
                };
                var flightOnDate6 = new FlightOnDate
                {
                    Flight = flight3,
                    Date = new DateTime(2019, 4, 3),
                    Seats = 100,
                    FreeSeats = 97
                };
                var flightOnDate7 = new FlightOnDate
                {
                    Flight = flight3,
                    Date = new DateTime(2019, 4, 5),
                    Seats = 100,
                    FreeSeats = 100
                };

                var flightOnDate8 = new FlightOnDate
                {
                    Flight = flight4,
                    Date = new DateTime(2019, 4, 2),
                    Seats = 90,
                    FreeSeats = 90
                };
                var flightOnDate9 = new FlightOnDate
                {
                    Flight = flight4,
                    Date = new DateTime(2019, 4, 4),
                    Seats = 90,
                    FreeSeats = 90
                };
                var flightOnDate10 = new FlightOnDate
                {
                    Flight = flight4,
                    Date = new DateTime(2019, 4, 6),
                    Seats = 90,
                    FreeSeats = 90
                };

                var flightOnDate11 = new FlightOnDate
                {
                    Flight = flight5,
                    Date = new DateTime(2019, 4, 10),
                    Seats = 120,
                    FreeSeats = 120
                };

                var flightOnDate12 = new FlightOnDate
                {
                    Flight = flight5,
                    Date = new DateTime(2019, 4, 7),
                    Seats = 60,
                    FreeSeats = 59
                };

                db.FlightOnDates.Add(flightOnDate);
                db.FlightOnDates.Add(flightOnDate1);
                db.FlightOnDates.Add(flightOnDate2);
                db.FlightOnDates.Add(flightOnDate3);
                db.FlightOnDates.Add(flightOnDate4);
                db.FlightOnDates.Add(flightOnDate5);
                db.FlightOnDates.Add(flightOnDate6);
                db.FlightOnDates.Add(flightOnDate7);
                db.FlightOnDates.Add(flightOnDate8);
                db.FlightOnDates.Add(flightOnDate9);
                db.FlightOnDates.Add(flightOnDate10);
                db.FlightOnDates.Add(flightOnDate11);
                db.FlightOnDates.Add(flightOnDate12);

                var passenger = new Passenger
                {
                    PassportNum = 1523,
                    FirstName = "Владимир",
                    LastName = "Кремлев",
                    MidName = "Алексеевич",
                    BirthDate = new DateTime(1998, 5, 21),
                    Seat = "11A",
                    FlightOnDate = flightOnDate6
                };
                var passenger1 = new Passenger
                {
                    PassportNum = 5612,
                    FirstName = "Анжелика",
                    LastName = "Чалова",
                    MidName = "Сергеевна",
                    BirthDate = new DateTime(1998, 8, 26),
                    Seat = "5E",
                    FlightOnDate = flightOnDate2
                };
                var passenger2 = new Passenger
                {
                    PassportNum = 7523,
                    FirstName = "Владимир",
                    LastName = "Морозов",
                    MidName = "Андреевич",
                    BirthDate = new DateTime(1998, 5, 28),
                    Seat = "13B",
                    FlightOnDate = flightOnDate12
                };
                var passenger3 = new Passenger
                {
                    PassportNum = 1324,
                    FirstName = "Андрей",
                    LastName = "Переверза",
                    MidName = "Сергеевич",
                    BirthDate = new DateTime(1968, 11, 1),
                    Seat = "6D",
                    FlightOnDate = flightOnDate6
                };
                var passenger4 = new Passenger
                {
                    PassportNum = 3211,
                    FirstName = "Дмитрий",
                    LastName = "Иванов",
                    MidName = "Владимирович",
                    BirthDate = new DateTime(1985, 6, 13),
                    Seat = "5F",
                    FlightOnDate = flightOnDate6
                };
                var passenger5 = new Passenger
                {
                    PassportNum = 4564,
                    FirstName = "Иван",
                    LastName = "Иванов",
                    MidName = "Иванович",
                    BirthDate = new DateTime(1946, 1, 1),
                    Seat = "17A",
                    FlightOnDate = flightOnDate1
                };

                db.Passengers.Add(passenger);
                db.Passengers.Add(passenger1);
                db.Passengers.Add(passenger2);
                db.Passengers.Add(passenger3);
                db.Passengers.Add(passenger4);
                db.Passengers.Add(passenger5);

                var flight7 = new Flight
                {
                    FlightNum = "SU1013",
                    Destination = "Калининград",
                    DepartureTime = new TimeSpan(16, 45, 0),
                    ArrivalTime = new TimeSpan(18, 50, 0),
                };
                var flightOnDate13 = new FlightOnDate
                {
                    Flight = flight7,
                    Date = new DateTime(2019, 4, 8),
                    Seats = 1,
                    FreeSeats = 0
                };
                var passenger6 = new Passenger
                {
                    PassportNum = 1111,
                    FirstName = "Иван",
                    LastName = "Колесников",
                    MidName = "Иванович",
                    BirthDate = new DateTime(1946, 1, 1),
                    Seat = "1A",
                    FlightOnDate = flightOnDate13
                };
                db.Flights.Add(flight7);
                db.FlightOnDates.Add(flightOnDate13);
                db.Passengers.Add(passenger6);

                db.SaveChanges();
            }
        }

        private void LoadDb()
        {
            try
            {
                using (FlightContext db = new FlightContext())
                {
                    FlightsList?.Clear();
                    FlightsList = db.Flights.ToList();
                    FlightsOnDateList = null;
                    FlightsOnDateList = db.FlightOnDates.Include("Flight").ToList();
                    PassengersList = null;
                    PassengersList = db.Passengers.Include("FlightOnDate").ToList();
                    Flights.ItemsSource = null;
                    Flights.ItemsSource = FlightsList;
                    FlightOnDate.ItemsSource = null;
                    FlightOnDate.ItemsSource = FlightsOnDateList;
                    Passengers.ItemsSource = null;
                    Passengers.ItemsSource = PassengersList;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Произошла неизвестная ошибка! {e.Message}", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddFlightClick(object sender, RoutedEventArgs e)
        {
            var window = new AddFlightWindow();
            window.ShowDialog();
            LoadDb();
        }

        private void AddFlightOnDateClick(object sender, RoutedEventArgs e)
        {
            var window = new AddFlightOnDateWindow();
            window.ShowDialog();
            LoadDb();
        }

        private void AddPassengerClick(object sender, RoutedEventArgs e)
        {
            var window = new AddPassengerWindow();
            window.ShowDialog();
            LoadDb();
        }

        private void EditFlight(object sender, RoutedEventArgs e)
        {
            if (Flights.SelectedIndex > -1 && Flights.SelectedIndex < FlightsList.Count)
            {
                var window = new EditFlightWindow()
                {
                    Flight = FlightsList[Flights.SelectedIndex]
                };
                window.ShowDialog();
                LoadDb();
            }
            else
            {
                MessageBox.Show("Выберите рейс из списка!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditFlightOnDate(object sender, RoutedEventArgs e)
        {
            if (FlightOnDate.SelectedIndex > -1 && FlightOnDate.SelectedIndex < FlightsOnDateList.Count)
            {
                var window = new EditFlightOnDateWindow()
                {
                    FlightOnDate = FlightsOnDateList[FlightOnDate.SelectedIndex]
                };
                window.ShowDialog();
                LoadDb();
            }
            else
            {
                MessageBox.Show("Выберите рейс из списка!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditPassenger(object sender, RoutedEventArgs e)
        {
            if (Passengers.SelectedIndex > -1 && Passengers.SelectedIndex < PassengersList.Count)
            {
                var window = new EditPassengerWindow()
                {
                    Passenger = PassengersList[Passengers.SelectedIndex]
                };
                window.ShowDialog();
                LoadDb();
            }
            else
            {
                MessageBox.Show("Выберите пассажира из списка!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteFlight(object sender, RoutedEventArgs e)
        {
            if (Flights.SelectedIndex > -1 && Flights.SelectedIndex < FlightsList.Count)
            {
                try
                {
                    var result = MessageBox.Show("Вы действительно хотите удалить рейс?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (FlightContext db = new FlightContext())
                        {
                            var flight = db.Flights.Find(FlightsList[Flights.SelectedIndex].Id);
                            db.Flights.Remove(flight);
                            db.SaveChanges();
                        }
                    }
                    LoadDb();
                }
                catch (Exception)
                {
                    MessageBox.Show("Сначала удалите все связанные данные!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите рейс из списка!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteFlightOnDate(object sender, RoutedEventArgs e)
        {
            if (FlightOnDate.SelectedIndex > -1 && FlightOnDate.SelectedIndex < FlightsOnDateList.Count)
            {
                try
                {
                    var result = MessageBox.Show("Вы действительно хотите удалить рейс по дате?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (FlightContext db = new FlightContext())
                        {
                            var flight = db.FlightOnDates.Find(FlightsOnDateList[FlightOnDate.SelectedIndex].Id);
                            db.FlightOnDates.Remove(flight);
                            db.SaveChanges();
                        }
                    }
                    LoadDb();
                }
                catch
                {
                    MessageBox.Show("Сначала удалите все связанные данные!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите рейс из списка!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeletePassenger(object sender, RoutedEventArgs e)
        {
            if (Passengers.SelectedIndex > -1 && Passengers.SelectedIndex < PassengersList.Count)
            {
                try
                {
                    var result = MessageBox.Show("Вы действительно хотите удалить пассажира?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        using (FlightContext db = new FlightContext())
                        {
                            var passenger = db.Passengers.Find(PassengersList[Passengers.SelectedIndex].Id);
                            db.Passengers.Remove(passenger);
                            db.SaveChanges();
                        }
                    }
                    LoadDb();
                }
                catch
                {
                    MessageBox.Show("Сначала удалите все связанные данные!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите пассажира из списка!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearFlights(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FlightContext db = new FlightContext())
                {
                    var result = MessageBox.Show("Вы действительно хотите очистить таблицу рейсов?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Flights]");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Сначала удалите все связанные данные!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearFlightsOnDate(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FlightContext db = new FlightContext())
                {
                    var result = MessageBox.Show("Вы действительно хотите очистить таблицу рейсов по дате?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.Database.ExecuteSqlCommand("TRUNCATE TABLE [FlightOnDates]");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Сначала удалите все связанные данные!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearPassengers(object sender, RoutedEventArgs e)
        {
            try
            {
                using (FlightContext db = new FlightContext())
                {
                    var result = MessageBox.Show("Вы действительно хотите очистить таблицу пассажиров?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Passengers]");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Сначала удалите все связанные данные!", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            TestInfo();
            LoadDb();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            var window = new PasswordWindow();
            window.ShowDialog();
        }
    }
}
