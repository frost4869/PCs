using AutoUpdaterDotNET;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PC.DataAccess;
using PC.Utils;
using System;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;

namespace PC
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : MetroWindow
    {
        public ProgressDialogController controller;

        public StartupWindow()
        {
            InitializeComponent();
            ShowDialogsOverTitleBar = false;
            ShowCloseButton = true;

            DataContext = new MyDataContext();
            this.EnableDWMDropShadow = true;
            WindowStyle = WindowStyle.SingleBorderWindow;
            Assembly assembly = Assembly.GetEntryAssembly();
            this.Title = "Quản lý PC - v" + assembly.GetName().Version;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AuthorizeAsync("Enter Username and Password.");
        }

        private async void AuthorizeAsync(string message)
        {

            LoginDialogData result = await this.ShowLoginAsync("Login", message, new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, EnablePasswordPreview = true });
            if (result != null)
            {
                controller = await this.ShowProgressAsync("Logging in", "Please wait...");
                controller.SetIndeterminate();
                await Task.Run(() =>
                {
                    ValidateLoginAsync(result.Username, result.Password);
                });
            }
        }

        private async void ValidateLoginAsync(string username, string password)
        {
            using (var db = new PCEntities())
            {
                try
                {
                    var user = db.Users.Find(username);
                    if (user != null && user.Password.Equals(password))
                    {

                        Dispatcher.Invoke(async () =>
                        {
                            await controller.CloseAsync();

                            MainWindow main = new MainWindow();
                            Application.Current.MainWindow = main;
                            this.Close();
                            main.Show();
                        });

                    }
                    else
                    {
                        Dispatcher.Invoke(async () =>
                         {
                             await controller.CloseAsync();
                             AuthorizeAsync("Wrong Username or Password, try again.");
                         });
                    }
                }
                catch (Exception ex)
                {
                    Util.WriteLog(ex.Message + "\n" + ex.StackTrace);

                    await this.Dispatcher.Invoke(async () =>
                     {
                         var messResult = await Util.ShowMessageBoxAsync("Error", "Cannot connect to Database !");
                         if (messResult == MessageDialogResult.Affirmative)
                         {
                             this.Close();
                         }
                     });
                }
            }
        }

    }
}
