﻿using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Task;

/// <summary>
/// Interaction logic for TaskListWindow.xaml
/// </summary>
/// 
public partial class TaskListWindow : Window
{
    public BO.EngineerExperience TaskFilter { get; set; } = BO.EngineerExperience.None;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public ObservableCollection<BO.Task> TaskList
    {
        get { return (ObservableCollection<BO.Task>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));
    /// <summary>
    /// Initialize TaskListWindow
    /// </summary>
    public TaskListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.Task.ReadAll();
        TaskList = temp == null ? new() : new(temp);
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var temp = TaskFilter == BO.EngineerExperience.None ?
        s_bl?.Task.ReadAll() :
            s_bl?.Task.ReadAll(item => item!.CopmlexityLevel == TaskFilter);
        TaskList = temp == null ? new() : new(temp);

    }
    /// <summary>
    /// The function is for btn Add or Update_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnAddOrUpdate_Click(object sender, RoutedEventArgs e)
    {
        TaskWindow win = new TaskWindow();
        win.ShowDialog();
        var temp = s_bl?.Task.ReadAll();
        TaskList = new(temp!);

    }
    /// <summary>
    /// The function updates the Task by clicking the button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateThisObject(object sender, MouseButtonEventArgs e)
    {
        BO.Task? TaskInList = (sender as ListView)?.SelectedItem as BO.Task;
        new TaskWindow(TaskInList!.Id).ShowDialog();
        var temp = s_bl?.Task.ReadAll();
        TaskList = new(temp!);

    }

}
