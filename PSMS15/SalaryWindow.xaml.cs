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

namespace PSMS15
{
    /// <summary>
    /// SalaryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SalaryWindow : Window
    {
        public SalaryWindow()
        {
            InitializeComponent();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
        }
        private void CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
        }

    }
}
