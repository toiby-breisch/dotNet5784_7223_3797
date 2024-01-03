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
using System.Windows.Shapes;

namespace pl.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public int ID
        {//לשאול
            get { return (int)GetValue(IDProperty); }
            set { SetValue(IDProperty, value); }
        }

        public BO.Engineer CurrentEngineer
        {

            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EngineersValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentEngineerProperty =
            DependencyProperty.Register("EngineersValue", typeof(BO.Engineer), typeof(BO.Engineer), new PropertyMetadata(0));
        //לשאול
        public static readonly DependencyProperty IDProperty =
          DependencyProperty.Register("EngineersValue", typeof(int), typeof(int), new PropertyMetadata(0));


        public EngineerWindow()
        {
            InitializeComponent();

        }
    }
}
