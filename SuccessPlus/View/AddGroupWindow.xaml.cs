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
    /// Логика взаимодействия для AddGroupWindow.xaml
    /// </summary>
    public partial class AddGroupWindow : Window
    {
        public Core db = new Core();
        public AddGroupWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Group newgroup = new Group
            {
                NameGroup = GroupName.Text
            };
            try
            {
                db.context.Group.Add(newgroup);
                db.context.SaveChanges();
                if (db.context.SaveChanges() == 0)
                    MessageBox.Show("Успешное доьбавление");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            


        }
    }
}
