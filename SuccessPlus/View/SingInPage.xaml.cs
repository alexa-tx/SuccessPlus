using SuccessPlus.Model;
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

namespace SuccessPlus.View
{
    /// <summary>
    /// Логика взаимодействия для SingInPage.xaml
    /// </summary>
    public partial class SingInPage : Page
    {
        public Core db = new Core();
        private SignIn _singIn;
        private User _user;
        public SingInPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(db.context.SignIn.Where(x => x.Login == LoginTextBox.Text).Count() == 1
                && db.context.SignIn.Where(x => x.Password == PasswordTextBox.Text).Count()==1)
            {
                _singIn = db.context.SignIn.Where(x => x.Login == LoginTextBox.Text).FirstOrDefault();
                _user = db.context.User.Where(x=> x.IdUser == _singIn.IdUser).FirstOrDefault();
                Properties.Settings.Default.userId = _user.IdUser;
                Properties.Settings.Default.loginUser = LoginTextBox.Text;
                Properties.Settings.Default.userRole = _user.Type;  
                Properties.Settings.Default.Save();
                this.NavigationService.Navigate(new HomePage());
                
            }
            
        }
    }
}
