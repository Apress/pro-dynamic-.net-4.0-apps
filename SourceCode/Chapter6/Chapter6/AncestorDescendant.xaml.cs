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
    /// Interaction logic for AncestorDescendant.xaml
    /// </summary>
    public partial class AncestorDescendant : Window
    {
        public AncestorDescendant()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            bool bIsAncestorOf;
            bool bIsDescendantOf;

            bIsAncestorOf = grpMyData.IsAncestorOf(chkCheckMe);  //returns true
            bIsAncestorOf = grpMyData.IsAncestorOf(cmdGetData);  //returns false
            bIsAncestorOf = cmdSave.IsAncestorOf(cmdCancel);  //returns false
            bIsAncestorOf = stackPanel1.IsAncestorOf(cmdCancel);  //returns true

            bIsDescendantOf = chkCheckMe.IsDescendantOf(grpMyData);  //returns true
            bIsDescendantOf = cmdSave.IsDescendantOf(stackPanel1);  //returns true
            bIsDescendantOf = cmdSave.IsDescendantOf(chkCheckMe);  //returns false

        }
    }
}
