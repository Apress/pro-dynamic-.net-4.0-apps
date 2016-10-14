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
    /// Interaction logic for NestedControls.xaml
    /// </summary>
    public partial class NestedControls : Window
    {
        string szData = string.Empty;

        public NestedControls()
        {
            InitializeComponent();
        }

        private void IterateControls(UIElementCollection oUIElementCollection)
        {
            Grid oGrid = null;
            GroupBox oGroupBox = null;
            StackPanel oStackPanel = null;

            foreach (UIElement oElement in oUIElementCollection)
            {
                switch (oElement.GetType().Name)
                {
                    case "TextBox":
                        szData += ((TextBox)oElement).Text + "\n";
                        break;

                    case "ComboBox":
                        szData += ((ComboBox)oElement).Text + "\n";
                        break;

                    case "CheckBox":
                        szData += ((CheckBox)oElement).Content.ToString() + "\n";
                        break;

                    case "Button":
                        szData += ((Button)oElement).Content.ToString() + "\n";
                        break;

                    case "GroupBox":
                        oGroupBox = ((GroupBox)oElement);
                        szData += oGroupBox.Header + "\n";
                        IterateControls(GetUIElementCollection(oGroupBox.Content));
                        break;

                    case "Grid":
                        oGrid = ((Grid)oElement);
                        IterateControls(oGrid.Children);
                        break;

                    case "StackPanel":
                        oStackPanel = ((StackPanel)oElement);
                        IterateControls(oStackPanel.Children);
                        break;
                }

            }

        }

        private UIElementCollection GetUIElementCollection(object oContent)
        {
            UIElementCollection oUIElementCollection = null;

            switch (oContent.GetType().Name)
            {
                case "Grid":
                    oUIElementCollection = ((Grid)oContent).Children;
                    break;

                case "StackPanel":
                    oUIElementCollection = ((StackPanel)oContent).Children;
                    break;

                case "DockPanel":
                    oUIElementCollection = ((DockPanel)oContent).Children;
                    break;

                case "Canvas":
                    oUIElementCollection = ((Canvas)oContent).Children;
                    break;
            }

            return oUIElementCollection;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void cmdGetData_Click(object sender, RoutedEventArgs e)
        {
            IterateControls(GetUIElementCollection(this.Content));

            MessageBox.Show(szData); 
        }

    }
}
