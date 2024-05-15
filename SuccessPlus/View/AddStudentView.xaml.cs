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
    /// Логика взаимодействия для AddStudentView.xaml
    /// </summary>
    public partial class AddStudentView : Window
    {
        public Core db = new Core();
        List<Group> GroupList { get; set; } = new List<Group>();
        public AddStudentView()
        {
            InitializeComponent();
            GroupList = db.context.Group.ToList();
            GroupComboBox.ItemsSource = GroupList;
            GroupComboBox.SelectedValuePath = "IdGroup";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Student student = new Student
            {
                FisrtName = FirstName.Text,
                LastName = LastName.Text,
                GroupId = (int)GroupComboBox.SelectedValue
            };
            try
            {
                db.context.Student.Add(student);
                db.context.SaveChanges();
                if (db.context.SaveChanges() == 0)
                    MessageBox.Show("Успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
