using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PC.DataAccess;
using PC.ViewModels;
using System.Threading.Tasks;
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
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Quản lý PC";
            await AuthorizeAsync("Enter Username and Password.");
        }

        private async Task AuthorizeAsync(string message)
        {
            ShowDialogsOverTitleBar = false;
            ShowCloseButton = true;
            LoginDialogData result = await this.ShowLoginAsync("Login", message, new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, EnablePasswordPreview = true});
            if (result != null)
            {
                using (var db = new PCEntities())
                {
                    var user = await db.Users.FindAsync(result.Username);
                    if (user != null)
                    {
                        MainWindowContent.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        await AuthorizeAsync("Wrong Username or Password, try again.");
                    }
                }
            }
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
    }
}
