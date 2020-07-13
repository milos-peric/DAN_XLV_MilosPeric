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
    class ManagerViewModel : ViewModelBase
    {
        ManagerView mView;        
        DataBaseService dataBaseService = new DataBaseService();
        public ManagerViewModel(ManagerView managerView)
        {
            mView = managerView;
            warehouseItem = new vwProduct();
            WarehouseItemList = dataBaseService.GetAllWarehouseItems().ToList();
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


        private ICommand addItemCommand;
        public ICommand AddItemCommand
        {
            get
            {
                if (addItemCommand == null)
                {
                    addItemCommand = new RelayCommand(param => AddNewItemExecute());
                }
                return addItemCommand;
            }
        }

        private void AddNewItemExecute()
        {
            try
            {
                AddItem addItem = new AddItem();
                addItem.ShowDialog();

                if ((addItem.DataContext as AddItemViewModel).IsUpdateWarehouseItem == true)
                {
                    WarehouseItemList = dataBaseService.GetAllWarehouseItems().ToList();                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private ICommand editItemCommand;
        public ICommand EditItemCommand
        {
            get
            {
                if (editItemCommand == null)
                {
                    editItemCommand = new RelayCommand(param => EditItemCommandExecute(), param => CanEditItemCommandExecute());
                }
                return editItemCommand;
            }
        }

        private void EditItemCommandExecute()
        {
            try
            {
                EditItem editItem = new EditItem(WarehouseItem);
                editItem.ShowDialog();
                if ((editItem.DataContext as EditItemViewModel).IsUpdateWarehouseItem == true)
                {
                    WarehouseItemList = dataBaseService.GetAllWarehouseItems().ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditItemCommandExecute()
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

        private ICommand deleteItemCommand;
        public ICommand DeleteItemCommand
        {
            get
            {
                if (deleteItemCommand == null)
                {
                    deleteItemCommand = new RelayCommand(param => DeleteItemCommandExecute(), param => CanDeleteItemCommandExecute());
                }
                return deleteItemCommand;
            }
        }

        private void DeleteItemCommandExecute()
        {
            try
            {
                if (WarehouseItem != null)
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete warehouse item?", "Delete Record", MessageBoxButton.OKCancel);
                    switch (result)
                    {
                        case MessageBoxResult.OK:
                            int itemId = warehouseItem.ID;
                            dataBaseService.DeleteWarehouseItem(itemId);
                            //string logMessage = string.Format("Worker {0} {1} - JMBG:{2}, was deleted from database.", _worker.FirstName,
                            //    _worker.LastName, _worker.JMBG);
                            //actionEventObject.OnActionPerformed(logMessage);
                            WarehouseItemList = dataBaseService.GetAllWarehouseItems().ToList();
                            MessageBox.Show("Item deleted!", "Delete Record");
                            break;
                        case MessageBoxResult.Cancel:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanDeleteItemCommandExecute()
        {
            if (WarehouseItem == null)
            {
                return false;
            }
            else if (WarehouseItem.InStock == true)
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
                        mView.Close();
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
