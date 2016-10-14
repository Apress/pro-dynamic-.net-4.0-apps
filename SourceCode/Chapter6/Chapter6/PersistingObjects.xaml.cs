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

namespace Chapter6
{
    /// <summary>
    /// Interaction logic for PersistingObjects.xaml
    /// </summary>
    public partial class PersistingObjects : Window
    {
        public PersistingObjects()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dog oDog = new Dog();
            oDog.Breed = "Pit Bull";
            oDog.Name = "Elke";

            string szXAML = XamlWriter.Save(oDog);

        }
    }

    public class Dog
    {
        public string Name { get; set; }
        public string Breed { get; set; }

        public string Bark()
        {
            return "Woof, woof!";
        }
    }

}
