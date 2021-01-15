using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Serialization;
using System.IO;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace lista_4
{


    public class Nobel_Prize_winner : IDataErrorInfo
    {
        bool checkf, checkl, checky;
        public bool cansave,fncansave,lncansave,fcansave,ncansave,ycansave;

        [XmlElement("Date of birth")]
        public DateTime BirthD { get; set; }
        [XmlElement("Date of death")]
        public DateTime DeathD { get; set; }
        [XmlElement("First name")]
        private string firstname;
        public string FirstName
        {
            get { return firstname; }
            set { firstname = value; }
        }

        [XmlElement("Last name")]
        private string lastname;
        public string LastName
        {
            get { return lastname; }
            set { lastname = value; }
        }
        [XmlElement("Nationality")]
        private string nationality;
        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
        [XmlElement("Field")]
        private string field;
        public string Field
        {
            get { return field; }
            set { field = value; }
        }
        [XmlElement("Year")]
        private int year;
        public int Year
        {
            get { return year; }
            set { year = value; }
        }
        [XmlIgnore]
        public BitmapImage Picture { get; set; }


        [XmlElement("Image")]
        public byte[] LargeIconSerialized
        {
            get
            { // serialize
                if (Picture == null) return null;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(Picture));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    return ms.ToArray();
                }
            }
            set
            { // deserialize
                if (value == null)
                {
                    Picture = null;
                }
                else
                {
                    using (MemoryStream ms = new MemoryStream(value))
                    {
                        Picture = new BitmapImage();
                        Picture.BeginInit();
                        Picture.CacheOption = BitmapCacheOption.OnLoad;
                        Picture.StreamSource = ms;
                        Picture.EndInit();
                    }
                }
            }
        }

        public Nobel_Prize_winner()
        {
            BirthD = DateTime.MinValue;
            DeathD = DateTime.MinValue;
            FirstName = "";
            LastName = "";
            Field = "";
            Nationality = "";
            Year = 1901;
            Picture = new BitmapImage(new Uri(@"C:\Users\katar\Desktop\programowanie 3\lista 4\lista 4\bin\images\no.png", UriKind.Absolute));
        }

        public Nobel_Prize_winner(DateTime BirthD, DateTime DeathD, string FirstName, string LastName, string Nationality, string Field, int Year, BitmapImage Picture)
        {
            this.BirthD = BirthD;
            this.DeathD = DeathD;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Nationality = Nationality;
            this.Field = Field;
            this.Year = Year;
            this.Picture = Picture;
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string PropertyName]
        {
            get
            {
                string result = String.Empty;
                checkf = CheckForDigits(FirstName);
                checkl = CheckForDigits(LastName);
                checky = CheckYear(Year);
                switch (PropertyName)
                {
                    case "FirstName":
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            result = "First name is required!";
                            fncansave = false;
                        }
                        else if (checkf == false)
                        {
                            result = "First name can not contain digits!";
                            fncansave = false;
                        }
                        else
                        {
                            fncansave = true;
                        }
                        break;
                    case "LastName":
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            result = "Last name is required!";
                            lncansave = false;
                        }
                        else if (checkl == false)
                        {
                            result = "Last name can not contain digits!";
                            lncansave = false;
                        }
                        else
                        {
                            lncansave = true;
                        }
                        break;
                    case "Nationality":
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            result = "Nationality is required!";
                            ncansave = false;
                        }
                        else
                        {
                            ncansave = true;
                        }
                        break;
                    case "Field":
                        if (string.IsNullOrEmpty(FirstName))
                        {
                            result = "Field is required!";
                            fcansave = false;
                        }
                        else
                        {
                            fcansave = true;
                        }
                        break;
                    case "Year":
                        if ( checky==false )
                        {
                            result = "Choose year from 1901 to 2020";
                            ycansave = false;
                        }
                        else
                        {
                            ycansave = true;
                        }
                            break;
                }
                if(fncansave ==true && lncansave == true && fcansave == true && ncansave == true&&ycansave == true)
                {
                    cansave = true;
                }
                else
                {
                    cansave=false;
                }
                return result;
            }
        }

        bool CheckForDigits(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.IsDigit(str[i])) return false;
            }
            return true;
        }

        bool CheckYear(int y)
        {
            if (y < 1901 || y > 2020)
            {
                return false;
            }
            else { return true; };
        }

    }
}
