using System.Windows;

namespace AirportSystem
{
    public partial class IATAWindow : Window
    {
        public IATAWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
