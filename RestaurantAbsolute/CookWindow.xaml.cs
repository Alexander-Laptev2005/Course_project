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
    /// Логика взаимодействия для CookWindow.xaml
    /// </summary>
    public partial class CookWindow : Window
    {
        DispatcherTimer sessionTimeСook = new DispatcherTimer();
        int SeansSeconds = 30;
        public CookWindow()
        {
            InitializeComponent();
            sessionTimeСook.Interval = TimeSpan.FromSeconds(1);
            sessionTimeСook.Tick += Session_Tick;
            sessionTimeСook.Start();

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
                sessionTimeСook.Stop();
                MessageBox.Show("Время сеанса истекло!");
                Close();
            }
        }
    }
}
