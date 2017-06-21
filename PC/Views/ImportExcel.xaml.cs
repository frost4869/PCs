using AutoMapper;
using AutoMapper.QueryableExtensions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PC.DataAccess;
using PC.Utils;
using PC.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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

namespace PC.Views
{
    /// <summary>
    /// Interaction logic for ImportExcel.xaml
    /// </summary>
    public partial class ImportExcel : UserControl
    {
        private MapperConfiguration config = new MapperConfiguration(q =>
        {
            q.CreateMap<Pc, PcViewModel>();
            q.CreateMap<PcViewModel, Pc>();
        });
        private ObservableCollection<Pc> pcList;
        private ObservableCollection<PcViewModel> pcViewModelList;
        public ImportExcel(IEnumerable<Pc> _pcList)
        {
            this.pcList = Util.ToObservableCollection(_pcList);
            InitializeComponent();
            LoadDataSource();
        }

        private void LoadDataSource()
        {
            if (pcList != null)
            {
                pcViewModelList = Util.ToObservableCollection<PcViewModel>(pcList.AsQueryable().ProjectTo<PcViewModel>(config));
                pcImportDataGrid.ItemsSource = pcViewModelList;
            }
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFromExcelAsync();
        }

        private async void SaveFromExcelAsync()
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            MessageDialogStyle style = MessageDialogStyle.AffirmativeAndNegative;
            var result = await metroWindow.ShowMessageAsync("Confirm", "Are you sure to add these records to database ?", style);

            if(result == MessageDialogResult.Affirmative)
            {
                var controller = await metroWindow.ShowProgressAsync("Adding", "Please wait...");
                controller.SetIndeterminate();


                using (var db = new PCEntities())
                {
                    try
                    {
                        await Task.Run(() =>
                        {
                            var list = pcViewModelList.AsQueryable().ProjectTo<Pc>(config);

                            foreach (Pc item in list)
                            {
                                item.Active = true;
                                db.Pcs.Add(item);
                            }
                        });

                        await db.SaveChangesAsync();
                        await controller.CloseAsync();
                        var mesDialogResult = await metroWindow.ShowMessageAsync("Success", "Saved to Database.");
                        btnSaveExcel.Content = "Saved";
                        btnSaveExcel.IsEnabled = false;

                        if (mesDialogResult == MessageDialogResult.Affirmative)
                        {
                            metroWindow.FindChild<MetroAnimatedSingleRowTabControl>("MainTabControl").SelectedIndex = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        await controller.CloseAsync();
                        await metroWindow.ShowMessageAsync("Error", ex.Message);
                        throw;
                    }
                }
            }
            
        }
        private async void btnDeleteExcel_Click(object sender, RoutedEventArgs e)
        {
            var metroWindow = (Application.Current.MainWindow as MetroWindow);
            var result = await metroWindow.ShowMessageAsync("Delete ?", "Are you sure to delete selected records ?");

            if (result == MessageDialogResult.Affirmative)
            {
                var selectedList = pcViewModelList.Where(q => q.IsSelected).ToList();

                foreach (var item in selectedList)
                {
                    pcViewModelList.Remove(item);
                }

                pcImportDataGrid.ItemsSource = pcViewModelList;
            }

        }

        private void headerCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var item in pcViewModelList)
            {
                item.IsSelected = true;
            }
        }

        private void headerCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in pcViewModelList)
            {
                item.IsSelected = false;
            }
        }
    }
}
