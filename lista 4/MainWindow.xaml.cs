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
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Data.SqlClient;

namespace lista_4
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Nobel_Prize_winner> NobelList = null;
        Database baza = new Database();

        public MainWindow()
        {
            InitializeComponent();

            XmlSerializer xs = new XmlSerializer(typeof(List<Nobel_Prize_winner>));
            try
            {
                using (Stream s = File.OpenRead("../list.xml"))
                {
                    NobelList = (List<Nobel_Prize_winner>)xs.Deserialize(s);
                }
            }
            catch { };

            DataTable dt = baza.readBase();
            //ListPersons.ItemsSource = dt.DefaultView;
            NobelList = new List<Nobel_Prize_winner>();
            foreach (DataRow row in dt.Rows)
            {
                NobelList.Add(new Nobel_Prize_winner(Convert.ToDateTime(row["Date of Birth"]), Convert.ToDateTime(row["Date of Death"]), row["First Name"].ToString(), row["Last Name"].ToString(), row["Nationality"].ToString(), row["Field"].ToString(), Convert.ToInt32(row["Year"]), row["Picture"].ToString()));
            }
            ListPersons.ItemsSource = NobelList;

            //InitBinding();
        }

        public void InitBinding()
        {
            var dbirth1 = new DateTime(1845, 03, 27, 0, 0, 0);
            var ddeath1 = new DateTime(1923, 02, 10, 0, 0, 0);
            NobelList = new List<Nobel_Prize_winner>();
            BitmapImage Picture1 = new BitmapImage(new Uri(@"C:\Users\katar\Desktop\programowanie 3\lista 4\lista 4\bin\images\roentgen.jpg", UriKind.Absolute));
            NobelList.Add(new Nobel_Prize_winner(dbirth1, ddeath1, "Wilhelm", "Röntgen", "German", "Physics", 1901, Picture1));

            var dbirth2 = new DateTime(1828, 05, 08, 0, 0, 0);
            var ddeath2 = new DateTime(1923, 11, 30, 0, 0, 0);
            BitmapImage Picture2 = new BitmapImage(new Uri(@"C:\Users\katar\Desktop\programowanie 3\lista 4\lista 4\bin\images\dunant.jpg", UriKind.Absolute));
            NobelList.Add(new Nobel_Prize_winner(dbirth2, ddeath2, "Henri", "Dunant", "Swiss", "Peace", 1901,Picture2));


            XmlSerializer xs = new XmlSerializer(typeof(List<Nobel_Prize_winner>));
            using (Stream s = File.Create("../list.xml"))
            {
                xs.Serialize(s, NobelList);
            }
            ListPersons.ItemsSource = NobelList;
        }

        private void New_Person_Click(object sender, RoutedEventArgs e)
        {
            NewWindow addPerson = new NewWindow(baza);
            Nobel_Prize_winner person = new Nobel_Prize_winner();
            addPerson.PersonForm.DataContext = person;

            addPerson.ShowDialog();
            if ((addPerson.ReturnPerson != null && addPerson.ReturnPerson.FirstName != null && addPerson.ReturnPerson.LastName != null && addPerson.ReturnPerson.Year > 1900))
            {
                NobelList.Add(addPerson.ReturnPerson);
            }

            ListPersons.ItemsSource = NobelList;
            ListPersons.Items.Refresh();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Nobel_Prize_winner>));
            using (Stream s = File.Create("../list.xml"))
            {
                xs.Serialize(s, NobelList);
            }
        }

        private void ListPersons_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NewWindow changePerson = new NewWindow(baza);
            changePerson.PersonForm.DataContext = ListPersons.SelectedItem;
            changePerson.ShowDialog();
        }

     /*   private void SQL_Connection_Click(object sender, RoutedEventArgs e)
        {
            string connectionString;
            SqlConnection cnn;

            //connectionString = 

            cnn = new SqlConnection(connectionString);

            cnn.Open();
            MessageBox.Show("Connection Open!");
            cnn.Close();
        }*/
    }
}
