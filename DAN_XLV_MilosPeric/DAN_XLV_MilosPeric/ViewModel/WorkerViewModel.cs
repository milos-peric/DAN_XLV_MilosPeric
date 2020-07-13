using DAN_XLV_MilosPeric.Command;
using DAN_XLV_MilosPeric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLV_MilosPeric.ViewModel
{
    class WorkerViewModel : ViewModelBase
    {
        WorkerView wView;
        DataBaseService dataBaseService = new DataBaseService();
        public WorkerViewModel(WorkerView workerView)
        {
            wView = workerView;
            warehouseItem = new vwProduct();
            WarehouseItemList = dataBaseService.GetAllWarehouseItems().ToList();
            StorageCapacity = 0;
            foreach (var item in WarehouseItemList)
            {
                if (item.InStock == true)
                {
                    StorageCapacity += (int)item.Amount;
                }               
            }
        }

        private vwProduct warehouseItem;
        public vwProduct WarehouseItem
        {
            get { return warehouseItem; }
            set
            {
                warehouseItem = value;
                OnPropertyChanged("WarehouseItem");
            }
        }

        private List<vwProduct> warehouseItemList;
        public List<vwProduct> WarehouseItemList
        {
            get { return warehouseItemList; }
            set
            {
                warehouseItemList = value;
                OnPropertyChanged("WarehouseItemList");
            }
        }

        private int storageCapacity;
        public int StorageCapacity
        {
            get { return storageCapacity; }
            set
            {
                storageCapacity = value;
                OnPropertyChanged("StorageCapacity");
            }
        }

        private ICommand changeStorageStatusCommand;
        public ICommand ChangeStorageStatusCommand
        {
            get
            {
                if (changeStorageStatusCommand == null)
                {
                    changeStorageStatusCommand = new RelayCommand(param => ChangeStorageStatusCommandExecute(), param => CanChangeStorageStatusCommandExecute());
                }
                return changeStorageStatusCommand;
            }
        }

        private void ChangeStorageStatusCommandExecute()
        {
            try
            {
                if (WarehouseItem.InStock == false)
                {
                    StorageCapacity += (int)WarehouseItem.Amount;
                    if (StorageCapacity <= 100)
                    {
                        WarehouseItem.InStock = true;
                        dataBaseService.ChangeStoredStatus(WarehouseItem);
                        WarehouseItemList = dataBaseService.GetAllWarehouseItems().ToList();
                        MessageBox.Show("Item added to warehouse successfully.", "Info");
                    }
                    else
                    {
                        StorageCapacity -= (int)WarehouseItem.Amount;
                        MessageBox.Show("Warehouse storage capacity exeeded, cannot store selected items!", "Info");
                    }                   
                }
                else
                {
                    StorageCapacity -= (int)WarehouseItem.Amount;
                    WarehouseItem.InStock = false;
                    dataBaseService.ChangeStoredStatus(WarehouseItem);
                    WarehouseItemList = dataBaseService.GetAllWarehouseItems().ToList();
                    MessageBox.Show("Item removed from warehouse successfully.", "Info");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanChangeStorageStatusCommandExecute()
        {
            if (WarehouseItem == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand logoutCommand;
        public ICommand LogoutCommand
        {
            get
            {
                if (logoutCommand == null)
                {
                    logoutCommand = new RelayCommand(param => Logout());
                    return logoutCommand;
                }
                return logoutCommand;
            }
        }

        public void Logout()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to logout?", "Confirmation", MessageBoxButton.OKCancel);
                switch (result)
                {
                    case MessageBoxResult.OK:
                        Login loginView = new Login();
                        Thread.Sleep(1000);
                        wView.Close();
                        loginView.Show();
                        return;
                    case MessageBoxResult.Cancel:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
