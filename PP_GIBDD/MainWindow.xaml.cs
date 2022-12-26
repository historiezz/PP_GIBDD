using PP_GIBDD.Window;
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

namespace PP_GIBDD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        private Create_Driver createDriver;

        private Windows.CreateLicence createLicence;

        public MainWindow()
        {
            InitializeComponent();

            Drivers.IsEnabled = false;
            InitPage(@"Pages/Drivers_Page.xaml");
            Pages.Drivers_Page.mainWindow = this;
        }

        public void InitPage(string uri)
        {
            MainFrame.NavigationService.Navigate(new Uri(uri, UriKind.Relative));
        }

        private void Drivers_Click(object sender, RoutedEventArgs e)
        {
            InitPage(@"Pages/Drivers_Page.xaml");
            Licence.IsEnabled = true;
            Drivers.IsEnabled = false;
        }

        private void CreateDriver_Click(object sender, RoutedEventArgs e)
        {
            if (createDriver == null)
            {
                createDriver = new Create_Driver();
                createDriver.Show();
                this.Close();
            }
        }

        private void Licence_Click(object sender, RoutedEventArgs e)
        {
            InitPage(@"Pages/Licence.xaml");
            Drivers.IsEnabled = true;
            Licence.IsEnabled = false;
        }

        private void CreateLicence_Click(object sender, RoutedEventArgs e)
        {
            if (createLicence == null)
            {
                createLicence = new Create_Licence();
                createLicence.Show();
                this.Close();
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
}
