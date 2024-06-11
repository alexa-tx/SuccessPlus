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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuccessPlus.View
{
    /// <summary>
    /// Логика взаимодействия для File.xaml
    /// </summary>
    public partial class File : Page
    {
         private Core _db = new Core();
        public File()
        {
            InitializeComponent();
        }

        private void ReportStudents_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper())
                {
                    if (excelHelper.Open(filePath: System.IO.Path.Combine(Environment.CurrentDirectory, "Test4.xlsx")))
                    {
                        List<Student> students = _db.context.Student.ToList();
                        string[] rowArray = new string[] { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
                        var i = 2;
                        foreach (var item in students)
                        {

                            excelHelper.Set(column: "A", i, data: $"{item.FIO}");
                            excelHelper.Set(column: "B", i, data: $"{item.GroupName}");
                            excelHelper.Set(column: "C", i, data: $"{item.TotalVisiting}");
                            excelHelper.Set(column: "D", i, data: $"{item.AVGEventMark}");
                            excelHelper.Set(column: "E", i, data: $"{item.AVGMark}");
                            excelHelper.Set(column: "F", i, data: $"{item.RatingScore}");

                            i++;

                        }







                        excelHelper.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            
        }

        private void ReportGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper())
                {
                    if (excelHelper.Open(filePath: System.IO.Path.Combine(Environment.CurrentDirectory, "Test3.xlsx")))
                    {
                        List<Group> groups = _db.context.Group.ToList();
                        string[] rowArray = new string[] { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
                        var i = 0;
                        foreach (var item in groups)
                        {

                            excelHelper.Set(column: $"{rowArray[i]}", row: 2, data: $"{item.NameGroup}");
                            excelHelper.Set(column: $"{rowArray[i]}", row: 3, data: $"{_db.context.Student.Where(x => x.GroupId == item.IdGroup).Count()}");
                            excelHelper.Set(column: $"{rowArray[i]}", row: 4, data: $"{item.AverageGroupRating.ToString("#.##")}");
                            excelHelper.Set(column: $"{rowArray[i]}", row: 5, data: $"{_db.context.VisitingStudent.Where(x => x.Student.GroupId == item.IdGroup).Count()}");
                            i++;

                        }







                        excelHelper.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
            }
            
        }

        private void ReportDepartment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (ExcelHelper excelHelper = new ExcelHelper())
                {
                    if (excelHelper.Open(filePath: System.IO.Path.Combine(Environment.CurrentDirectory, "Test5.xlsx")))
                    {
                        List<Departmen> groups = _db.context.Departmen.ToList();
                        string[] rowArray = new string[] { "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
                        var i = 0;
                        foreach (var item in groups)
                        {

                            excelHelper.Set(column: $"{rowArray[i]}", row: 2, data: $"{item.NameDepartmen}");
                            excelHelper.Set(column: $"{rowArray[i]}", row: 3, data: $"{_db.context.Group.Where(x => x.IdDepartmen == item.IdDepartmen).Count()}");
                            excelHelper.Set(column: $"{rowArray[i]}", row: 4, data: $"{_db.context.VisitingStudent.Where(x => x.Student.Group.Departmen.IdDepartmen == item.IdDepartmen).Count()}");

                            i++;

                        }







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
