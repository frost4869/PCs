using AutoMapper;
using AutoMapper.QueryableExtensions;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PC.DataAccess;
using PC.DataAccess.Repository;
using PC.Utils;
using PC.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Globalization;

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

        private async void ShowMessageBoxAsync(string title, string mess)
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
                ShowMessageBoxAsync("Error !", "Error occured: " + ex.Message);
            }

            changesCounts = 0;
        }

    }
}
