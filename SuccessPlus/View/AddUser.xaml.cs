using SuccessPlus.Model;
using System;
using System.Linq;
using System.Windows;

namespace SuccessPlus.View
{
    public partial class AddUser : Window
    {
        private Core db = new Core();

        public AddUser()
        {
            InitializeComponent();
            LoadAccountTypes();
            LoadGroups();
        }

        private void LoadAccountTypes()
        {
            try
            {
                var accountTypes = db.context.Type.ToList();
                TypeUserComboBox.ItemsSource = accountTypes;
                TypeUserComboBox.SelectedValuePath = "IdType";
                TypeUserComboBox.DisplayMemberPath = "NameAcc";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке типов учетных записей: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadGroups()
        {
            try
            {
                var groups = db.context.Group.ToList();
                GroupComboBox.ItemsSource = groups;
                GroupComboBox.SelectedValuePath = "IdGroup";
                GroupComboBox.DisplayMemberPath = "NameGroup";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при загрузке групп: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void TypeUserComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedType = TypeUserComboBox.SelectedValue;
            if (selectedType != null)
            {
                int typeId = (int)selectedType;
                // если выбранный тип равен 4, то появляется комбобокс с выбором группы
                GroupComboBoxContainer.Visibility = typeId == 4 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LastName.Text) || string.IsNullOrWhiteSpace(FirstName.Text) || string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(Password.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var selectedType = TypeUserComboBox.SelectedValue;

            if (selectedType == null)
            {
                MessageBox.Show("Пожалуйста, выберите тип учетной записи", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                int typeId = (int)selectedType;

                var newUser = new User
                {
                    LastName = LastName.Text.Trim(),
                    FirstName = FirstName.Text.Trim(),
                    Type = typeId
                };

                if (typeId == 4) 
                {
                    if (GroupComboBox.SelectedValue == null)
                    {
                        MessageBox.Show("Пожалуйста, выберите группу для старосты", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    newUser.GroupId = (int)GroupComboBox.SelectedValue;
                }

                db.context.User.Add(newUser);
                db.context.SaveChanges();

                var newSignIn = new SignIn
                {
                    Login = Login.Text.Trim(),
                    Password = Password.Text,
                    IdUser = newUser.IdUser 
                };

                db.context.SignIn.Add(newSignIn);
                db.context.SaveChanges();

                MessageBox.Show("Пользователь был добавлен", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при добавлении пользователя: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
