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
    /// Логика взаимодействия для EditStudent.xaml
    /// </summary>
    public partial class EditStudent : Window
    {
        Core db = new Core();
        Student selectedStudent;
        public List<Group> Groups;
        public List<Subjects> Subjects;
        public List<EventType> EventTypes;
        private int IdStudent;
        public EditStudent(int idStudent)
        {
            InitializeComponent();
            IdStudent = idStudent;
            selectedStudent = db.context.Student.Where(x=> x.IdStudent == idStudent).FirstOrDefault();
            NameStudent.Text = selectedStudent.FIO;
            Groups = db.context.Group.ToList();
            EventTypes = db.context.EventType.ToList();
            EventType.ItemsSource = EventTypes;
            EventType.DisplayMemberPath = "Name";
            EventType.SelectedValuePath = "IdTypeEvent";
            ComboBoxStudent.ItemsSource = Groups;
            ComboBoxStudent.SelectedItem = Groups.Where(x => x.IdGroup == selectedStudent.GroupId).FirstOrDefault();
            ComboBoxStudent.DisplayMemberPath = "NameGroup";
            ComboBoxStudent.SelectedValuePath = "IdGroup";
            Subjects = db.context.Subjects.ToList();
            DataGridSubject.ItemsSource = Subjects;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selectedStudent.GroupId = (int)ComboBoxStudent.SelectedValue;
            Event newEvent = new Event
            {
                Name = NameEventSearch.Text,
                Date = EventDatePicker.SelectedDate.HasValue ? EventDatePicker.SelectedDate.Value : DateTime.Now,
                Type = (int)EventType.SelectedValue
            };
            db.context.Event.Add(newEvent);
           
            int eventMark;
            if (!int.TryParse(EventMark.Text, out eventMark))
            {
                MessageBox.Show("Пожалуйста, введите корректную оценку.");
                return;
            }
            EventStudent neweventStudent = new EventStudent
            {
                IdStudent = selectedStudent.IdStudent,
                IdEvent = newEvent.IdEvent,
                IdMark = eventMark
            };
            db.context.EventStudent.Add(neweventStudent);
            try
            {
                db.context.SaveChanges();
                if (db.context.SaveChanges() == 0)
                    MessageBox.Show("Успешное изменение");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddMark addMark = new AddMark(IdStudent);
            addMark.ShowDialog();
        }

    }
}
