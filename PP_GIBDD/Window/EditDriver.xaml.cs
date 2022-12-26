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
    /// Логика взаимодействия для EditDriver.xaml
    /// </summary>
    public partial class EditDriver : Window
    {
        private int _driverId;

        private List<string> _cities;

        private string _fileName;

        private Driver _driver;

        private Windows.MainWindow _mainWindow;

        public EditDriver(object clubObject, Pages.DriversPage driversPage)
        {
            InitializeComponent();

            using (var productionPracticeEntities1 = new ProductionPracticeEntities1())
            {
                _driverId = productionPracticeEntities1.Drivers.Count() + 1;

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

            InitWindowDriver(clubObject);
        }

        public void InitWindowDriver(object objectItem)
        {
            _driver = (objectItem as Button).DataContext as Driver;

            IdBox.Text = _driver.Id.ToString();

            var names = _driver.Name.Split(' ');

            NameBox.Text = names[0].ToString();
            LastNameBox.Text = names[1].ToString();
            ModdleNameBox.Text = _driver.MiddleName;

            PassportSerialBox.Text = _driver.PassportSerial.ToString();
            PassportNumberBox.Text = _driver.PassportNumber.ToString();
            PostCodeBox.Text = _driver.PostCode.ToString();
            CityRegComBox.Text = _driver.City;
            AddressBox.Text = _driver.Address.ToString();
            CityLifeComBox.Text = _driver.CityLife;
            AddressLifeBox.Text = _driver.AddressLife;
            CompanyBox.Text = _driver.Company;
            JobNameBox.Text = _driver.JobName;
            PhoneBox.Text = _driver.Phone;
            EmailBox.Text = _driver.Email;
            ImageBox.Text = _driver.Image;
            DescriptionBox.Text = _driver.Description;
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
                    var driver = new Driver();
                    driver.Id = int.Parse(IdBox.Text);
                    driver.Name = NameBox.Text.Trim() + " " + LastNameBox.Text.Trim();
                    driver.MiddleName = ModdleNameBox.Text.Trim();
                    driver.PassportSerial = int.Parse(PassportSerialBox.Text);
                    driver.PassportNumber = int.Parse(PassportNumberBox.Text);
                    driver.PostCode = int.Parse(PostCodeBox.Text);
                    driver.City = CityRegComBox.Text;
                    driver.Address = AddressBox.Text;
                    driver.CityLife = CityLifeComBox.Text;
                    driver.AddressLife = AddressBox.Text;
                    driver.Company = CompanyBox.Text;
                    driver.JobName = JobNameBox.Text;
                    driver.Phone = PhoneBox.Text;
                    driver.Email = EmailBox.Text;
                    driver.Image = ImageBox.Text;
                    driver.Description = DescriptionBox.Text;

                    var entityForRemove = productionPracticeEntities1.Drivers.Find(_driver.Id);
                    productionPracticeEntities1.Drivers.Remove(entityForRemove);
                    productionPracticeEntities1.Drivers.Add(driver);
                    productionPracticeEntities1.SaveChanges();

                    MessageBox.Show("Радактирование прошло успешно");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка редактирования водителя");
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (_mainWindow == null)
            {
                _mainWindow = new MainWindow();
                _mainWindow.Show();
                this.Close();
            }
        }
    }
}
