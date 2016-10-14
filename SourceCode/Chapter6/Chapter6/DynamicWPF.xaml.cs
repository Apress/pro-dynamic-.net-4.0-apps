using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chapter6
{
    /// <summary>
    /// Interaction logic for DynamicWPF.xaml
    /// </summary>
    public partial class DynamicWPF : Window
    {
        public DynamicWPF()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox oTextBox = new TextBox();
            oTextBox.Name = "txtFirstName";
            oTextBox.Width = 100;
            oTextBox.Height = 24;
            oTextBox.Text = "Hello";
            oTextBox.Margin = new Thickness(6, 6, 0, 0);
            oTextBox.VerticalAlignment = VerticalAlignment.Top;

            this.Grid1.Children.Add(oTextBox);

        }
    }
}
