using SuccessPlus.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace SuccessPlus.View
{
    public partial class Journal : Page
    {
        private Core db = new Core();
        private int IdStudent;
        public Journal()
        {
            InitializeComponent();
            LoadGroups();
            LoadSubjects();
        }

        private void LoadGroups()
        {
            try
            {
                var groups = db.context.Group.ToList();
                GroupComboBox.ItemsSource = groups;
                GroupComboBox.DisplayMemberPath = "NameGroup";
                GroupComboBox.SelectedValuePath = "IdGroup";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке групп: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadSubjects()
        {
            try
            {
                //программное создание колонок с предметами
                var subjects = db.context.Subjects.ToList();
                foreach (var subject in subjects)
                {
                    JournalDataGrid.Columns.Add(new DataGridTextColumn
                    {
                        Header = subject.Name,
                        Binding = new System.Windows.Data.Binding($"Subjects[{subject.IdSubjects}]")
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке предметов: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadStudentsAndMarks();
        }

        private void ApplyDateFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadStudentsAndMarks();
        }

        private void LoadStudentsAndMarks()
        {
            if (GroupComboBox.SelectedValue == null || !StartDatePicker.SelectedDate.HasValue || !EndDatePicker.SelectedDate.HasValue)
                return;

            int groupId = (int)GroupComboBox.SelectedValue;
            DateTime startDate = StartDatePicker.SelectedDate.Value;
            DateTime endDate = EndDatePicker.SelectedDate.Value;

            try
            {
                var students = db.context.Student
                    .Where(s => s.GroupId == groupId)
                    .Select(s => new
                    {
                        s.IdStudent,
                        Name = s.LastName + " " + s.FisrtName
                    })
                    .ToList();

                var marks = db.context.MarkStudent
                    .Where(m => m.Student.GroupId == groupId && m.Date >= startDate && m.Date <= endDate)
                    .GroupBy(m => new { m.IdStudent, m.IdSubject })
                    .Select(g => new
                    {
                        g.Key.IdStudent,
                        g.Key.IdSubject,
                        AverageMark = g.Average(m => m.IdMark)
                    })
                    .ToList();

                var studentMarks = students.Select(s => new
                {
                    s.Name,
                    Subjects = marks.Where(m => m.IdStudent == s.IdStudent).ToDictionary(m => m.IdSubject, m => m.AverageMark)
                }).ToList();

                JournalDataGrid.ItemsSource = studentMarks;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке студентов и оценок: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddMark addMark = new AddMark(IdStudent);
            addMark.ShowDialog();
        }
    }
}
