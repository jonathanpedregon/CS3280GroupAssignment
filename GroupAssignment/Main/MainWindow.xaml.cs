using GroupAssignment.Main;
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
       
        public MainWindow()
        {
            InitializeComponent();
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddInvoice();
            addWindow.Show();
        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            var editWindow = new EditInvoice();
            editWindow.Show();
        }

        private void button_Copy1_Click(object sender, RoutedEventArgs e)
        {
            var deleteWindow = new DeleteInvoice();
            deleteWindow.Show();
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
