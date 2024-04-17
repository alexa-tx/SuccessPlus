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
    /// Логика взаимодействия для EditGroupWindow.xaml
    /// </summary>
    public partial class EditGroupWindow : Window
    {
        Core db = new Core();
        Group selectedGroup;
        public EditGroupWindow(int idGroup)
        {
            InitializeComponent();
            selectedGroup = db.context.Group.Where(x=> x.IdGroup == idGroup).FirstOrDefault();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selectedGroup.NameGroup = GroupNameTextBox.Text;
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
    }
}
