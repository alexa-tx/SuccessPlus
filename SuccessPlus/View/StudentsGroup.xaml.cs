using SuccessPlus.Model;
using SuccessPlus.Services;
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

            if (Properties.Settings.Default.userRole == 3)
                BtnAddGroups.Visibility = Visibility.Collapsed;
            else
                BtnAddGroups.Visibility = Visibility.Visible;


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
            if (Properties.Settings.Default.userRole == 3)
                System.Windows.MessageBox.Show("У вас нет доступа");
            else
            {
                EditGroupWindow editGroupWindow = new EditGroupWindow((int)DataGrid.SelectedValue);
                editGroupWindow.ShowDialog();
            }
                
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (IsAdmin(Properties.Settings.Default.userRole))
                System.Windows.MessageBox.Show("У вас нет доступа");
            else
            {
                DialogResult result = System.Windows.Forms.MessageBox.Show(
                    "Вы уверенны,что хотите удалить?",
                    "Удаление группы",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2
                    );

                if (result == DialogResult.Yes)
                {
                    Group selectedGroup = _db.context.Group.Where(x => x.IdGroup == (int)DataGrid.SelectedValue).FirstOrDefault();
                    _db.context.Group.Remove(selectedGroup);
                    _db.context.SaveChanges();
                    if (_db.context.SaveChanges() == 0)
                        System.Windows.MessageBox.Show("Удалено");

                }
            }

                

        }
        public static bool IsAdmin(int userRole)
        {
            if (userRole == 3)
                return true;
            return false;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper())
                {

                    if (excelHelper.Open(filePath: System.IO.Path.Combine(Environment.CurrentDirectory, "Test2.xlsx")))
                    {
                        Group SelectedGroup = _db.context.Group.Where(x => x.IdGroup == (int)DataGrid.SelectedValue).FirstOrDefault();
                        excelHelper.Set(column: "B", row: 2, data: $"{SelectedGroup.NameGroup}");
                        excelHelper.Set(column: "B", row: 3, data: $"{SelectedGroup.Departmen.NameDepartmen}");
                        excelHelper.Set(column: "B", row: 4, data: $"{SelectedGroup.AverageGrade.ToString("#.##")}");//
                        var a = (SelectedGroup.TotalPublicActivity + SelectedGroup.TotalArtActivity + SelectedGroup.TotalSports + SelectedGroup.TotalNTT + 1)/4;
                        excelHelper.Set(column: "B", row: 5, data: $"{a.ToString("#.##")}");//
                        excelHelper.Set(column: "B", row: 6, data: $"{SelectedGroup.AverageGroupRating.ToString("#.##")}");
                        
                        
                        
                        
                        
                        excelHelper.Save();

                    }
                        
                    
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
        }
    }
}
