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
using System.IO;
using System.Windows.Markup;

namespace Chapter6
{
    /// <summary>
    /// Interaction logic for XAMLReader.xaml
    /// </summary>
    public partial class XAMLReader : Window
    {
        public XAMLReader()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string szXAML = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"\griddemo.xaml");

            this.Content = XamlReader.Parse(szXAML);

        }
    }
}
