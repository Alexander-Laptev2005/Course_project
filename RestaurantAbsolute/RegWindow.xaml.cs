using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace RestaurantAbsolute
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public RegWindow()
        {
            InitializeComponent();
        }

        private void Image_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            HidePassword();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            Entities.UserData user = new Entities.UserData();
            if (!check())
            {
                return;
            }
            user.Namee = NameTB.Text;
            user.Surname = SurNameTB.Text;
            user.Loginn = RegUserTB.Text;
            user.Passwordd = PasswordBox.Password;
            user.Email = EmailTB.Text;
            App.db.UserData.Add(user);
            MessageBox.Show("Успешная регистрация! Вы можете авторизироваться!");
            App.db.SaveChanges();
            MainWindow mn = new MainWindow();
            mn.Show();
            this.Close();
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
            txtVisibleRepeatePasswordbox.Visibility = Visibility.Visible;
            RepeatePasswordBox.Visibility = Visibility.Hidden;
            txtVisibleRepeatePasswordbox.Text = RepeatePasswordBox.Password;
        }
        void HidePassword()
        {
            txtVisiblePasswordbox.Visibility = Visibility.Hidden;
            PasswordBox.Visibility = Visibility.Visible;
            txtVisibleRepeatePasswordbox.Visibility = Visibility.Hidden;
            RepeatePasswordBox.Visibility = Visibility.Visible;
            PasswordBox.Focus();
        }

        private bool check()
        {
            if (!CheckEmpty())
            {
                return false;
            }
            if (!CheckPass())
            {
                return false;
            }
            if (!CheckRepUser())
            {
                return false;
            }
            if (!CheckRepPass())
            {
                return false;
            }
            if (!CheckEmail())
            {
                return false;
            }
            return true;
        }

        bool CheckEmpty()
        {
            if (NameTB.Text == "" | SurNameTB.Text == "" | PasswordBox.Password == "" | RegUserTB.Text == ""
                | RepeatePasswordBox.Password == "" | EmailTB.Text == "")
            {
                MessageBox.Show("Введите все данные для дальнейшей регистрации!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

       

        // Метод проверки правильности введенного пользователем пароля
        bool CheckPass()
        {
            if (PasswordBox.Password.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать от 8 символов!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (PasswordBox.Password.Length > 16)
            {
                MessageBox.Show("Пароль не должен превышать 16 символов!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Regex MaskPassNumber = new Regex(@"(\w*\d+\w*)");
            MatchCollection passN = MaskPassNumber.Matches(PasswordBox.Password);
            if (passN.Count <= 0)
            {
                MessageBox.Show("Пароль должен содержать хотя бы 1 цифру", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Regex MaskPassUpLet = new Regex(@"(\w*[A-Z]+\w*)");
            MatchCollection passL = MaskPassUpLet.Matches(PasswordBox.Password);
            if (passL.Count <= 0)
            {
                MessageBox.Show("Пароль должен содержать хотя бы 1 заглавную букву", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        bool CheckRepUser()
        {
            if (App.db.UserData.Any(s => s.Loginn == RegUserTB.Text))
            {
                MessageBox.Show("Пользователь с таким логином уже есть!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        bool CheckEmail()
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);
            MatchCollection email = regex.Matches(EmailTB.Text);
            if(email.Count <= 0)
            {
                MessageBox.Show("Указан неправильный Email!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
            }
            return true;
        }


        bool CheckRepPass()
        {
            if (PasswordBox.Password != RepeatePasswordBox.Password)
            {
                MessageBox.Show("Подтверждение пароля не совпадает с паролем!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
