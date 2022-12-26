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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        private Windows.MainWindow _mainWindow;

        private int _countAttempts = 0;
        private bool _autoflag = false;

        public Authorize()
        {
            InitializeComponent();

        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(LoginBox.Text) || String.IsNullOrEmpty(PasswordBox.Text))
            {
                MessageBox.Show("Поля Логин и Пароль должны быть заполнены.");
            }

            using (ProductionPracticeEntities1 productionPracticeEntities1 = new ProductionPracticeEntities1())
            {
                foreach (var item in productionPracticeEntities1.Users)
                {
                    if (item.Login == LoginBox.Text && item.Password == PasswordBox.Text)
                    {
                        _autoflag = true;

                        if (_mainWindow == null)
                        {
                            _mainWindow = new Windows.MainWindow();
                            _mainWindow.Show();
                            this.Close();
                        }
                    }
                }
            }

            if (!_autoflag)
            {
                MessageBox.Show("Неправильный логин или пароль.");
            }
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Заявка отправлена администратору");
            RegBtn.IsEnabled = false;
        }
    }
}
