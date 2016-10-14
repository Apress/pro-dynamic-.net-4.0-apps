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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void cmdSimpleWPFWindow_Click(object sender, RoutedEventArgs e)
        {
            SimpleWPFWindow oSimpleWPFWindow = new SimpleWPFWindow();
            oSimpleWPFWindow.Show();
        }

        private void cmdStackPanelDemo_Click(object sender, RoutedEventArgs e)
        {
            StackPanelDemo oStackPanelDemo = new StackPanelDemo();
            oStackPanelDemo.Show();
        }

        private void cmdWrapPanelDemo_Click(object sender, RoutedEventArgs e)
        {
            WrapPanelDemo oWrapPanelDemo = new WrapPanelDemo();
            oWrapPanelDemo.Show();
        }

        private void cmdDockPanelDemo_Click(object sender, RoutedEventArgs e)
        {
            DockPanelDemo oDockPanelDemo = new DockPanelDemo();
            oDockPanelDemo.Show();
        }

        private void cmdCanvasDemo_Click(object sender, RoutedEventArgs e)
        {
            CanvasDemo oCanvasDemo = new CanvasDemo();
            oCanvasDemo.Show();
        }

        private void cmdXAMLForCanvas_Click(object sender, RoutedEventArgs e)
        {
            XAMLForCanvas oXAMLForCanvas = new XAMLForCanvas();
            oXAMLForCanvas.Show();
        }

        private void cmdGridDemo_Click(object sender, RoutedEventArgs e)
        {
            GridDemo oGridDemo = new GridDemo();
            oGridDemo.Show();
        }

        private void cmdDynamicWPF_Click(object sender, RoutedEventArgs e)
        {
            DynamicWPF oDynamicWPF = new DynamicWPF();
            oDynamicWPF.Show();
        }

        private void cmdDynamicGrid_Click(object sender, RoutedEventArgs e)
        {
            DynamicGrid oDynamicGrid = new DynamicGrid();
            oDynamicGrid.Show();
        }

        private void cmdNestedControls_Click(object sender, RoutedEventArgs e)
        {
            NestedControls oNestedControls = new NestedControls();
            oNestedControls.Show();
        }

        private void cmdXAMLReader_Click(object sender, RoutedEventArgs e)
        {
            XAMLReader oXAMLReader = new XAMLReader();
            oXAMLReader.Show();
        }

        private void cmdPersistingObjects_Click(object sender, RoutedEventArgs e)
        {
            PersistingObjects oPersistingObjects = new PersistingObjects();
            oPersistingObjects.Show();
        }

        private void cmdAncestorDescendant_Click(object sender, RoutedEventArgs e)
        {
            AncestorDescendant oAncestorDescendant = new AncestorDescendant();
            oAncestorDescendant.Show();
        }

        private void cmdWiringEvents_Click(object sender, RoutedEventArgs e)
        {
            WiringEvents oWiringEvents = new WiringEvents();
            oWiringEvents.Show();
        }

        private void cmdMenuDemo_Click(object sender, RoutedEventArgs e)
        {
            MenuDemo oMenuDemo = new MenuDemo();
            oMenuDemo.Show();
        }

        private void cmdDynamicMenu_Click(object sender, RoutedEventArgs e)
        {
            DynamicMenu oDynamicMenu = new DynamicMenu();
            oDynamicMenu.Show();
        }
    }
}
