﻿using KundenVerwaltung.ViewModel;
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


namespace KundenVerwaltung.View
{
    /// <summary>
    /// Interaktionslogik für AddCustomerDialog.xaml
    /// </summary>
    public partial class AddCustomerDialog : Window
    {
        public AddCustomerDialog(AddCustomerDialogViewModel addCustomerDialogViewModel)
        {
            InitializeComponent();
            DataContext = addCustomerDialogViewModel;
        }


    }
}
