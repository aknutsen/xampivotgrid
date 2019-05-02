using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Infragistics.Olap;
using Infragistics.Olap.FlatData;

namespace XamPivotGridTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }

        private void SaveLayout(object sender, RoutedEventArgs e)
        {
            var layout = ((ViewModel) DataContext).FlatDataSource.SaveCustomizations();
            Debug.WriteLine(layout);

        }

        private void ApplyLayout(object sender, RoutedEventArgs e)
        {
            ((ViewModel) DataContext).FlatDataSource.LoadCustomizations(@"<?xml version=""1.0"" encoding=""utf - 16""?>
                <DataSourceSnapshot>
                <DataSourceType>Flat</DataSourceType>
                <Rows>
                <FilterViewModel>
                <HierarchyUniqueName>[ReportLevel1].[ReportLevel1]</HierarchyUniqueName>
                </FilterViewModel>
                <FilterViewModel>
                <HierarchyUniqueName>[ReportLevel2].[ReportLevel2]</HierarchyUniqueName>
                </FilterViewModel>
                <FilterViewModel>
                <HierarchyUniqueName>[Unit].[Unit]</HierarchyUniqueName>
                </FilterViewModel>
                </Rows>
                <Columns>
                <FilterViewModel>
                <HierarchyUniqueName>[Year].[Year]</HierarchyUniqueName>
                </FilterViewModel>
                </Columns>
                <Measures>
                <string>Value</string>
                </Measures>
                <DefferedUpdate>false</DefferedUpdate>
                </DataSourceSnapshot> ");
        }
    }
}
