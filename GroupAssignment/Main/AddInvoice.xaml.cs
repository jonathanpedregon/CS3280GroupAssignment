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
    /// Interaction logic for AddInvoice.xaml
    /// </summary>
    public partial class AddInvoice : Window
    {
        private DatabaseHandler DbHandler;

        public AddInvoice()
        {
            InitializeComponent();
            DbHandler = new DatabaseHandler();
            var items = DbHandler.GetItems();
            foreach(var item in items)
            {
                itemDropDown.Items.Add(item.ToString());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var date = invoiceDate.SelectedDate;
            if(date.HasValue)
            {
                var invoiceDate = date.Value.ToString("MM/dd/yyyy");
                var id = DbHandler.AddInvoice(invoiceDate);
                invoiceNumberTb.Text = id.ToString();
            }

            
        }
    }
}
