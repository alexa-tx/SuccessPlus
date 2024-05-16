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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SuccessPlus.View
{
    /// <summary>
    /// Логика взаимодействия для Students.xaml
    /// </summary>
    public partial class Students : Page
    {
        public List<Student> StudentList { get; set; } = new List<Student>();

        private Core _db = new Core();
        public int SelectedStudentId { get; set; } 
        public Students()
        {

            InitializeComponent();
            StudentList = _db.context.Student.ToList();
            DataGrid.ItemsSource = StudentList;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStudentView addStudentView = new AddStudentView();
            addStudentView.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show(
        "Вы уверенны,что хотите удалить?",
        "Удаление студента",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Exclamation,
        MessageBoxDefaultButton.Button2
        );

            if (result == DialogResult.Yes)
            {
                Student selectedStudent = _db.context.Student.Where(x => x.IdStudent == (int)DataGrid.SelectedValue).FirstOrDefault();
                _db.context.Student.Remove(selectedStudent);
                _db.context.SaveChanges();
                if (_db.context.SaveChanges() == 0)
                    System.Windows.MessageBox.Show("Удалено");

            }

        }
    }
}
