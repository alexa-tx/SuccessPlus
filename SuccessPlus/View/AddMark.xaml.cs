using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SuccessPlus.Model;

namespace SuccessPlus.View
{
    /// <summary>
    /// Логика взаимодействия для AddMark.xaml
    /// </summary>
    public partial class AddMark : Window
    {
        private Core _db = new Core();
        private int _studentId;
        public List<Subjects> Subject;
        public List<Marks> Mark;
        public List<Student> students;

        public AddMark(int IdStudent)
        {
            InitializeComponent();
            _studentId = IdStudent;
            Subject = _db.context.Subjects.ToList();
            SubjectComboBox.ItemsSource = Subject;
            SubjectComboBox.DisplayMemberPath = "Name";
            SubjectComboBox.SelectedValuePath = "IdSubjects";
            Mark = _db.context.Marks.ToList();
            MarkComboBox.ItemsSource = Mark;
            MarkComboBox.DisplayMemberPath = "MarkName";
            MarkComboBox.SelectedValuePath = "IdMark";
            students = _db.context.Student.ToList();
            var studentNames = students.Select(s => $"{s.LastName} {s.FisrtName}").ToList();
            StudentComboBox.ItemsSource = studentNames;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (StudentComboBox.SelectedItem == null || SubjectComboBox.SelectedItem == null || MarkComboBox.SelectedItem == null || !MarkDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Пожалуйста, выберите студента, предмет, оценку и дату.");
                return;
            }

            var selectedStudentName = (string)StudentComboBox.SelectedItem;
            var selectedStudent = students.FirstOrDefault(s => $"{s.LastName} {s.FisrtName}" == selectedStudentName);

            if (selectedStudent == null)
            {
                MessageBox.Show("Не удалось найти выбранного студента.");
                return;
            }

            int studentId = selectedStudent.IdStudent;
            int subjectId = (int)SubjectComboBox.SelectedValue;
            int markId = (int)MarkComboBox.SelectedValue;
            DateTime selectedDate = MarkDatePicker.SelectedDate.Value;

            MarkStudent markStudent = new MarkStudent
            {
                IdStudent = studentId,
                IdSubject = subjectId,
                IdMark = markId,
                Date = selectedDate
            };

            _db.context.MarkStudent.Add(markStudent);
            try
            {
                _db.context.SaveChanges();
                MessageBox.Show("Оценка успешно добавлена.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
    }
