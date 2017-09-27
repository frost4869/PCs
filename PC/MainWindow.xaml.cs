using AutoUpdaterDotNET;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using PC.DataAccess;
using PC.ViewModels;
using PC.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace PC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private PCEntities pcContext = new PCEntities();
        private readonly MainWindowViewModel viewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;

            Assembly assembly = Assembly.GetEntryAssembly();
            this.EnableDWMDropShadow = true;
            WindowState = WindowState.Maximized;
            WindowStyle = WindowStyle.SingleBorderWindow;
            this.Title = "Quản lý PC - v" + assembly.GetName().Version;

            AutoUpdater.CurrentCulture = new CultureInfo("en-EN");
            AutoUpdater.LetUserSelectRemindLater = true;
            AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Minutes;
            AutoUpdater.RemindLaterAt = 1;
            AutoUpdater.ReportErrors = true;
        }

        private void BtnCreateClick(object sender, RoutedEventArgs e)
        {
            MetroTabItem item = new MetroTabItem
            {
                Header = "Create new PC",
                CloseButtonEnabled = true,
            };

            Views.CreateForm view = new Views.CreateForm();
            item.Content = view;

            MainTabControl.Items.Add(item);
            item.Focus();
        }

        private async void BtnImportClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Excel Files|*.xlsx*| All files|*.*";
            if (openDialog.ShowDialog() == true)
            {
                ImportExcel(openDialog.FileName, openDialog.SafeFileName);
            }
        }

        private void MainTabControl_TabItemClosingEvent(object sender, BaseMetroTabControl.TabItemClosingEventArgs e)
        {
            if (e.ClosingTabItem.Header.ToString().StartsWith("sizes"))
                e.Cancel = true;
        }

        private void BtnFilterClick(object sender, RoutedEventArgs e)
        {
            MetroTabItem item = new MetroTabItem
            {
                Header = "Filter",
                CloseButtonEnabled = true,
            };

            Views.Filter view = new Views.Filter();
            item.Content = view;

            MainTabControl.Items.Add(item);
            item.Focus();
        }

        private async void ImportExcel(string filePath, string fileName)
        {
            var controller = await this.ShowProgressAsync("Importing", "Please wait...");
            controller.SetIndeterminate();

            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = application.Workbooks.Open(filePath);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;

            int rowCount = range.Rows.Count;
            int columnCount = range.Columns.Count;

            var pcList = new List<Pc>();
            await Task.Run(() =>
            {
                for (int i = 2; i <= rowCount; i++)
                {
                    var listResultString = new List<string>();
                    for (int j = 1; j <= columnCount; j++)
                    {
                        if (range.Cells[i, j] != null && range.Cells[i, j].Value != null)
                        {
                            listResultString.Add(range.Cells[i, j].Value.ToString());
                        }
                        else
                        {
                            listResultString.Add("");
                        }
                    }

                    pcList.Add(new Pc
                    {
                        PC_Name = listResultString[0] == "" ? null : listResultString[0],
                        Type = listResultString[1] == "" ? null : listResultString[1],
                        HDD = listResultString[2] == "" ? null : listResultString[2],
                        CPU = listResultString[3] == "" ? null : listResultString[3],
                        RAM = listResultString[4] == "" ? null : listResultString[4],
                        OS = listResultString[5] == "" ? null : listResultString[5],
                        IP = listResultString[6] == "" ? null : listResultString[6],
                        MAC = listResultString[7] == "" ? null : listResultString[7],
                        MAC2 = listResultString[8] == "" ? null : listResultString[8],
                        NV = listResultString[9] == "" ? null : listResultString[9],
                        NVCode = listResultString[10] == "" ? null : listResultString[10],
                        PB = listResultString[11] == "" ? null : listResultString[11],
                        Office_Located = listResultString[12] == "" ? null : listResultString[12],
                        ServiceTag = listResultString[13] == "" ? null : listResultString[13],
                        Model = listResultString[14] == "" ? null : listResultString[14],
                        Mouse = listResultString[15] == "" ? null : listResultString[15],
                        KeyBoard = listResultString[16] == "" ? null : listResultString[16],
                        Notes = listResultString[17] == "" ? null : listResultString[17],
                    });
                }

                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(range);
                Marshal.ReleaseComObject(worksheet);

                //close and release
                workbook.Close();
                Marshal.ReleaseComObject(workbook);

                //quit and release
                application.Quit();
                Marshal.ReleaseComObject(application);
            });

            

            MetroTabItem item = new MetroTabItem
            {
                Header = fileName,
                CloseButtonEnabled = true,
            };
            ImportExcel view = new Views.ImportExcel(pcList);

            item.Content = view;

            await controller.CloseAsync();

            MainTabControl.Items.Add(item);
            item.Focus();
        }

        private void BtnCheckUpdateClicked(object sender, RoutedEventArgs e)
        {
            AutoUpdater.Start("https://raw.githubusercontent.com/frost4869/uploadfiles/master/update.xml");
        }
    }
}
