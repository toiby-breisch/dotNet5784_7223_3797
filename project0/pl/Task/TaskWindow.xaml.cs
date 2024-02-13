using BO;
using System;
using System.Collections.Generic;
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
public partial class TaskWindow : Window
{
    private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.Task CurrentTask
    {
        get { return (BO.Task)GetValue(CurrentTaskProperty); }
        set { SetValue(CurrentTaskProperty, value); }
    }

    // Using a DependencyProperty as the backing store for TasksValue.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty CurrentTaskProperty =
        DependencyProperty.Register("CurrentTask", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));


    public static bool inputIntegrityCheck(BO.Task? task)
    {
        if (task!.Description =="" || task.Alias == "" )
        {
            MessageBox.Show("ERROR: '\n'The data you entered is incorrect.");
            return false;
        }
        return true;
    }
    /// <summary>
    ///  Initialize Engineer window
    /// </summary>
    /// <param name="Id"></param>
    public TaskWindow(int Id = 0)
    {
        InitializeComponent();
        if (Id == 0)
        {
            CurrentTask = new BO.Task 
            { 
                Id = 0,
                Description = "",
                Alias = "",
                CreatedAtDate = DateTime.Now,
                status= BO.Status.Unscheduled,
                DependenciesList=null,
                Milestone=null,
                StartDate=null,
                ScheduledDate=null,
                ForecastDate=null,
                DeadlineDate=null,
                CompleteDate=null,
                Remarks=null,
                Deliverables=null,
                Engineer=new EngineerInTask(Id=0,Name=null!),
                CopmlexityLevel = BO.EngineerExperience.None
            };
        }
        else
        {
            try
            {
                CurrentTask = s_bl.Task.Read(Id)!;
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
                if (inputIntegrityCheck(CurrentTask))
                {
                    s_bl.Task.create(CurrentTask);
                    MessageBox.Show("Object with id " + CurrentTask.Id + "had added successfully!");
                    this.Close();
                }
            }
            else
            {
                if (inputIntegrityCheck(CurrentTask))
                {
                    s_bl.Task.Update(CurrentTask);
                    MessageBox.Show("Object with id " + CurrentTask.Id + "had updated successfully!");
                    this.Close();
                }

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