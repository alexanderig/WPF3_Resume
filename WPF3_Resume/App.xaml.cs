using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF3_Resume
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Onstart(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
            VMclass vmobj = new VMclass();
            window.DataContext = vmobj;
            window.Show();
        }
    }
}
