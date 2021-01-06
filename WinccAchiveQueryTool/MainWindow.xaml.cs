using Ruifei.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Sql;
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

namespace Ruifei.AppMain
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        BatchDataListViewModel vm;
        WinccArchiveDBHelper dBHelper;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vm = this.DataContext as BatchDataListViewModel;
            dBHelper = new WinccArchiveDBHelper(vm.Computer);
            vm.EndTime = DateTime.Now;
            vm.StartTime = DateTime.Now.AddDays(-1);

            _ = Task.Run(async () =>
            {
                var names = await dBHelper.GetDabaseNames();
                foreach (var name in names)
                {
                    vm.DatabaseEnums.Add(name);
                }
            });

            _ = Task.Run(() =>
            {
                vm.ComputerList = new ObservableRangeCollection<string>(GetSqlServerNames());
            });
        }

        private async void btnQuery_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (string.IsNullOrEmpty(vm.ArchiveName))
            {
                MessageBox.Show("请选择变量名");
            }
            if (string.IsNullOrEmpty(vm.DatabaseName))
            {
                MessageBox.Show("请选择归档库");
            }
            if (vm.StartTime == null)
            {
                MessageBox.Show("请选择开始时间");
            }
            if (vm.EndTime == null)
            {
                MessageBox.Show("请选择结束时间");
            }
            try
            {
                await Task.Run(async () =>
                   {
                       var list = await dBHelper.QueryWinccData(vm.ArchiveName, vm.DatabaseName, vm.StartTime.Value, vm.EndTime.Value);
                       vm.BatchDataList = list;
                       vm.TotalCount = list.Count;
                   });
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private async void cbDBName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = await dBHelper.GetAchiveNames(vm.DatabaseName);
            vm.ArchiveNameList = list.Select(a => a.ValueName).ToList();
        }

        private List<string> GetSqlServerNames()
        {
            List<string> ServerNameList = new List<string>();
            //枚举本地计算机SQL服务器列表
            SqlDataSourceEnumerator sqlServer = SqlDataSourceEnumerator.Instance;
            DataTable db = sqlServer.GetDataSources();
            foreach (DataRow row in db.Rows)
            {
                ServerNameList.Add(row[0].ToString().Trim() + @"\" + row[1].ToString().Trim());
            }
            return ServerNameList;
        }
    }
}
