using AutoMapper;
using AutoMapper.QueryableExtensions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PC.DataAccess;
using PC.Utils;
using PC.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using Microsoft.Win32;

namespace PC.Views
{
    /// <summary>
    /// Interaction logic for AllPC.xaml
    /// </summary>
    public partial class AllPC : UserControl
    {
        private readonly PCEntities db = new PCEntities();
        private ObservableCollection<PcViewModel> pcViewSource = new ObservableCollection<PcViewModel>();
        private MapperConfiguration config = new MapperConfiguration(q =>
        {
            q.CreateMap<Pc, PcViewModel>();
            q.CreateMap<PcViewModel, Pc>();
        });
        private int changesCounts = 0;
        public AllPC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                LoadDataSource();
            }
        }

        private async void BtnDeleteClicked(object sender, RoutedEventArgs e)
        {
            var result = ShowMessageBox("Delete ?", "Are you sure to delete selected records ?");

            if (result == MessageDialogResult.Affirmative)
            {
                var selectedList = pcViewSource.Where(q => q.IsSelected)
                .AsQueryable()
                .ProjectTo<Pc>(config);

                foreach (var item in selectedList)
                {
                    item.Active = false;
                    db.Pcs.Attach(item);
                    db.Entry(item).State = EntityState.Modified;
                }
                await db.SaveChangesAsync();
            }

            LoadDataSource();
        }

        private void PcViewSource_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var edited_pc = e.Row.DataContext as PcViewModel;

            pcViewSource.FirstOrDefault(q => q.ID == edited_pc.ID).IsUpdated = true;
            ++changesCounts;
        }

        private void LoadDataSource()
        {
            var pcViewModelList = db.Pcs.Where(q => q.Active)
                   .ProjectTo<PcViewModel>(config);

            pcViewSource = Util.ToObservableCollection(pcViewModelList);
            pcDataGrid.ItemsSource = pcViewSource;
        }

        private MessageDialogResult ShowMessageBox(string title, string message)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var messDialogSetting = new MetroDialogSettings
            {
                AffirmativeButtonText = "Yes",
                NegativeButtonText = "No",
                FirstAuxiliaryButtonText = "Cancel",
            };

            var result = metroWindow.ShowModalMessageExternal(title, message,
                    MessageDialogStyle.AffirmativeAndNegativeAndSingleAuxiliary, messDialogSetting);

            return result;
        }

        private async Task ShowMessageBoxAsync(string title, string mess)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            await metroWindow.ShowMessageAsync(title, mess);
        }

        private void BtnEditChecked(object sender, RoutedEventArgs e)
        {
            BtnDelete.Visibility = Visibility.Visible;
            BtnUpdate.Visibility = Visibility.Visible;
            pcDataGrid.IsReadOnly = false;
        }

        private async void BtnEditUnchecked(object sender, RoutedEventArgs e)
        {
            if (changesCounts > 0)
            {
                var result = ShowMessageBox("Save changes ?", "Do you want to save changes before turn off edit mode ?");
                if (result == MessageDialogResult.Affirmative)
                {
                    await UpdateAsync();
                    BtnDelete.Visibility = Visibility.Hidden;
                    BtnUpdate.Visibility = Visibility.Hidden;
                    pcDataGrid.IsReadOnly = true;

                    changesCounts = 0;
                    LoadDataSource();
                }
                else if (result == MessageDialogResult.FirstAuxiliary)
                {
                    btn_toggle_edit.IsChecked = true;
                }
                else
                {
                    BtnDelete.Visibility = Visibility.Hidden;
                    BtnUpdate.Visibility = Visibility.Hidden;
                    pcDataGrid.IsReadOnly = true;

                    changesCounts = 0;
                    LoadDataSource();
                }

            }
            else
            {
                BtnDelete.Visibility = Visibility.Hidden;
                BtnUpdate.Visibility = Visibility.Hidden;
                pcDataGrid.IsReadOnly = true;

                LoadDataSource();
            }

        }

        private async void BtnUpdateClicked(object sender, RoutedEventArgs e)
        {
            var result = ShowMessageBox("Confirm", "Are you sure to update modified records (highlighted)?");
            if (result == MessageDialogResult.Affirmative)
            {
                await UpdateAsync();
                LoadDataSource();
            }
        }

        private async Task UpdateAsync()
        {
            var mapper = config.CreateMapper();
            try
            {
                using (var db = new PCEntities())
                {
                    foreach (var item in pcViewSource)
                    {
                        var entity = mapper.Map<Pc>(item);

                        if (item.ID == 0)
                        {
                            //ad new pc
                            entity.Active = true;
                            db.Pcs.Add(entity);
                        }
                        else if (item.IsUpdated)
                        {
                            //update a pc
                            db.Pcs.Attach(entity);
                            db.Entry(entity).State = EntityState.Modified;
                        }
                    }
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                await ShowMessageBoxAsync("Error !", "Error occured: " + ex.Message);
            }

            changesCounts = 0;
        }

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            var list = pcViewSource.AsQueryable().ProjectTo<Pc>(config).ToList();
            var dataTable = Util.ToDataTable<Pc>(list);
            ExportExcelAsync(dataTable);
        }

        private async void ExportExcelAsync(DataTable dt)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var controller = await metroWindow.ShowProgressAsync("Export Excel", "Please wait...");
            controller.SetIndeterminate();

            
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            excel.DisplayAlerts = false;
            excel.Visible = false;
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;
            var datetime = DateTime.Now.ToString();
            worksheet.Name = "Pc List " + datetime.Replace("/", ".").Replace(":", "-");
            Microsoft.Office.Interop.Excel.Range cellRange;
            try
            {
                await Task.Run(() =>
                {
                    var tempDT = dt;

                    var rowCount = 1;
                    Microsoft.Office.Interop.Excel.Range rng = worksheet.Cells[1, 1] as Microsoft.Office.Interop.Excel.Range;
                    rng.EntireRow.Font.Bold = true;

                    for (int i = 1; i < tempDT.Columns.Count - 1; i++)
                    {
                        worksheet.Cells[1, i] = tempDT.Columns[i].ColumnName;
                    }

                    foreach (DataRow row in tempDT.Rows)
                    {
                        rowCount += 1;
                        for (int i = 1; i < tempDT.Columns.Count - 1; i++) //taking care of each column  
                        {
                            worksheet.Cells[rowCount, i] = row[i].ToString();
                        }
                    }

                    cellRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[rowCount, tempDT.Columns.Count]];
                    cellRange.EntireColumn.AutoFit();

                });

                //Getting the location and file name of the excel to save from user.
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    await controller.CloseAsync();
                    await ShowMessageBoxAsync("Message", "Exported Successfully at: " + saveDialog.FileName);
                }
            }
            catch (Exception ex)
            {
                await controller.CloseAsync();
                await ShowMessageBoxAsync("Error", ex.Message);
            }
            finally
            {
                workbook.Close();
                excel.Quit();
                workbook = null;
                excel = null;
            }
        }
    }

}
