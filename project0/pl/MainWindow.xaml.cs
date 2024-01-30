﻿using PL.Engineer;
using System.Windows;
namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    /// <summary>
    /// Initialize the mainWindow
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
       
    }
    /// <summary>
    ///  /// <summary>
    /// The function is for btn Engineers_Click
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }
    /// <summary>
    ///  /// <summary>
    /// The function is for btn DalTestInitialization_click
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnDalTestInitialization_click(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show("Do you want to create new data?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            DalApi.IDal dal = DalApi.Factory.Get;
            DalTest.Initialization.Do(dal);
        }
    }
}
