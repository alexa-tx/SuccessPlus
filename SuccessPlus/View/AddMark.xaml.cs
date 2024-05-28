using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SuccessPlus.Model;
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
    /// Логика взаимодействия для AddMark.xaml
    /// </summary>
    public partial class AddMark : Window
    {
        private Core _db = new Core();
        private int _studentId;
        public List<Subjects> Subject;
        public List<Marks> Mark;
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
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SubjectComboBox.SelectedItem == null || MarkComboBox.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите предмет и оценку.");
                return;
            }

            int subjectId = (int)SubjectComboBox.SelectedValue;
            int markId = (int)MarkComboBox.SelectedValue;

            MarkStudent markStudent = new MarkStudent
            {
                IdStudent = _studentId,
                IdSubject = subjectId,
                IdMark = markId,
                Date = DateTime.Now
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
