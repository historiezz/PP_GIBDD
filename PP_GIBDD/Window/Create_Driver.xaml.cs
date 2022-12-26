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
    /// Логика взаимодействия для Create_Driver.xaml
    /// </summary>
    public partial class Create_Driver : Window
    {
        private int _driverId;

        private List<string> _cities;

        private string _fileName;

        private Driver _driver;

        private Windows.MainWindow mainWindow;

        public CreateDriver()
        {
            InitializeComponent();

            using (var productionPracticeEntities1 = new ProductionPracticeEntities1())
            {
                _driverId = productionPracticeEntities1.Drivers.ToList().Count() + 1;

                IdBox.Text = _driverId.ToString();
                IdBox.IsReadOnly = true;

                _cities = new List<string>();

                foreach (var item in productionPracticeEntities1.Cities)
                {
                    _cities.Add(item.City1);
                }
            }

            CityRegComBox.ItemsSource = _cities;
            CityLifeComBox.ItemsSource = _cities;
        }

        private void CityRegComBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CityRegComBox.IsDropDownOpen = true;
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CityRegComBox.ItemsSource);
            cv.Filter = s =>
                ((string)s).IndexOf(CityRegComBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void CityLifeComBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CityLifeComBox.IsDropDownOpen = true;
            var tb = (TextBox)e.OriginalSource;
            tb.Select(tb.SelectionStart + tb.SelectionLength, 0);
            CollectionView cv = (CollectionView)CollectionViewSource.GetDefaultView(CityLifeComBox.ItemsSource);
            cv.Filter = s =>
                ((string)s).IndexOf(CityLifeComBox.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void CreateClub_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameBox.Text))
            {
                MessageBox.Show("Поле Фамилия должно быть заполнено");
                return;
            }

            if (string.IsNullOrEmpty(ModdleNameBox.Text))
            {
                MessageBox.Show("Поле Имя должно быть заполнено");
                return;
            }

            if (string.IsNullOrEmpty(LastNameBox.Text))
            {
                MessageBox.Show("Поле Отчество должно быть заполнено");
                return;
            }

            if (string.IsNullOrEmpty(PassportSerialBox.Text))
            {
                MessageBox.Show("Поле Серия паспорта должно быть заполнено");
                return;
            }

            if (string.IsNullOrEmpty(PassportNumberBox.Text))
            {
                MessageBox.Show("Поле номер паспорта должно быть заполнено");
                return;
            }

            if (string.IsNullOrEmpty(PostCodeBox.Text))
            {
                MessageBox.Show("Поле Почтовый индекс должно быть заполнено");
                return;
            }

            if (string.IsNullOrEmpty(PhoneBox.Text))
            {
                MessageBox.Show("Поле Телефон должно быть заполнено");
                return;
            }

            if (string.IsNullOrEmpty(ImageBox.Text))
            {
                MessageBox.Show("Поле Изображение должно быть заполнено");
                return;
            }


            Regex regx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regx.Match(EmailBox.Text);

            if (!match.Success)
            {
                MessageBox.Show("Введите корректный адрес электронной почты");
                return;
            }

            try
            {
                using (var productionPracticeEntities1 = new ProductionPracticeEntities1())
                {
                    _driver = new Driver();
                    _driver.Id = int.Parse(IdBox.Text);
                    _driver.Name = NameBox.Text.Trim() + " " + LastNameBox.Text.Trim();
                    _driver.MiddleName = ModdleNameBox.Text.Trim();
                    _driver.PassportSerial = int.Parse(PassportSerialBox.Text);
                    _driver.PassportNumber = int.Parse(PassportNumberBox.Text);
                    _driver.PostCode = int.Parse(PostCodeBox.Text);
                    _driver.City = CityRegComBox.Text;
                    _driver.Address = AddressBox.Text;
                    _driver.CityLife = CityLifeComBox.Text;
                    _driver.AddressLife = AddressBox.Text;
                    _driver.Company = CompanyBox.Text;
                    _driver.JobName = JobNameBox.Text;
                    _driver.Phone = PhoneBox.Text;
                    _driver.Email = EmailBox.Text;
                    _driver.Image = ImageBox.Text;
                    _driver.Description = DescriptionBox.Text;

                    productionPracticeEntities1.Drivers.Add(_driver);
                    productionPracticeEntities1.SaveChanges();

                    MessageBox.Show("Водитель успешно добавлен");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка сохранения водителя");
            }

        }

        private void ChangeImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            if (ofd.ShowDialog() == true)
            {
                _fileName = ofd.FileName.ToString();
            }

            ImageBox.Text = _fileName;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindow == null)
            {
                mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        private void PassportSerialBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PassportNumberBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PostCodeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PhoneBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
}
