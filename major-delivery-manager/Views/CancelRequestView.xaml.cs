﻿using System;
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

namespace major_delivery_manager.Views
{
    /// <summary>
    /// Логика взаимодействия для CancelRequestView.xaml
    /// </summary>
    public partial class CancelRequestView : Window
    {
        public CancelRequestView()
        {
            InitializeComponent();
        }

        private void OnClosing_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
