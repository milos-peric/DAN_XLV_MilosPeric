﻿using DAN_XLV_MilosPeric.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace DAN_XLV_MilosPeric.View
{
    /// <summary>
    /// Interaction logic for EditItem.xaml
    /// </summary>
    public partial class EditItem : Window
    {
        public EditItem(vwProduct warehouseItemEdit)
        {
            InitializeComponent();
            this.DataContext = new EditItemViewModel(this, warehouseItemEdit);
        }
    }
}