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

namespace SWAM.Controls.Windows
{
    /// <summary>
    /// Interaction logic for DeleteProductWindow.xaml
    /// </summary>
    public partial class DeleteProductWindow : Window
    {
        public DeleteProductWindow()
        {
            InitializeComponent();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
