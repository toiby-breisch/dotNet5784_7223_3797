using pl.Engineer;
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
using System.Xml.Linq;

namespace pl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;
        public MainWindow()
        {
            InitializeComponent();
           
        }
        private void btnEngineers_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }
        private void btnDalTestInitialization_click(object sender, RoutedEventArgs e)
        {
           //זה הכוונה?
            MessageBox.Show("To Initialization?" );
            DalTest.Initialization.Do(_dal);
        }
    }
}
