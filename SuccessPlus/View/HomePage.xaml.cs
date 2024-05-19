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
    /// Логика взаимодействия для HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        
        private Core _db = new Core();

        public HomePage()
        {
            InitializeComponent();
        }

        private void HomePage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEventsToCalendar();
        }


        private void LoadEventsToCalendar()
        {
            var events = _db.context.Event.ToList();

            foreach (var ev in events)
            {
                if (ev.Date.HasValue) // Проверяем, что дата события не равна null
                {
                    DateTime date = ev.Date.Value; // Преобразуем Nullable<DateTime> в DateTime
                    EventCalendar.BlackoutDates.Add(new CalendarDateRange(date));
                }
            }
        }
    }
}
