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
        //private Control ControlObject;

        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            DataContext = Control.Instance;
            SearchResultList.ItemsSource = Control.Instance.SearchList;
        }


        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var task = Task.Run(() =>
            {
                Control.Instance.search();
            }
            );
            SearchResultList.ItemsSource = Control.Instance.SearchList;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Application.Current.Shutdown();
                    break;
                case Key.Enter:
                    MessageBox.Show("Launch");
                    break;
                case Key.Up:
                case Key.Down:
                    break;

            }
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() => Control.Instance.search());
        }
        //public void updateList()
        //{
        //    if (ListBox == null)
        //    {return;}
        //    ListBox.Items.Clear();
        //    foreach (var (key, value) in Control.Instance.SearchDict)
        //    {ListBox.Items.Add(value);}
        //}
    }
}
