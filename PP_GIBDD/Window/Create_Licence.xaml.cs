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

namespace PP_GIBDD.Window
{
    /// <summary>
    /// Логика взаимодействия для Create_Licence.xaml
    /// </summary>
    public partial class Create_Licence : Window
    {
        private int _id;

        private Windows.MainWindow _mainWindow;

        private Licence _licence;

        public CreateLicence()
        {
            InitializeComponent();

            using (var productionPracticeEntities1 = new ProductionPracticeEntities1())
            {
                _id = productionPracticeEntities1.Licences.ToList().Count() + 1;
                IdBox.Text = _id.ToString();

                IdBox.IsReadOnly = true;

                StatusLicenceComBox.ItemsSource = productionPracticeEntities1.StatusLicences.ToList();
                NameDriverComBox.ItemsSource = productionPracticeEntities1.Drivers.ToList();
            }
        }

        private void CreateClub_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime dateTimeCreate = Convert.ToDateTime(CreateDp.Text);
                DateTime dateTimeEnd = Convert.ToDateTime(EndDp.Text);
            }
            catch
            {
                MessageBox.Show("Укажите верную дату");
                return;
            }

            if (String.IsNullOrEmpty(CategoryBox.Text))
            {
                MessageBox.Show("Поле Категория лицензии должно быть заполнено");
                return;
            }

            if (String.IsNullOrEmpty(LicenceSerialBox.Text))
            {
                MessageBox.Show("Поле Серия лицензии должно быть заполнено");
                return;
            }

            if (String.IsNullOrEmpty(LicenceNumberBox.Text))
            {
                MessageBox.Show("Поле Номер лицензии должно быть заполнено");
                return;
            }

            if (String.IsNullOrEmpty(NameDriverComBox.Text))
            {
                MessageBox.Show("Выберите Водителя");
                return;
            }

            if (String.IsNullOrEmpty(StatusLicenceComBox.Text))
            {
                MessageBox.Show("Выберите Статус лицензии");
                return;
            }

            using (var productionPracticeEntities1 = new ProductionPracticeEntities1())
            {
                _licence = new Licence();

                _licence.LicenceDate = CreateDp.Text;
                _licence.ExpireDate = EndDp.Text;
                _licence.Categories = CategoryBox.Text;
                _licence.LicenceSeries = int.Parse(LicenceSerialBox.Text);
                _licence.LicenceNumber = int.Parse(LicenceNumberBox.Text);

                Driver item = (Driver)NameDriverComBox.SelectedItem;

                _licence.IdDriver = item.Id;

                StatusLicence statusLicence = (StatusLicence)StatusLicenceComBox.SelectedItem;
                _licence.IdStatus = statusLicence.Id;

                productionPracticeEntities1.Licences.Add(_licence);
                productionPracticeEntities1.SaveChanges();

                MessageBox.Show("Лицензия успешно добавлена");
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_mainWindow == null)
            {
                _mainWindow = new Windows.MainWindow();
                _mainWindow.Show();
                this.Close();
            }
        }

        private void CreateDp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void EndDp_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void LicenceSerialBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void LicenceNumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
