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
    /// Логика взаимодействия для Vizitka.xaml
    /// </summary>
    public partial class Vizitka : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        public Vizitka()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            timer.Stop();
            this.Close();
        }
    }
}
