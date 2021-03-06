﻿using DAN_XLV_MilosPeric.Command;
using DAN_XLV_MilosPeric.EventLogger;
using DAN_XLV_MilosPeric.Validation;
using DAN_XLV_MilosPeric.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLV_MilosPeric.ViewModel
{
    class EditItemViewModel : ViewModelBase
    {
        EditItem editItem;
        DataBaseService dataBaseService = new DataBaseService();
        ActionEvent actionEventObject;

        public EditItemViewModel(EditItem editItemOpen, vwProduct warehouseItemEdit)
        {
            editItem = editItemOpen;
            WarehouseItem = warehouseItemEdit;
            WarehouseItemList = dataBaseService.GetAllWarehouseItems().ToList();
            ProductNameBeforeEdit = warehouseItem.ProductName;
            ProductNumberBeforeEdit = warehouseItem.ProductNumber;
            AmountBeforeEdit = warehouseItem.Amount;
            PriceBeforeEdit = warehouseItem.Price;
            actionEventObject = new ActionEvent();
            actionEventObject.ActionPerformed += ActionPerformed;
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

        public string ProductNameBeforeEdit { get; set; }
        public string ProductNumberBeforeEdit { get; set; }
        public int? AmountBeforeEdit { get; set; }
        public string PriceBeforeEdit { get; set; }

        private ICommand editCommand;
        public ICommand EditCommand
        {
            get
            {
                if (editCommand == null)
                {
                    editCommand = new RelayCommand(param => EditCommandExecute(), param => CanEditCommandExecute());
                }
                return editCommand;
            }
        }

        private void EditCommandExecute()
        {
            try
            {
                if (EntryValidation.ValidateProductName(WarehouseItem.ProductName) == false)
                {
                    MessageBox.Show("Product name can only contain letters and numbers. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateProductNumber(WarehouseItem.ProductNumber) == false)
                {
                    MessageBox.Show("Product number can only contain numbers. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidateAmount((int)WarehouseItem.Amount) == false)
                {
                    MessageBox.Show("Amount must be greated than 0 and less or equat to 100. Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidatePriceFormat(WarehouseItem.Price) == false)
                {
                    MessageBox.Show("Price can contain only decimal numbers (numbers, comma and dot is allowed). Please try again", "Invalid input");
                    return;
                }
                if (EntryValidation.ValidatePriceAmount(WarehouseItem.Price) == false)
                {
                    MessageBox.Show("Price must be a positive number. Please try again", "Invalid input");
                    return;
                }
                dataBaseService.EditWarehouseItem(WarehouseItem);
                IsUpdateWarehouseItem = true;
                string logMessage = string.Format("Warehouse Item: {0}, Item number: {1}, Item amount: {2}, Item price: {3} was edited to new Item Name:{4}," +
                    " Item Number: {5}, Item Amount: {6}, Item Price: {7}.", ProductNameBeforeEdit, ProductNumberBeforeEdit, AmountBeforeEdit, PriceBeforeEdit, WarehouseItem.ProductName,
                WarehouseItem.ProductNumber, WarehouseItem.Amount, WarehouseItem.Price);
                actionEventObject.OnActionPerformed(logMessage);
                MessageBox.Show("New Warehouse Item Edited Successfully!", "Info");
                editItem.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanEditCommandExecute()
        {
            if (string.IsNullOrEmpty(warehouseItem.ProductName) || string.IsNullOrEmpty(warehouseItem.ProductNumber) ||
                string.IsNullOrEmpty(warehouseItem.Amount.ToString()) || string.IsNullOrEmpty(warehouseItem.Price))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (cancelCommand == null)
                {
                    cancelCommand = new RelayCommand(param => CancelCommandExecute());
                }
                return cancelCommand;
            }
        }

        private void CancelCommandExecute()
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to close window?", "Close Window", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        editItem.Close();
                        break;
                    case MessageBoxResult.No:
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool _isUpdateWarehouseItem;
        public bool IsUpdateWarehouseItem
        {
            get
            {
                return _isUpdateWarehouseItem;
            }
            set
            {
                _isUpdateWarehouseItem = value;
            }
        }

        void ActionPerformed(object source, ActionEventArgs args)
        {
            Logger.logMessage = args.LogMessage;
        }
    }
}
