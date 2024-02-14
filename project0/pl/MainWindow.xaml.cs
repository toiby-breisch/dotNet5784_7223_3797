using PL.Task;
using System.Windows;
using PL.Engineer;
namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly DalApi.IDal _dal = DalApi.Factory.Get;
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
    private void BtnEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }
    private void BtnTask_Click(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }
    /// <summary>
    ///  /// <summary>
    /// The function is for btn DalTestInitialization_click
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnDalTestInitialization_click(object sender, RoutedEventArgs e)
    {
        if (MessageBox.Show("Do you want to create new data?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            DalApi.IDal dal = DalApi.Factory.Get;
            DalTest.Initialization.Do(dal);
        }
    }
}
