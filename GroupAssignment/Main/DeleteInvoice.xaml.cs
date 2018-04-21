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

namespace GroupAssignment.Main
{
    /// <summary>
    /// Interaction logic for DeleteInvoice.xaml
    /// </summary>
    public partial class DeleteInvoice : Window
    {
        private DatabaseHandler DbHandler;

        public DeleteInvoice()
        {
            InitializeComponent();
            DbHandler = new DatabaseHandler();
            GetInvoices();
        }

        private void GetInvoices()
        {
            invoiceDropDown.Items.Clear();
            var invoices = DbHandler.GetInvoices();
            foreach (var invoice in invoices)
            {
                invoiceDropDown.Items.Add(invoice.Id);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var invoiceId = invoiceDropDown.Text;
            if(invoiceId != "")
            {
                DbHandler.DeleteInvoice(invoiceId);
                GetInvoices();
            }
        }
    }
}
