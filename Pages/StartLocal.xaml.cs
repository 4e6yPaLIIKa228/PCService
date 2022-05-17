using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
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
using PCService.Connect;
using PCService.Pages;


namespace PCService.Pages
{
    /// <summary>
    /// Логика взаимодействия для StartLocal.xaml
    /// </summary>
    public partial class StartLocal : Window
    {
        string IPReg, IPLast;
        int provekraLogin = 0;
        public StartLocal()
        {
            InitializeComponent();
        }

        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnAvtoriz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(SqlDBConnection.connection))
                {
                    if (txtlog.Text != "" && txtlog.Text.Length > 2 && txtpass.Password != "" && txtpass.Password.Length > 2)
                    {

                        MessageBox.Show("1");
                        var Pass = SimpleComand.GetHash(txtpass.Password);
                        connection.Open();
                        string query = $@"SELECT  COUNT(1) FROM Accounts WHERE Login=@Login AND Pass=@Pass";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        string LoginLower = txtlog.Text.ToLower();
                        cmd.Parameters.AddWithValue("@Login", txtlog.Text.ToLower());
                        cmd.Parameters.AddWithValue("@Pass", Pass);
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        connection.Close();
                        if (count == 1)
                        {
                            connection.Open();
                            string smaltxt = txtlog.Text.ToLower();
                            query = $@"SELECT ID FROM Accounts WHERE Login={smaltxt}";
                            // cmd.Parameters.AddWithValue("@Login", txtlog.Text.ToLower());
                            // int countID = Convert.ToInt32(cmd.ExecuteScalar());
                            Saver.Login = txtlog.Text.ToLower();

                            SQLiteDataReader dr = null;
                            SQLiteCommand cmd1 = new SQLiteCommand(query, connection);
                            dr = cmd1.ExecuteReader();
                            while (dr.Read())
                            {

                                Saver.IDAcc = dr["ID"].ToString();
                                //  Saver.IDAcc = countID;
                            }
                            connection.Close();
                            MessageBox.Show("Добро пожаловать! " + $@"{txtlog.Text}");
                            Menu Aftoriz = new Menu();
                            this.Close();
                            Aftoriz.ShowDialog();
                        }
                        else
                        {
                            {
                                MessageBox.Show("Неверный логин или пароль");
                            }
                        }

                    }
                    else
                    {
                        CheckerLog();
                        MessageBox.Show("2");
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
            //MenuBank Aftoriz = new MenuBank();
            //this.Close();
            //Aftoriz.ShowDialog();
        }

        public void CheckerLog() // Для авторизации
        {
            try
            {
                SimpleComand.CheckTextBox(txtlog);
                if (txtlog.Text.Length < 1)
                {
                    // MessageBox.Show("Логин должне быть больше 5 символов!", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtlog.BorderBrush = Brushes.Red;
                }


                if (txtpass.Password.Length < 1)
                {
                    // MessageBox.Show("Пароль должне быть больше 5 символов!", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtpass.BorderBrush = Brushes.Red;
                }
                else
                {
                    txtpass.BorderBrush = new SolidColorBrush(color: (Color)ColorConverter.ConvertFromString("#89000000")); ;
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(Convert.ToString(er));
            }


        }

        public void Checker() //Для регстирации
        {
            try
            {
                SimpleComand.CheckTextBox(txtlogreg);
                //SimpleComand.CheckTextBox(txtpassreg);
                //SimpleComand.CheckPassBox(txtpassreg);

                if (txtlogreg.Text.Length < 1)
                {
                    // MessageBox.Show("Логин должне быть больше 5 символов!", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtlogreg.BorderBrush = Brushes.Red;
                }
                if (txtpassreg.Password.Length < 1)
                {
                    // MessageBox.Show("Пароль должне быть больше 5 символов!", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtpassreg.BorderBrush = Brushes.Red;
                }
                else
                {
                    txtpassreg.BorderBrush = new SolidColorBrush(color: (Color)ColorConverter.ConvertFromString("#89000000")); ;
                }
                if (txtpassreg2.Password.Length < 1)
                {
                    // MessageBox.Show("Пароль должне быть больше 5 символов!", "Ошбика", MessageBoxButton.OK, MessageBoxImage.Error);
                    txtpassreg2.BorderBrush = Brushes.Red;
                }
                else
                {
                    txtpassreg2.BorderBrush = new SolidColorBrush(color: (Color)ColorConverter.ConvertFromString("#89000000")); ;
                }
                if ((txtpassreg2.Password.Length < 1 && txtpassreg.Password.Length < 1) || (txtpassreg2.Password != txtpassreg.Password) )
                {
                    txtpassreg2.BorderBrush = Brushes.Red;
                }
                else
                {
                    txtpassreg2.BorderBrush = new SolidColorBrush(color: (Color)ColorConverter.ConvertFromString("#89000000")); ;
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(Convert.ToString(er));
            }
        }

        public void InfoLogin() //Проверка на повтор логина
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(SqlDBConnection.connection))
                {
                    connection.Open();
                    string query = $@"SELECT  COUNT(1) FROM Accounts WHERE Login=@Login";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@Login", txtlogreg.Text.ToLower());
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count == 1)
                    {
                        MessageBox.Show("Login занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        provekraLogin = 1;
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
        }

        private void btnreg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(SqlDBConnection.connection))
                {
                    Checker();

                    MessageBox.Show("1");
                    if (txtlogreg.Text != "" && txtlogreg.Text.Length > 2 && txtpassreg.Password != "" && txtpassreg.Password.Length > 2)
                    {
                        InfoLogin();
                        if (provekraLogin != 1 && txtpassreg2.Password == txtpassreg.Password)
                        {
                            InfoIP();
                            connection.Open();
                            MessageBox.Show("1.1"); //Верно
                            var Login = txtlogreg.Text.ToLower();
                            var Pass = SimpleComand.GetHash(txtpassreg.Password);
                            string query = $@"INSERT INTO ACCOUNTS ('Login','Pass','IPReg',IPLast) VALUES (@Login,@Pass,@IPReg,@IPLast)";
                            SQLiteCommand cmd = new SQLiteCommand(query, connection);
                            cmd.Parameters.AddWithValue("@Login", txtlogreg.Text.ToLower());
                            cmd.Parameters.AddWithValue("@Pass", Pass);
                            cmd.Parameters.AddWithValue("@IPReg", IPReg);
                            cmd.Parameters.AddWithValue("@IPLast", IPLast);
                            cmd.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show("Аккаунт зарегистрирован.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            // MessageBox.Show($@"{Pass}");
                        }
                    }
                    else
                    {
                        MessageBox.Show("1.2"); //Неверно
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void InfoIP() //Получение ip-адреса.
        {
            // Получение имени компьютера.
            String host = System.Net.Dns.GetHostName();
            // Получение ip-адреса.
            System.Net.IPAddress IPReg0 = System.Net.Dns.GetHostByName(host).AddressList[0];
            System.Net.IPAddress IPLast0 = System.Net.Dns.GetHostByName(host).AddressList[0];
            IPLast = IPLast0.ToString();
            IPReg = IPReg0.ToString();
            // MessageBox.Show(IPReg.ToString());
        }

    }
}
