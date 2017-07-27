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
using System.Windows.Input;
using System.Collections.Generic;

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
            filter_options.ItemsSource = Enum.GetValues(typeof(Filters)).Cast<Filters>();

            var locations = db.Offices.ToList();
            office_locations.ItemsSource = locations.Select(q => q.Location);
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
                await Util.ShowMessageBoxAsync("Error !", "Error occured: " + ex.Message);
                Util.WriteLog(ex.Message + "\n" + ex.StackTrace);
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
                    cellRange.EntireRow.AutoFit();
                    cellRange.EntireRow.VerticalAlignment = Microsoft.Office.Interop.Excel.XlTopBottom.xlTop10Top;
                });

                //Getting the location and file name of the excel to save from user.
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveDialog.FilterIndex = 2;

                if (saveDialog.ShowDialog() == true)
                {
                    workbook.SaveAs(saveDialog.FileName);
                    await controller.CloseAsync();
                    await Util.ShowMessageBoxAsync("Message", "Exported Successfully at: " + saveDialog.FileName);
                }
                else
                {
                    await controller.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                await controller.CloseAsync();
                await Util.ShowMessageBoxAsync("Error", ex.Message);
                Util.WriteLog(ex.Message + "\n" + ex.StackTrace);
            }
            finally
            {
                workbook.Close();
                excel.Quit();
                workbook = null;
                excel = null;
            }
        }

        private void headerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in pcViewSource)
            {
                item.IsSelected = true;
            }
        }

        private void headerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in pcViewSource)
            {
                item.IsSelected = false;
            }
        }

        private void txt_filter_value_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Return))
            {
                if (string.IsNullOrEmpty(txt_filter_value.Text))
                {
                    LoadDataSource();
                }
                else
                {
                    var query = txt_filter_value.Text;
                    var filterOption = (Filters)Enum.Parse(typeof(Filters), filter_options.SelectedItem.ToString());

                    LoadDataSource(pcViewSource, filterOption, query);
                }
            }
        }

        private void LoadDataSource(IEnumerable<Pc> PcList, Filters? option, string query)
        {
            query = Util.RejectMarks(query);

            using (var db = new PCEntities())
            {
                if (option != null)
                {
                    switch (option.Value)
                    {
                        case Filters.Pc_Name:
                            PcList = PcList.Where(q => Util.RejectMarks(q.PC_Name).Contains(query) && q.Active == true);
                            break;
                        case Filters.PB:
                            PcList = PcList.Where(q => Util.RejectMarks(q.PB).Contains(query) && q.Active == true);
                            break;
                        case Filters.NV:
                            PcList = PcList.Where(q => Util.RejectMarks(q.NV).Contains(query) &&
                                            q.Active == true);
                            break;
                        case Filters.MAC:
                            PcList = PcList.Where(q => q.MAC != null && q.MAC.ToLower().Equals(query) && q.Active == true);
                            break;
                        case Filters.MAC2:
                            PcList = PcList.Where(q => q.MAC2 != null && q.MAC2.ToLower().Equals(query) && q.Active == true);
                            break;
                        case Filters.IP:
                            PcList = PcList.Where(q => q.IP.Equals(query) && q.Active == true);
                            break;
                        case Filters.Location:
                            PcList = PcList.Where(q => Util.RejectMarks(q.Office_Located).Contains(query) &&
                                            q.Active == true);
                            break;
                        case Filters.Service_Tag:
                            PcList = PcList.Where(q => Util.RejectMarks(q.ServiceTag).Contains(query) &&
                                            q.Active == true);
                            break;
                        default:
                            break;
                    }
                }

                pcDataGrid.ItemsSource = Util.ToObservableCollection(PcList);
            }
        }

        private bool handle = true;

        private void Handle()
        {
            var filterOption = (Filters)Enum.Parse(typeof(Filters), filter_options.SelectedItem.ToString());

            if (filterOption == Filters.Location)
            {
                txt_filter_value.Visibility = Visibility.Collapsed;
                office_locations.Visibility = Visibility.Visible;
            }
            else
            {
                txt_filter_value.Visibility = Visibility.Visible;
                office_locations.Visibility = Visibility.Collapsed;
            }
        }

        private void filter_options_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        private void filter_options_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void office_locations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            var location_option = cmb.SelectedItem.ToString();
            pcDataGrid.ItemsSource = pcViewSource.Where(q => q.Office_Located.Equals(location_option) && q.Active);
        }
    }

}
