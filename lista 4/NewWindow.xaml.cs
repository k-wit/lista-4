using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace lista_4
{
    /// <summary>
    /// Logika interakcji dla klasy Window.xaml
    /// </summary>
    public partial class NewWindow : Window
    {
        BitmapImage Picture;


        List<string> Nationalities
        {
            get { return new List<string>() { "Polish", "German", "French", "English", "Czech" , "Danish" , "Greek", "Chinese" , "Hungarian" , "Italian" , "Slovak" , "Spanish" , "Swedish" ,"Swiss"}; }
        }  

        List<string> Fields
        {
            get { return new List<string>() { "Physics", "Chemistry", "Physiology or Medicine", "Literature", "Peace", "Economics" }; }
        }

        public Nobel_Prize_winner ReturnPerson
        {
            get;
            set;
        }

        public NewWindow()
        {
            InitializeComponent();
            OtherInitialize();
            cmbNationality.ItemsSource = Nationalities;
            cmbField.ItemsSource = Fields;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            ReturnPerson = (Nobel_Prize_winner)PersonForm.DataContext;
            if (ReturnPerson.cansave == true)
            {               
                this.Close();
            }
            else
            {
                MessageBox.Show("Wrong data.");
            }
        }
        private void OtherInitialize()
        {
            this.Closing += new CancelEventHandler(this.NewWindow_Closing);
        }
        
        void NewWindow_Closing(object sender, CancelEventArgs e)
        { 
            if (ReturnPerson==null)
            {
                e.Cancel = false;
                return;
            }
            ReturnPerson = (Nobel_Prize_winner)PersonForm.DataContext;
            if (ReturnPerson.cansave == false)
            {
                e.Cancel = true;
                MessageBox.Show("Enter the correct data and save it before closing.");
            }
            else
            {
                e.Cancel = false;
            }

        }
        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                imgDynamic.Source = new BitmapImage(fileUri);
                Picture = new BitmapImage(fileUri);
                ReturnPerson = (Nobel_Prize_winner)PersonForm.DataContext;
            }
        }
    }
}
