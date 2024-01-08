using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerWindow.xaml
/// </summary>
public partial class EngineerWindow : Window
{
    private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.Engineer CurrentEngineer
    {
        get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
        set { SetValue(CurrentEngineerProperty, value); }
    }

    // Using a DependencyProperty as the backing store for EngineersValue.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentEngineerProperty =
        DependencyProperty.Register("EngineersValue", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(0));


    public EngineerWindow(int Id = 0)
    {
        InitializeComponent();
        if (Id == 0)
        {
            BO.Engineer CurrentEngineer = new BO.Engineer { Id = 0, Name = "", Email = "", Cost = 0, Level = BO.EngineerExperience.None };
        }
        else
        {
            try
            {
                BO.Engineer CurrentEngineer = s_bl.Engineer.Read(Id)!;
            }
            ////////לשאול!!!!!!!!!!!!!!!!!!!!!!dont forget!!!!!!!!!!!!please!!!!!!forever!to smile;)
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    //{

    //}
    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            BO.Engineer newEngineer = new BO.Engineer
            {
                Id = CurrentEngineer.Id,
                Name = CurrentEngineer.Name,
                Email = CurrentEngineer.Email,
                Cost = CurrentEngineer.Cost,
                Level = CurrentEngineer.Level,
                CurrentTask = CurrentEngineer.CurrentTask
            };
            //if()
            s_bl.Engineer.Update(newEngineer);
            s_bl.Engineer.create(newEngineer);
        }
        ////////לשאול
         catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Close();
    }
}