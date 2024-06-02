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
        public List<MarkStudent> MarkStudents;
        public List<EventType> EventTypes;
        public List<TypeFine> Fines;
        public List<Marks> Marks;
        private int IdStudent;
        public EditStudent(int idStudent)
        {
            InitializeComponent();
            
            IdStudent = idStudent;
            selectedStudent = db.context.Student.Where(x=> x.IdStudent == idStudent).FirstOrDefault();
            NameStudent.Text = selectedStudent.FIO;
            Groups = db.context.Group.ToList();
            Fines = db.context.TypeFine.ToList();
            EventTypes = db.context.EventType.ToList();
            EventType.ItemsSource = EventTypes;
            EventType.DisplayMemberPath = "Name";
            EventType.SelectedValuePath = "IdTypeEvent";
            ComboBoxStudent.ItemsSource = Groups;
            ComboBoxStudent.SelectedItem = Groups.Where(x => x.IdGroup == selectedStudent.GroupId).FirstOrDefault();
            ComboBoxStudent.DisplayMemberPath = "NameGroup";
            ComboBoxStudent.SelectedValuePath = "IdGroup";
            FineTypeComboBox.ItemsSource = Fines;
            FineTypeComboBox.DisplayMemberPath = "NameType";
            FineTypeComboBox.SelectedValuePath = "IdTypeFine";
            Marks = db.context.Marks.ToList();
            FineMarkComboBox.ItemsSource = Marks;
            FineMarkComboBox.SelectedValuePath = "IdMark";
            FineMarkComboBox.DisplayMemberPath = "MarkName";
            


            //Subjects = db.context.Subjects.Where(x => x.).ToList();
            MarkStudents = db.context.MarkStudent.Where(x => x.IdStudent == idStudent).ToList();
            DataGridSubject.ItemsSource = MarkStudents;
            
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
            if (FineTypeComboBox.SelectedValue != null && FineMarkComboBox.SelectedValue != null)
            {
                Fine newFine = new Fine
                {
                    TypeFine = (int)FineTypeComboBox.SelectedValue,
                    IdMark = (int)FineMarkComboBox.SelectedValue,
                    IdStudent = selectedStudent.IdStudent
                };
                db.context.Fine.Add(newFine);
            }
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

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.userRole != 1)
            {
                TextGroupBlock.Visibility = Visibility.Collapsed;
                ComboBoxStudent.Visibility = Visibility.Collapsed;
                TypeFineText.Visibility = Visibility.Collapsed;
                FineTypeComboBox.Visibility = Visibility.Collapsed;
                MarkFineText.Visibility = Visibility.Collapsed;
                FineMarkComboBox.Visibility = Visibility.Collapsed;
                EventNameText.Visibility = Visibility.Collapsed;
                NameEventSearch.Visibility = Visibility.Collapsed;
                EventTypeText.Visibility = Visibility.Collapsed;
                EventType.Visibility = Visibility.Collapsed;
                EventDatePicker.Visibility = Visibility.Collapsed;
                DataEvent.Visibility = Visibility.Collapsed;
                MarkEvent.Visibility = Visibility.Collapsed;
                EventMark.Visibility = Visibility.Collapsed;

            }
            

            if (Properties.Settings.Default.userRole == 4)
            {
                EventNameText.Visibility = Visibility.Visible;
                NameEventSearch.Visibility = Visibility.Visible;
                EventTypeText.Visibility = Visibility.Visible;
                EventType.Visibility = Visibility.Visible;
                EventDatePicker.Visibility = Visibility.Visible;
                DataEvent.Visibility = Visibility.Visible;
                MarkEvent.Visibility = Visibility.Visible;
                EventMark.Visibility = Visibility.Visible;

            }


            if (Properties.Settings.Default.userRole == 3)
            {
                TypeFineText.Visibility = Visibility.Visible;
                FineTypeComboBox.Visibility = Visibility.Visible;
                MarkFineText.Visibility = Visibility.Visible;
                FineMarkComboBox.Visibility = Visibility.Visible;

            }
        }
    }
}
