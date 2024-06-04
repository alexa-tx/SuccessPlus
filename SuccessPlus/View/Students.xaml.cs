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

        User curentUser = new User();
        public Students()
        {

            InitializeComponent();

             curentUser = _db.context.User.Where(x => x.IdUser == Properties.Settings.Default.userId).FirstOrDefault();
            if (Properties.Settings.Default.userRole == 4)
            {
                StudentList = _db.context.Student.Where(x => x.GroupId == curentUser.GroupId).ToList();
                
            }
            else
                StudentList = _db.context.Student.ToList();
            
            DataGridStudent.ItemsSource = StudentList;
            DataGridStudent.SelectedValuePath = "IdStudent";
            var SocialEvent = _db.context.EventStudent.Where(x => x.IdStudent == 9 && x.IdEvent == 3).Select(x => x.IdMark).ToList();


            var marks = SocialEvent.Where(mark => mark.HasValue).Select(mark => mark.Value);

            if (Properties.Settings.Default.userRole == 3)
                BtnAddStudent.Visibility = Visibility.Collapsed;
            else
                BtnAddStudent.Visibility = Visibility.Visible;

            if (Properties.Settings.Default.userRole == 4)
                BtnAddStudent.Visibility = Visibility.Collapsed;
            else
                BtnAddStudent.Visibility = Visibility.Visible;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void Filter()
        {
            StudentList = _db.context.Student.ToList();
            List<DateTime> Date = _db.context.MarkStudent.Where(x => x.IdStudent == 4).Select(x => x.Date).ToList();
             DateTime? selectedDate = DatePickerMark.SelectedDate;
            if (selectedDate.HasValue)
            {
                StudentList = StudentList.Where(student => student.Date.Contains((DateTime)DatePickerMark.SelectedDate))
                    .ToList();
                StudentList = StudentList.Where(x => x.TotalVisiting.ToString().ToLower().Contains(Poisk.Text.ToLower()) || x.AVGMark.ToString().ToLower().Contains(Poisk.Text.ToLower())
                || x.FisrtName.ToString().ToLower().Contains(Poisk.Text.ToLower()) || x.LastName.ToString().ToLower().Contains(Poisk.Text.ToLower()) ||
                x.GroupName.ToString().ToLower().Contains(Poisk.Text.ToLower())).ToList();
            }
            else
            {
                StudentList = StudentList.Where(x => x.TotalVisiting.ToString().ToLower().Contains(Poisk.Text.ToLower()) || x.AVGMark.ToString().ToLower().Contains(Poisk.Text.ToLower())
                || x.FisrtName.ToString().ToLower().Contains(Poisk.Text.ToLower()) || x.LastName.ToString().ToLower().Contains(Poisk.Text.ToLower()) ||
                x.GroupName.ToString().ToLower().Contains(Poisk.Text.ToLower())).ToList();
            }
            //StudentList = StudentList.Where(student => student.Date.ToString().Contains(DatePickerMark.SelectedDate.ToString()))
            //.ToList();
            //StudentList = StudentList.Where(x => x.TotalVisiting.ToString().Contains(Poisk.Text) || x.AVGMark.ToString().Contains(Poisk.Text)
            //|| x.FisrtName.ToString().Contains(Poisk.Text) || x.LastName.ToString().Contains(Poisk.Text) || 
            //x.GroupName.ToString().Contains(Poisk.Text)).ToList();
            DataGridStudent.ItemsSource = StudentList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddStudentView addStudentView = new AddStudentView();
            addStudentView.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.userRole == 4 )
                System.Windows.MessageBox.Show("У вас нет доступа");
            else
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
                    Student selectedStudent = _db.context.Student.Where(x => x.IdStudent == (int)DataGridStudent.SelectedValue).FirstOrDefault();
                    _db.context.Student.Remove(selectedStudent);
                    _db.context.SaveChanges();
                    if (_db.context.SaveChanges() == 0)
                        System.Windows.MessageBox.Show("Удалено");

                }
            }

           

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EditStudent editStudent = new EditStudent((int)DataGridStudent.SelectedValue);
            editStudent.ShowDialog();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Проверяем, выбрана ли какая-либо строка
            if (DataGridStudent.SelectedItem != null)
            {
                // Получаем выбранного студента
                var selectedStudent = DataGridStudent.SelectedItem as Student;
                if (selectedStudent != null)
                {
                    // Формируем информацию о студенте
                    StringBuilder info = new StringBuilder();
                    info.AppendLine($"Студент: {selectedStudent.FisrtName} {selectedStudent.LastName}");
                    info.AppendLine($"Количество оценок: {selectedStudent.CountFives + selectedStudent.CountFours + selectedStudent.CountThrees}");
                    info.AppendLine($"Средний балл: {selectedStudent.AVGMark}");
                    info.AppendLine($"Средняя оценка за поведение: {selectedStudent.BehaviorScore}");
                    info.AppendLine($"Средняя оценка за задолженности: {selectedStudent.DebtScore}");
                    info.AppendLine($"Средняя оценка за бонус руководства: {selectedStudent.LeadershipBonus}");

                    // Проверяем, где участвовал студент
                    if (selectedStudent.AVGSocialEvent > 0)
                    {
                        string socialEvents = string.Join(", ", selectedStudent.NameSocialEvent);
                        info.AppendLine($"Участие в общественных мероприятиях: {socialEvents}");
                    }
                    if (selectedStudent.AVGSportEvent > 0)
                    {
                        string sportEvents = string.Join(", ", selectedStudent.NameSportEvent);
                        info.AppendLine($"Участие в спортивных мероприятиях: {sportEvents}");
                    }
                    if (selectedStudent.AVGNttEvent > 0)
                    {
                        string nttEvents = string.Join(", ", selectedStudent.NameNttEvent);
                        info.AppendLine($"Участие в научно-технологических мероприятиях: {nttEvents}");
                    }
                    if (selectedStudent.AVGSelfDev > 0)
                    {
                        string selfDevEvents = string.Join(", ", selectedStudent.SelfDevEvent);
                        info.AppendLine($"Участие в мероприятиях самосовершенствования: {selfDevEvents}");
                        
                    }
                    if (selectedStudent.AVGAmateurEvent > 0)
                    {
                        string amateurEvents = string.Join(", ", selectedStudent.AmateurEvent);
                        info.AppendLine($"Участие в любительских мероприятиях: {amateurEvents}");
                    }


                    // Отображаем информацию
                    System.Windows.MessageBox.Show(info.ToString(), "Информация о студенте");
                }
            }
            }

        private void DatePickerMark_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
