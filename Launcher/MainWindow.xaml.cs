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
            ControlObject = new Control();
            MessageBox.Show(ControlObject.test());

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }


        private void InputBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (InputBox.Text == "exit")
            { Application.Current.Shutdown(); }
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
    }
}
