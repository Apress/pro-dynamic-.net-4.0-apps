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
    /// Interaction logic for DynamicGrid.xaml
    /// </summary>
    public partial class DynamicGrid : Window
    {
        public DynamicGrid()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DynamicGridDemo();
        }

        private void DynamicGridDemo()
        {
            ColumnDefinition oColumnDefinition;
            RowDefinition oRowDefinition;
            Grid oGrid;
            Label oLabel;
            TextBox oTextBox;
            ComboBox oComboBox;
            Button oButton;

            //Create two columns and add the to the grid
            oColumnDefinition = new ColumnDefinition();
            Grid1.ColumnDefinitions.Add(oColumnDefinition);

            oColumnDefinition = new ColumnDefinition();
            Grid1.ColumnDefinitions.Add(oColumnDefinition);

            //Create a row and add it to then grid
            oRowDefinition = new RowDefinition();
            oRowDefinition.Height = GridLength.Auto;
            Grid1.RowDefinitions.Add(oRowDefinition);

            //... then add the controls for that row
            oLabel = new Label();
            oLabel.Content = "Last Name:";
            Grid.SetRow(oLabel, 0);
            Grid.SetColumn(oLabel, 0);
            Grid1.Children.Add(oLabel);

            oTextBox = new TextBox();
            oTextBox.Name = "txtLastName";
            oTextBox.Width = 100;
            Grid.SetRow(oTextBox, 0);
            Grid.SetColumn(oTextBox, 1);
            Grid1.Children.Add(oTextBox);

            //Create another row
            oRowDefinition = new RowDefinition();
            oRowDefinition.Height = GridLength.Auto;
            Grid1.RowDefinitions.Add(oRowDefinition);

            // ...and add controls here as well
            oLabel = new Label();
            oLabel.Content = "Salutation:";
            Grid.SetRow(oLabel, 1);
            Grid.SetColumn(oLabel, 0);
            Grid1.Children.Add(oLabel);

            oComboBox = new ComboBox();
            oComboBox.Name = "cmbSalutation";
            oComboBox.Width = 100;
            oComboBox.Items.Add("Mr.");
            oComboBox.Items.Add("Mrs.");
            oComboBox.Items.Add("Dr.");
            Grid.SetRow(oComboBox, 1);
            Grid.SetColumn(oComboBox, 1);
            Grid1.Children.Add(oComboBox);

            //Add one more row
            oRowDefinition = new RowDefinition();
            oRowDefinition.Height = GridLength.Auto;
            Grid1.RowDefinitions.Add(oRowDefinition);

            //...and add a Button contron wired to an event handler
            oButton = new Button();
            oButton.Width = 100;
            oButton.Content = "Get Data";
            oButton.Click += new RoutedEventHandler(this.cmdGetData_Click);
            Grid.SetRow(oButton, 2);
            Grid.SetColumn(oButton, 1);
            Grid1.Children.Add(oButton);

            //Add this grid to the owner window
            this.Content = Grid1;

        }

        private void cmdGetData_Click(object sender, RoutedEventArgs e)
        {
            //TextBox oTextBox = null;
            //ComboBox oComboBox = null;
            Grid oGrid;
            string szData;

            oGrid = ((Grid)this.Content);

            foreach (UIElement oElement in oGrid.Children)
            {
                switch (oElement.GetType().Name)
                {
                    case "TextBox":
                        szData = ((TextBox)oElement).Text;
                        break;

                    case "ComboBox":
                        szData = ((ComboBox)oElement).Text;
                        break;
                }
            }

        }
    }
}
