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

using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.Reporting;
using Microsoft.ReportingServices;
using Microsoft.Reporting.WinForms;
namespace lista_4
{

    public partial class Report : Window
    {
        public Report()
        {
            InitializeComponent();
            rptViewer.ServerReport.ReportServerUrl = new Uri("http://laptop-0v4r6qn9:80/ReportServer", System.UriKind.Absolute);
            rptViewer.ServerReport.ReportPath = "/Reports/Report1";
            rptViewer.RefreshReport();
        }

    }
}
