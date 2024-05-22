using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RestaurantAbsolute
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        DispatcherTimer sessionTimeClient = new DispatcherTimer();
        int SeansSeconds = 30;
        public ClientWindow()
        {
            InitializeComponent();
            sessionTimeClient.Interval = TimeSpan.FromSeconds(1);
            sessionTimeClient.Tick += Session_Tick;
            sessionTimeClient.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Session_Tick(object sender, EventArgs e)
        {
            SeansSeconds--;
            int hours = SeansSeconds / 60 / 60;
            int minutes = SeansSeconds / 60 % 60;
            int seconds = SeansSeconds % 60;

            sessionBlock.Text = $"{hours:00}:{minutes:00}:{seconds:00}";

            if (SeansSeconds <= 0)
            {
                sessionTimeClient.Stop();
                MessageBox.Show("Время сеанса истекло!");
                Close();
            }
        }
    }
}
