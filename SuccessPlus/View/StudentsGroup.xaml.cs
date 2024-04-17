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
using System.Windows.Shapes;

namespace SuccessPlus.View
{
    /// <summary>
    /// Логика взаимодействия для StudentsGroup.xaml
    /// </summary>
    public partial class StudentsGroup : Page
    {
        private Core _db = new Core();
        public List<GroupExtended> GroupList { get; set; }

        public int SelectedGroupId { get; set; } 

        public StudentsGroup()
        {
            InitializeComponent();

            var userSims = (from user in _db.context.Group
                           select new GroupExtended { IdGroup = user.IdGroup,
                               NameGroup = user.NameGroup,
                               CountStudents = _db.context.Student.Where(x => x.GroupId == user.IdGroup).Count(),
                           }).ToList();
            DataGrid.ItemsSource = userSims;
            DataGrid.SelectedValuePath = "IdGroup";

        }
        //кнопка добавить группу
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddGroupWindow addGroupWindow = new AddGroupWindow();
            addGroupWindow.ShowDialog();
        }
        //редактировать
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EditGroupWindow editGroupWindow = new EditGroupWindow((int)DataGrid.SelectedValue);
            editGroupWindow.ShowDialog();
        }
    }
}
