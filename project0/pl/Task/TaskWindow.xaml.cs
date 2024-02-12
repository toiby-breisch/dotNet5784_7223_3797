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

namespace PL.Task;

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

    /// <summary>
    ///  Initialize Engineer window
    /// </summary>
    /// <param name="Id"></param>
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
            catch (BO.BlDoesNotExistException message)
            { MessageBox.Show(message.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
    /// <summary>
    /// The function is for btn Add or Update_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {
        string content = (sender as Button)!.Content.ToString()!;
        try
        {
            if (content == "Add")
            {
                var t = s_bl.Engineer.create(CurrentEngineer);
            }
            else
            {
                s_bl.Engineer.Update(CurrentEngineer);
            }
        }
        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        catch (BO.BlNullOrNotIllegalPropertyException
        ex)
        { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        MessageBox.Show("the transaction completed successfully");
        Close();

    }

}