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
    /// Логика взаимодействия для Students.xaml
    /// </summary>
    public partial class Students : Page
    {
        public List<Student> StudentList {  get; set; } = new List<Student>();

        private Core _db = new Core();
        public Students()
        {

            InitializeComponent();
            StudentList = _db.context.Student.ToList();
            DataGrid.ItemsSource = StudentList;

           
        }
    }
}
