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

namespace GroupAssignment
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        private DatabaseHandler DbHandler;


        public Search()
        {
            InitializeComponent();
            DbHandler = new DatabaseHandler();
            var invoices = DbHandler.GetInvoices();

            dataGrid.ItemsSource = DbHandler.GetInvoices();

            foreach (var invoice in invoices)
            {
                invoiceNumber.Items.Add(invoice.Id);
            }

            foreach (var invoice in invoices)
            {
                invoiceDate.Items.Add(invoice.Date);
            }
            //InitializeComponent();
        }
        /// <summary>
        /// select the items in which the user has selected from the drop down box and fill to grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectButton_click(object sender, RoutedEventArgs e)
        {
            var invoiceId = invoiceNumber.Text;
            if (invoiceId != "")
            {
                var invoiceItems = DbHandler.GetInvoiceItems(invoiceId);
                foreach (var invoiceItem in invoiceItems)
                {
                    // var data = new Item() { Id = 0 , Name = "Name", Cost = 0 };
                    //itemDataGrid.Items.Add(invoiceItem);
                }
            }
            //select the data from our Invoicedata() Class 

            var invoiceD = invoiceDate.Text;
            if (invoiceId != "")
            {
                var invoiceDates = DbHandler.GetInvoiceDate(invoiceD);
                foreach (var invoiceItem in invoiceDates)
                {
                    // var data = new Item() { Id = 0 , Name = "Name", Cost = 0 };
                    //itemDataGrid.Items.Add(invoiceItem);
                }
            }
        }
        /// <summary>
        /// It will close the form bring us back 
        /// to the main menu where the user be able to create invoices 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancleButton_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            // it will close the form 
        }

        // we will have a third button that the user search each items
    }
}

