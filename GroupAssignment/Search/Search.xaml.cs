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
        public Search()
        {
            InitializeComponent();
        }
        /// <summary>
        /// select the items in which the user has selected from the drop down box and fill to grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectButton_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            //select the data from our Invoicedata() Class 
            
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
