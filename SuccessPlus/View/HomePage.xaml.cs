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
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        
        private Core _db = new Core();

        public HomePage()
        {
            InitializeComponent();
            LoadStudentCount();
            LoadGroupCount();
            LoadUserFio();
        }

        private User GetCurrentUser()
        {
            return _db.context.User.Where(x => x.IdUser == Properties.Settings.Default.userId).First();

        }
        private void LoadUserFio()
        {
            FioTextBox.Text = $"{GetCurrentUser().LastName} {GetCurrentUser().FirstName}";
        }

        private void LoadStudentCount()
        {
            int studentCount = GetTotalStudentCount();
            StudentCountText.Text = studentCount.ToString();
        }

        private int GetTotalStudentCount()
        {
            
            return _db.context.Student.Count(); 
        }

        private void LoadGroupCount()
        {
            int groupCount = GetTotaGroupCount();
            GroupCountText.Text = groupCount.ToString();
        }

        private int GetTotaGroupCount()
        {
            
            return _db.context.Group.Count(); 
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.userRole == 1)
            {
                BtnAddUser.Visibility = Visibility.Visible;
                AddUser addUser = new AddUser();
                addUser.ShowDialog();
            }
            else
            {
                BtnAddUser.Visibility= Visibility.Collapsed;
            }
            

        }
    }
}
