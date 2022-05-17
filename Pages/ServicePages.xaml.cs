using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для ServicePages.xaml
    /// </summary>
    public partial class ServicePages : Window
    {
        public ServicePages()
        {
            InitializeComponent();
            LoadList();
            
        }
        
        public void LoadList()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(SqlDBConnection.connection))
                {
                    connection.Open();
                    string query = $@"SELECT PCService.ID,Type,Status,Masters.Name,Masters.Family,Money,StartData FROM PCService 
                                      JOIN TypesPc on PCService.IDTypePC = TypesPc.ID
                                      JOIN Statuses on PCService.IDStatus = Statuses.ID
                                      JOIN Masters on PCService.IDMaster = Masters.ID
                                        WHERE IDAccount = {Saver.IDAcc}";

                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    DataTable DT = new DataTable("Traffics");
                    SQLiteDataAdapter SDA = new SQLiteDataAdapter(cmd);
                    SDA.Fill(DT);
                    service.ItemsSource = DT.DefaultView;
                    cmd.ExecuteNonQuery();


                    //SELECT Type,Status,Masters.Name,Masters.Family,Money,StartData FROM PCService 
                    //                    JOIN TypesPc on PCService.IDTypePC = TypesPc.ID
                    //JOIN Statuses on PCService.IDStatus = Statuses.ID
                    //JOIN Masters on PCService.IDMaster = Masters.ID
                    //WHERE IDAccount = 1
                    // SQLiteDataReader dr = null;
                    //SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    // agents.Items.Clear();
                    //SQLiteDataReader dataReader = cmd.ExecuteReader();
                    //if (dataReader.HasRows)
                    // {
                    //CategoryList.BeginUpdate();
                    //   while (dataReader.Read())
                    //   {
                    // CategoryList.Items.Add(dataReader.GetInt32(0));
                    //CategoryList.Items.Add(dataReader.GetString(1));
                    //   agents.Items.Add(dataReader["ID"]);
                    //  }
                    //CategoryList.EndUpdate();
                    //  }
                    // DataSet dtSet = new DataSet();
                    //var ds = new DataSet();
                    // query.Fill(ds);
                    //SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                    //adapter.SelectCommand = cmd;
                    //adapter.Fill(dtSet, "PCService");
                    //var sda = new SQLiteCommand(query);
                    //var ds = new DataSet();
                    //listBox1.DataContext = dtSet;

                    //CategoryList.Items.Clear();
                    //da.Fill(ds);
                    // dr = cmd.ExecuteReader();

                    //PCServiceInfo.Items.Clear();
                    //PCServiceInfo.ItemsSource = dr["PCServiceInfo"].ToString();
                    //while (dr.Read())
                    // {
                    //      CategoryList.Items.Add(dr["ФИО"].ToString());
                    //CategoryList.Items.Add(dr["id"]);
                    //listBox1.Items.Add(dr["name"]);
                    //listBox1.Items.Add(dr["lastname"]);
                    //string sLastName = dr["ID"].ToString();
                    //PCServiceInfo.Items.Add(sLastName);
                    //string id = dr["ID"].ToString();
                    //var newit = new ListBoxItem(id);
                    //PCServiceInfo.Items.add(newit);
                    //INSERT INTO PCService(IDTypePC, IDStatus, IDMaster, Money, StartData) VALUES('1', '1', '1', '3000', '17.05.2022');
                    // PCServiceInfo.Items.Add(dr.GetValue(0).ToString());
                    // PCServiceInfo.Items.Add(dr.GetString(0));
                    // }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка" + ex);
            }
        }

    }
}
