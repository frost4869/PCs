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

            if(result == MessageDialogResult.Affirmative)
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

        private async void PcViewSource_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var edited_pc = e.Row.DataContext as Pc;
            var entity = config.CreateMapper().Map<Pc>(edited_pc);
           
            //update a pc
            if (edited_pc.ID > 0)
            {
                var result = ShowMessageBox("Confirm", "Are you sure to update this record ?");
                if (result == MessageDialogResult.Affirmative)
                {
                    db.Pcs.Attach(entity);
                    db.Entry(entity).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                else
                {
                    LoadDataSource();
                }
            }
            //add a new pc
            else
            {
                var result = ShowMessageBox("Confirm", "Are you sure to create this pc: " + entity.PC_Name + " ?");
                if (result == MessageDialogResult.Affirmative)
                {
                    entity.Active = true;
                    db.Pcs.Add(entity);
                    await db.SaveChangesAsync();
                }
            }
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
    }
}
