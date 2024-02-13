using BO;
using DO;
using System;
using System.Text.RegularExpressions;
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
    public static bool IsValidEmailAddress(string? s)
    {
        Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        return regex.IsMatch(s!);
    }

    public static bool inputIntegrityCheck(BO.Engineer? engineer)
    {
        if (engineer?.Id <= 0 || engineer!.Name == "" || engineer.Cost <= 0 || !IsValidEmailAddress(engineer.Email))
        {
            MessageBox.Show("ERROR: '\n'The data you entered is incorrect.");
            return false;
        }
        return true;
    }

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
                    if (inputIntegrityCheck(CurrentEngineer))
                    {
                        s_bl.Engineer.create(CurrentEngineer!);
                        MessageBox.Show("Object with id " + CurrentEngineer.Id + "had added successfully!");
                        this.Close();
                    }
                }
                else
                {
                        if (inputIntegrityCheck(CurrentEngineer))
                        {
                            s_bl.Engineer.Update(CurrentEngineer!);
                            MessageBox.Show("Object with id " + CurrentEngineer.Id + "had updated successfully!");
                            this.Close();
                        }
                       
                }
            
         
         
        }
        catch (BO.BlAlreadyExistsException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        catch (BO.BlDoesNotExistException ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        catch (BO.BlNullOrNotIllegalPropertyException
        ex) { MessageBox.Show(ex.Message, "error Window", MessageBoxButton.OK, MessageBoxImage.Error); Close(); return; }
        Close();

    }

}