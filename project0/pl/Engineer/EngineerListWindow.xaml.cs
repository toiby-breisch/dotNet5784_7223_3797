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
/// /////////////Engineer Or EngineerInList
public partial class EngineerListWindow : Window
{

    public BO.EngineerExperience EngineerFilter { get; set; } = BO.EngineerExperience.None;
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public ObservableCollection<BO.Engineer> EngineerList
    {
        get { return (ObservableCollection<BO.Engineer>)GetValue(EngineerListProperty); }
        set { SetValue(EngineerListProperty, value); }
    }

    public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

    public EngineerListWindow()
    {
        InitializeComponent();
        var temp = s_bl?.Engineer.ReadAll();//
        EngineerList = temp == null ? new() : new(temp);/////////////
    }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var temp = EngineerFilter == BO.EngineerExperience.None?
        s_bl?.Engineer.ReadAll():
            s_bl?.Engineer.ReadAll(item => item!.Level == EngineerFilter);
            EngineerList = temp == null ? new() : new(temp);

    }
    private void btnAddOrUpdate_Click(object sender, RoutedEventArgs e)
    {
        EngineerWindow win = new EngineerWindow();
        win.ShowDialog();

    }
    private void UpdateThisObject(object sender, MouseButtonEventArgs e)
    {
        BO.Engineer? engineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
        new EngineerWindow(engineerInList!.Id).ShowDialog();
        var temp = s_bl?.Engineer.ReadAll();//
        EngineerList= new(temp!);/////////////

    }

}
