using PC.DataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.ViewModels
{
    class PcViewModel : Pc, INotifyPropertyChanged, IEditableObject
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

        private PcViewModel backupCopy;
        private bool inEdit;

        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = this.MemberwiseClone() as PcViewModel;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            PC_Name = backupCopy.PC_Name;
            Type = backupCopy.Type;
            HDD = backupCopy.HDD;
            CPU = backupCopy.CPU;
            RAM = backupCopy.RAM;
            OS = backupCopy.OS;
            IP = backupCopy.IP;
            MAC = backupCopy.MAC;
            MAC2 = backupCopy.MAC2;
            NV = backupCopy.NV;
            NVCode = backupCopy.NVCode;
            PB = backupCopy.PB;
            Office_Located = backupCopy.Office_Located;
            ServiceTag = backupCopy.ServiceTag;
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
