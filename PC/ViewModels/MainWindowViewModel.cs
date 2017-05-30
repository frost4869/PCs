using MahApps.Metro.Controls;
using PC.DataAccess;
using PC.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace PC.ViewModels
{
    class MainWindowViewModel
    {
        private readonly PCEntities db = new PCEntities();
        private UserControl _userControl;
        public MainWindowViewModel() { }
        public MainWindowViewModel(UserControl userControl)
        {
            this._userControl = userControl;
        }
        private ICommand txtSearchCmt;
        public ICommand TxtSearchCmt
        {
            get
            {
                return this.txtSearchCmt ?? (this.txtSearchCmt = new Command
                {
                    CanExecuteDelegate = q => true,
                    ExecuteDelegate = q =>
                    {
                        if (q is string)
                        {
                            var searchParam = (string)q;

                        }
                    }
                });
            }
        }
    }
}
