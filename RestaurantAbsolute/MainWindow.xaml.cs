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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RestaurantAbsolute
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int failCaptcha = 0;
        DispatcherTimer time = new DispatcherTimer();
        DispatcherTimer sessionTime = new DispatcherTimer();
        int SeansSeconds = 30;
        public MainWindow()
        {
            InitializeComponent();
            sessionTime.Interval = TimeSpan.FromSeconds(1);
            sessionTime.Tick += Session_Tick;
            sessionTime.Start();

            time.Interval = TimeSpan.FromSeconds(1);
            time.Tick += time_tick;
        }

        //string loginClient = "user123";
        //string passClient = "abc123";
        //string loginCook = "cook123";
        //string passCook = "abc123";
        //string loginAdmin = "admin123";
        //string passAdmin = "abc123";

        private void Session_Tick(object sender, EventArgs e)
        {
            SeansSeconds--;
            int hours = SeansSeconds / 60 / 60;
            int minutes = SeansSeconds / 60 % 60;
            int seconds = SeansSeconds % 60;

            sessionBlock.Text = $"{hours:00}:{minutes:00}:{seconds:00}";

            if (SeansSeconds <= 0)
            {
                sessionTime.Stop();
                MessageBox.Show("Время сеанса истекло!");
                Close();
            }
        }

        private void Image_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            HidePassword();
        }

        private void Image_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowPassword();
        }

        void ShowPassword()
        {
            txtVisiblePasswordbox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Hidden;
            txtVisiblePasswordbox.Text = PasswordBox.Password;
        }
        void HidePassword()
        {
            txtVisiblePasswordbox.Visibility = Visibility.Hidden;
            PasswordBox.Visibility = Visibility.Visible;
            PasswordBox.Focus();
        }

        int FailAutorization;
        private void LogButton_Click(object sender, RoutedEventArgs e)
        {
            var login = LogUserTB.Text;
            var password = PasswordBox.Password;
            if (PasswordBox.Password == "" | LogUserTB.Text == "")
            {
                MessageBox.Show("Введите логин и пароль для входа!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            foreach (var user in App.db.UserData)
            {
                foreach (var userr in App.db.TypeUser)
                {
                    if (userr.id_post == 1 & user.id_User == 1)
                    {
                        if (user.Loginn == login)
                        {
                            if (user.Passwordd == password)
                            {
                                MessageBox.Show("Успешная авторизация!");
                                AdminWindow admin = new AdminWindow();
                                admin.Show();
                                admin.LoginShow.Content = user.Namee + " " + user.Surname;
                                sessionTime.Stop();
                                this.Close();
                                return;
                            }
                            else
                            {
                                FailAutorization++;
                                if (FailAutorization == 3)
                                {
                                    sessionTime.Stop();
                                    LogButton.Visibility = Visibility.Hidden;
                                    Reg.Visibility = Visibility.Hidden;
                                    NameLabel.Visibility = Visibility.Hidden;
                                    captcha.Visibility = Visibility.Visible;
                                    FailAutorization = 0;
                                    generatecaptcha();
                                }
                                else MessageBox.Show("Неверный пароль или логин!", "Внимание",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        //else
                        //{
                        //    FailAutorization++;
                        //    if (FailAutorization == 3)
                        //    {
                        //        LogButton.Visibility = Visibility.Hidden;
                        //        Reg.Visibility = Visibility.Hidden;
                        //        NameLabel.Visibility = Visibility.Hidden;
                        //        captcha.Visibility = Visibility.Visible;
                        //        FailAutorization = 0;
                        //        generatecaptcha();
                        //    }
                        //    else MessageBox.Show("Неверный пароль или логин!", "Внимание",
                        //        MessageBoxButton.OK, MessageBoxImage.Error);
                        //    return;
                        //}
                    }
                    else if (userr.id_post == 2 & user.id_User == 2)
                    {
                        if (user.Loginn == login)
                        {
                            if (user.Passwordd == password)
                            {
                                MessageBox.Show("Успешная авторизация!");
                                CookWindow cook = new CookWindow();
                                cook.Show();
                                cook.LoginShow.Content = user.Namee + " " + user.Surname;
                                sessionTime.Stop();
                                this.Close();
                                return;
                            }
                            else
                            {
                                FailAutorization++;
                                if (FailAutorization == 3)
                                {
                                    LogButton.Visibility = Visibility.Hidden;
                                    Reg.Visibility = Visibility.Hidden;
                                    NameLabel.Visibility = Visibility.Hidden;
                                    captcha.Visibility = Visibility.Visible;
                                    FailAutorization = 0;
                                    generatecaptcha();
                                    sessionTime.Stop();
                                }
                                else MessageBox.Show("Неверный пароль или логин!", "Внимание",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        //else
                        //{
                        //    FailAutorization++;
                        //    if (FailAutorization == 3)
                        //    {
                        //        LogButton.Visibility = Visibility.Hidden;
                        //        Reg.Visibility = Visibility.Hidden;
                        //        NameLabel.Visibility = Visibility.Hidden;
                        //        captcha.Visibility = Visibility.Visible;
                        //        FailAutorization = 0;
                        //    }
                        //    else MessageBox.Show("Неверный пароль или логин!", "Внимание",
                        //        MessageBoxButton.OK, MessageBoxImage.Error);
                        //    return;
                        //}
                    }
                    else if (userr.id_post == 3 & user.id_User == 3)
                    {
                        if (user.Loginn == login)
                        {
                            if (user.Passwordd == password)
                            {
                                MessageBox.Show("Успешная авторизация!");
                                ClientWindow client = new ClientWindow();
                                client.Show();
                                client.LoginShow.Content = user.Namee + " " + user.Surname;
                                sessionTime.Stop();
                                this.Close();
                                return;
                            }
                            else
                            {
                                FailAutorization++;
                                if (FailAutorization == 3)
                                {
                                    LogButton.Visibility = Visibility.Hidden;
                                    Reg.Visibility = Visibility.Hidden;
                                    NameLabel.Visibility = Visibility.Hidden;
                                    captcha.Visibility = Visibility.Visible;
                                    FailAutorization = 0;
                                    generatecaptcha();
                                    sessionTime.Stop();
                                }
                                else MessageBox.Show("Неверный пароль или логин!", "Внимание",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        //else
                        //{
                        //    FailAutorization++;
                        //    if (FailAutorization == 3)
                        //    {
                        //        LogButton.Visibility = Visibility.Hidden;
                        //        Reg.Visibility = Visibility.Hidden;
                        //        NameLabel.Visibility = Visibility.Hidden;
                        //        captcha.Visibility = Visibility.Visible;
                        //    }
                        //    else MessageBox.Show("Неверный пароль или логин!", "Внимание",
                        //        MessageBoxButton.OK, MessageBoxImage.Error);
                        //    return;
                        //}

                    }
                    else if (userr.id_post == 3 & user.id_User >= 4)
                    {
                        if (user.Loginn == login)
                        {
                            if (user.Passwordd == password)
                            {
                                MessageBox.Show("Успешная авторизация!");
                                ClientWindow clientWindow = new ClientWindow();
                                clientWindow.Show();
                                clientWindow.LoginShow.Content = user.Namee + " " + user.Surname;
                                sessionTime.Stop();
                                this.Close();
                                return;
                            }
                            else
                            {
                                FailAutorization++;
                                if (FailAutorization == 3)
                                {
                                    LogButton.Visibility = Visibility.Hidden;
                                    Reg.Visibility = Visibility.Hidden;
                                    NameLabel.Visibility = Visibility.Hidden;
                                    captcha.Visibility = Visibility.Visible;
                                    FailAutorization = 0;
                                    generatecaptcha();
                                    sessionTime.Stop();
                                }
                                else MessageBox.Show("Неверный пароль или логин!", "Внимание",
                                    MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                        }
                        //else
                        //{
                        //    FailAutorization++;
                        //    if (FailAutorization == 3)
                        //    {
                        //        captcha.Visibility = Visibility.Visible;
                        //        LogButton.Visibility = Visibility.Hidden;
                        //        Reg.Visibility = Visibility.Hidden;
                        //        NameLabel.Visibility = Visibility.Hidden;
                        //        FailAutorization = 0;
                        //        generatecaptcha();
                                
                        //    }
                        //    else MessageBox.Show("Неверный пароль или логин!", "Внимание",
                        //        MessageBoxButton.OK, MessageBoxImage.Error);
                        //    return;
                        //}
                    }
                    //else
                    //{
                    //    FailAutorization++;
                    //    if (FailAutorization == 3)
                    //    {
                    //        LogButton.Visibility = Visibility.Hidden;
                    //        Reg.Visibility = Visibility.Hidden;
                    //        NameLabel.Visibility = Visibility.Hidden;
                    //        captcha.Visibility = Visibility.Visible;
                    //    }
                    //    else MessageBox.Show("Неверный пароль или логин!", "Внимание",
                    //        MessageBoxButton.OK, MessageBoxImage.Error);
                    //    return;
                    //}
                }
            }
            MessageBox.Show("Такого пользователя нет!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow();
            regWindow.Show();
            sessionTime.Stop();
            this.Close();
        }

        private void InsertCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (textcaptcha.Text.ToLower() == captchaText)
            {
                textcaptcha.Text = string.Empty;
                LogUserTB.Text = string.Empty;
                txtVisiblePasswordbox.Text = string.Empty;
                PasswordBox.Password = string.Empty;
                Autorization.Visibility = Visibility.Visible;
                captcha.Visibility = Visibility.Hidden;
                LogButton.Visibility = Visibility.Visible;
                Reg.Visibility = Visibility.Visible;
                NameLabel.Visibility = Visibility.Visible;
                sessionTime.Start();
            }
            else
            {
                failCaptcha++;
                generatecaptcha();
                if (failCaptcha > 2)
                {
                    captcha.Visibility = Visibility.Hidden;
                    Autorization.Visibility = Visibility.Visible;
                    LogButton.Visibility = Visibility.Visible;
                    Reg.Visibility = Visibility.Visible;
                    NameLabel.Visibility = Visibility.Visible;
                    LogButton.IsEnabled = false;
                    textcaptcha.Text = string.Empty;
                    LogUserTB.Text = string.Empty;
                    txtVisiblePasswordbox.Text = string.Empty;
                    PasswordBox.Password = string.Empty;
                    time.Start();

                }
            }
        }

        private void ReloadCaptcha_Click(object sender, RoutedEventArgs e)
        {
            generatecaptcha();
        }

        string captchaText = "";
        public void generatecaptcha()
        {
            canvasCaptcha.Children.Clear();
            Random rnd = new Random();
            RotateTransform rotateTransform = new RotateTransform();
            TextBlock textBlock = new TextBlock();
            string captchasim = "qwertyuiopasdfghjklzxcvbnm1234567890";
            captchaText = "";
            rotateTransform.Angle = rnd.Next(-20, 20);
            for (int i = 0; i < 4; i++)
            {
                char generat = captchasim[rnd.Next(captchasim.Length)];
                captchaText += generat;

            }
            rotateTransform.Angle = rnd.Next(-20, 20);
            textBlock.Text = captchaText;
            textBlock.FontSize = 32;
            textBlock.RenderTransform = rotateTransform;
            Canvas.SetLeft(textBlock, rnd.Next(20, 100));
            Canvas.SetTop(textBlock, rnd.Next(20, 30));
            canvasCaptcha.Children.Add(textBlock);
            for (int i = 0; i < 600; i++)
            {

                Ellipse ellipse = new Ellipse();
                int r = rnd.Next(3, 5);
                ellipse.Height = r; ellipse.Width = r;
                Brush brus = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(1, 255), (byte)rnd.Next(1, 255), (byte)rnd.Next(1, 233)));
                ellipse.Fill = brus;
                Canvas.SetLeft(ellipse, rnd.Next(250));
                Canvas.SetTop(ellipse, rnd.Next(100));

                canvasCaptcha.Children.Add(ellipse);
            }
            for (int i = 0; i < 2; i++)
            {
                Line line = new Line();
                line.X1 = rnd.Next(0, 50);
                line.Y1 = rnd.Next(0, 50);
                line.X2 = rnd.Next(150, 250);
                line.Y2 = rnd.Next(51, 100);
                Brush brus = new SolidColorBrush(Color.FromRgb((byte)rnd.Next(1, 255), (byte)rnd.Next(1, 255), (byte)rnd.Next(1, 233)));
                line.Stroke = brus;
                line.StrokeThickness = 3;
                canvasCaptcha.Children.Add(line);

            }
        }

        int sec = 10;
        public void time_tick(object sender, EventArgs e)
        {
            Time.Text = "Система заблокирована осталось " + sec.ToString() + " секунд";
            if (sec == 0)
            {
                Time.Text = string.Empty;
                LogButton.IsEnabled = true;
                time.Stop();
                sessionTime.Start();
                sec = 10;
            }
            sec--;

        }
    }
}
