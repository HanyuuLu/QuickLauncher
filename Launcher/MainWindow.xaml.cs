using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;

namespace Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Control ControlObject;

        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = Control.Instance;
            InitializeComponent();
        }


        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputBox.Text == "exit")
            { Application.Current.Shutdown(); }
            Control.Instance.search(InputBox.Text);
            updateList();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
             if(e.Key==Key.Escape)
            {
                Application.Current.Shutdown();
            }
            else if(e.Key==Key.Enter)
            {
                MessageBox.Show("search!");
            }
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListBox.Items.Clear();
            foreach(var (key,value) in Control.Instance.LaunchList)
            {
                ListBox.Items.Add(value);
            }
        }
        public void updateList()
        {
            if(ListBox==null)
            {
                return;
            }
            ListBox.Items.Clear();
            foreach (var (key, value) in Control.Instance.SearchList)
            {
                ListBox.Items.Add(value);
            }
            
        }
    }
}
