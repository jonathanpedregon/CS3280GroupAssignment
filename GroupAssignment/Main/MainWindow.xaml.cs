﻿using GroupAssignment.Main;
using GroupAssignment.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GroupAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {
        private DatabaseHandler DbHandler;
        public MainWindow()
        {
            InitializeComponent();
            DbHandler = new DatabaseHandler();
        }

        /// <summary>
        /// This will bring the new window that the user search different data from the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchInvoice_click(object sender, RoutedEventArgs e)
        {
            Search Info = new Search();
            Info.ShowDialog();


        }




        private void updateInvoice_click(object sender, RoutedEventArgs e)
        {
            Update Info = new Update();
            Info.ShowDialog();
        }

        private void AddInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            AddInvoiceCanvas.Visibility = Visibility.Visible;
            GetAndPopulateItems();
            CreateAddInvoiceDataGrid();
        }

        private void GetAndPopulateItems()
        {
            var items = DbHandler.GetItems();
            itemDropDown.Items.Clear();
            foreach (var item in items)
            {
                itemDropDown.Items.Add(item.ToString());
            }
        }

        private void CreateInvoiceButtonClick(object sender, RoutedEventArgs e)
        {
            var date = invoiceDate.SelectedDate;
            if (date.HasValue)
            {
                var invoiceDate = date.Value.ToString("MM/dd/yyyy");
                var id = DbHandler.AddInvoice(invoiceDate);
                invoiceNumberTb.Text = id.ToString();
            }
        }

        private void CreateAddInvoiceDataGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();
            invoiceItemDataGrid.Columns.Add(col1);
            invoiceItemDataGrid.Columns.Add(col2);
            invoiceItemDataGrid.Columns.Add(col3);
            col1.Binding = new Binding("Id");
            col2.Binding = new Binding("Name");
            col3.Binding = new Binding("Cost");
            col1.Width = 50;
            col2.Width = 150;
            col3.Width = 50;
            col1.Header = "Id";
            col2.Header = "Name";
            col3.Header = "Cost";
        }

        private void AddItemButtonClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = itemDropDown.SelectedItem.ToString().Split('-');
            if (selectedItem.Length > 1)
            {
                var itemId = int.Parse(selectedItem[0].Trim());
                var name = selectedItem[1].Trim();
                var cost = Math.Round(decimal.Parse(selectedItem[2].Trim().Substring(1, selectedItem[2].Trim().Length - 1)), 2);
                var invoiceId = int.Parse(invoiceNumberTb.Text);
                DbHandler.InsertItem(itemId, invoiceId);
                invoiceItemDataGrid.Items.Add(new Item(itemId, name, cost));
                IncreaseInvoiceTotal(cost);
            }
        }

        private void IncreaseInvoiceTotal(decimal additionalCost)
        {
            var currentCost = decimal.Parse(invoiceTotalTb.Text.Remove(0, 1));
            var newTotal = currentCost + additionalCost;
            invoiceTotalTb.Text = $"${newTotal}";
        }

        private void EditInvoiceButton_Click(object sender, RoutedEventArgs e)
        {
            EditInvoiceCanvas.Visibility = Visibility.Visible;
            PopulateInvoiceDropdown();
            CreateInvoiceItemsDataGrid();
            PopulateEditInvoiceAddItemDropDown();
        }

        private void PopulateEditInvoiceAddItemDropDown()
        {
            var items = DbHandler.GetItems();
            AddInvoiceItemDropDown.Items.Clear();
            foreach (var item in items)
            {
                AddInvoiceItemDropDown.Items.Add(item.ToString());
            }
        }
        private void PopulateInvoiceDropdown()
        {
            var invoices = DbHandler.GetInvoices();
            invoiceDropDown.Items.Clear();
            foreach (var invoice in invoices)
            {
                invoiceDropDown.Items.Add(invoice.Id);
            }
        }

        private void PopulateInvoiceItemDataGrid(List<InvoiceItem> invoiceItems)
        {
            InvoiceItemsGrid.Items.Clear();
            foreach (var item in invoiceItems)
            {
                InvoiceItemsGrid.Items.Add(item);
            }
        }

        private void PopulateDeleteInvoiceItemDropDown(List<InvoiceItem> invoiceItems)
        {
            DeleteInvoiceItemDropDown.Items.Clear();
            foreach (var invoiceItem in invoiceItems)
            {
                DeleteInvoiceItemDropDown.Items.Add(invoiceItem.InvoiceItemId);
            }
        }

        private void invoiceDropDown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var invoiceId = invoiceDropDown.SelectedValue.ToString();
            var items = DbHandler.GetInvoiceItems(invoiceId);

            PopulateInvoiceItemDataGrid(items);
            PopulateDeleteInvoiceItemDropDown(items);
        }

        private void CreateInvoiceItemsDataGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();
            DataGridTextColumn col4 = new DataGridTextColumn();
            InvoiceItemsGrid.Columns.Add(col1);
            InvoiceItemsGrid.Columns.Add(col2);
            InvoiceItemsGrid.Columns.Add(col3);
            InvoiceItemsGrid.Columns.Add(col4);
            col1.Binding = new Binding("InvoiceItemId");
            col2.Binding = new Binding("Item Id");
            col3.Binding = new Binding("Name");
            col4.Binding = new Binding("Cost");
            col1.Width = 75;
            col2.Width = 50;
            col3.Width = 150;
            col4.Width = 50;
            col1.Header = "InvoiceItemId";
            col2.Header = "Id";
            col3.Header = "Name";
            col4.Header = "Cost";
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            var invoiceId = invoiceDropDown.SelectedValue.ToString();
            var itemId = AddInvoiceItemDropDown.SelectedValue.ToString().Split('-')[0].Trim();
            DbHandler.AddInvoiceItem(invoiceId, itemId);
            var items = DbHandler.GetInvoiceItems(invoiceId);
            PopulateInvoiceItemDataGrid(items);
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            var invoiceItemId = DeleteInvoiceItemDropDown.SelectedValue.ToString();
            var invoiceId = invoiceDropDown.SelectedValue.ToString();
            DbHandler.DeleteInvoiceItem(invoiceItemId);
            var items = DbHandler.GetInvoiceItems(invoiceId);
            PopulateInvoiceItemDataGrid(items);
        }

        /* We will have createInvoice button that will allow the user to create a new invoice
*  it will open up a new window that will allow user to input the date of the invoice
* and the total charge of the invoice
* 
*/

        /* We will have EditInvoice button that will allow the user to edit  invoice
        *  it will open up a new window that will allow user to change the date of the invoice
        * and the total charge of the invoice
        * 
        */
        /* We will have DeleteInvoice button that will allow the user to delete invoice
        *  it will open up a new window that will allow user to delete the date of the invoice
        * and the total charge of the invoice
        * 
        */
        // we can have data access class
        // we will have sql generator
        // invoice class that we can get all invoice data
    }
}
