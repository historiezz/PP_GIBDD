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

namespace PP_GIBDD.Pages
{
    /// <summary>
    /// Логика взаимодействия для Drivers_Page.xaml
    /// </summary>
    public partial class Drivers_Page : Page
    {
        private ProductionPracticeEntities1 productionPracticeEntities = new ProductionPracticeEntities1();

        private Windows.EditDriver editDriver;

        public static Windows.MainWindow mainWindow;

        public DriversPage()
        {
            InitializeComponent();

            DataGridDrivers.ItemsSource = productionPracticeEntities.Drivers.ToList();
        }

        private void EditColumn_Click(object sender, RoutedEventArgs e)
        {
            if (editDriver == null)
            {
                Pages.DriversPage driversPage = this;
                editDriver = new Windows.EditDriver(sender, driversPage);
                editDriver.Show();
                mainWindow.Close();
            }
        }
    }
}
