using PL.Task;
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

namespace PL.Engineer;

/// <summary>
/// Interaction logic for EngineerListWindow.xaml
/// </summary>
/// 
public partial class EngineerListWindow : Window
{
    public BO.EngineerExperience EngineerFilter { get; set; } = BO.EngineerExperience.None;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public ObservableCollection<BO.EngineerInList> EngineerList
    {
        get { return (ObservableCollection<BO.EngineerInList>)GetValue(EngineerInListProperty); }
        set { SetValue(EngineerInListProperty, value); }
    }

    public static readonly DependencyProperty EngineerInListProperty =
        DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.EngineerInList>), typeof(EngineerListWindow), new PropertyMetadata(null));
    /// <summary>
    /// Initialize EngineerListWindow
    /// </summary>
    public EngineerListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.EngineerInList.ReadAll().OrderBy(engineer => engineer.Id);
        EngineerList = temp == null ? new() : new(temp);
    }
    /// <summary>
    /// combox selection change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var temp = EngineerFilter == BO.EngineerExperience.None?
        s_bl?.EngineerInList.ReadAll().OrderBy(engineer => engineer.Id) :
            s_bl?.EngineerInList.ReadAll(item => item!.Level == EngineerFilter).OrderBy(engineer => engineer.Id);
            EngineerList = temp == null ? new() : new(temp);
    }
    /// <summary>
    /// The function is for btn Add or Update_Click
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BtnAddOrUpdate_Click(object sender, RoutedEventArgs e)
    {
        EngineerWindow win = new();
        win.ShowDialog();
        var temp = s_bl?.EngineerInList.ReadAll();
        EngineerList = new(temp!);
    }
    /// <summary>
    /// The function updates the engineer by clicking the button.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void UpdateThisObject(object sender, MouseButtonEventArgs e)
    {
        BO.EngineerInList? engineerInList = (sender as ListView)?.SelectedItem as BO.EngineerInList;
        new EngineerWindow(engineerInList!.Id).ShowDialog();
        var temp =EngineerFilter == BO.EngineerExperience.None ?
        s_bl?.EngineerInList.ReadAll().OrderBy(engineer=>engineer.Id) :
            s_bl?.EngineerInList.ReadAll(item => item!.Level == EngineerFilter).OrderBy(engineer => engineer.Id);
        EngineerList = new(temp!);

    }

}
