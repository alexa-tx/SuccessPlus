﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new SingInPage());
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
                
                if (Properties.Settings.Default.userId == 0)
                    TopBarMenu.Visibility = Visibility.Collapsed;
                else
                    TopBarMenu.Visibility = Visibility.Visible;
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Properties.Settings.Default.userId = 0;
            Properties.Settings.Default.userRole = 0;
            Close();
        }

        private void TextBlock_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new StudentsGroup());
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
        }
        //кнопка студенты
        private void TextBlock_PreviewMouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

                        MainFrame.Navigate(new Students());
        }
        //рейтлисты
        private void TextBlock_PreviewMouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            //MainFrame.Navigate(new HomePage());
        }
        //отчеты
        private void TextBlock_PreviewMouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            //MainFrame.Navigate(new HomePage());
        }
    }
}
