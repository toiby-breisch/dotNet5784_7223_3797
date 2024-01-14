using BO;
using System;

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
        DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));


    public EngineerWindow(int Id = 0)
    {
        InitializeComponent();
        if (Id == 0)
        {
            CurrentEngineer = new BO.Engineer { Id = 0, Name = "", Email = "", Cost = 0, Level = BO.EngineerExperience.None };
        }
        else
        {
            try
            {
                CurrentEngineer = s_bl.Engineer.Read(Id)!;
            }
            ////////לשאול!!!!!!!!!!!!!!!!!!!!!!dont forget!!!!!!!!!!!!please!!!!!!forever!to smile;)
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        //Console.Write(CurrentEngineer.Name);
        string content = (sender as Button)!.Content.ToString()!;
        try
        {
            if (content == "Add")
            {
               var t= s_bl.Engineer.create(CurrentEngineer);
            }
            else
            {
                s_bl.Engineer.Update(CurrentEngineer);
            }

        }
        ////////לשאול
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MessageBox.Show("Succeeded!");
        this.Close();
    }
}