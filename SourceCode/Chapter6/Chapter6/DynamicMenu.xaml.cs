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
using System.Windows.Markup;
using System.IO;
using System.Xml;

namespace Chapter6
{
    /// <summary>
    /// Interaction logic for DynamicMenu.xaml
    /// </summary>
    public partial class DynamicMenu : Window
    {
        public DynamicMenu()
        {
            InitializeComponent();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem oMenuItem = ((MenuItem)sender);

            MessageBox.Show(oMenuItem.Header.ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (FileStream oFileStream = new FileStream("Menu.xaml", FileMode.Open))
            {
                Menu oMenu = XamlReader.Load(oFileStream) as Menu;

                if (oMenu != null)
                {
                    this.Grid1.Children.Insert(0, oMenu);

                    WireMenus(oMenu.Items);
                }

            }

        }

        private void WireMenus(ItemCollection oItemCollection)
        {
            foreach (MenuItem oMenuItem in oItemCollection)
            {
                if (oMenuItem.HasItems)
                    WireMenus(oMenuItem.Items);
                else
                    oMenuItem.Click += new RoutedEventHandler(Menu_Click);
            }
        }
    }
}
