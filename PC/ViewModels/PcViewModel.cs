using PC.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.ViewModels
{
    class PcViewModel : Pc, INotifyPropertyChanged
    {
        private bool mIsSelected;
        private bool mIsUpdated;

        public bool IsSelected
        {
            get
            {
                return mIsSelected;
            }
            set
            {
                mIsSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public bool IsUpdated
        {
            get
            {
                return mIsUpdated;
            }
            set
            {
                mIsUpdated = value;
                OnPropertyChanged("IsUpdated");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
