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
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        private DataAccessLayer DAL;
        private DatabaseHandler DbHandler;
        public Update()
        {
            InitializeComponent();
            DAL = new DataAccessLayer();
            DbHandler = new DatabaseHandler();

            var items = DbHandler.GetItems();
            dataGrid.ItemsSource = DbHandler.GetItems();
            foreach (var item in items)
            {
                comboBoxEdit.Items.Add(item.Name);
            }
            GetItem();
            getItemID();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
/// <summary>
/// Edit an existing Items
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            var ItemName = comboBoxEdit.Text;
            if (ItemName != "")
            {
                var invoiceItems = DbHandler.GetInvoiceItems(ItemName);
                
            }
        }
        /// <summary>
        /// delete an existing items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            var ItemName = comboBoxDelete.Text;
            if (ItemName != "")
            {
                DbHandler.DeleteInvoice(ItemName);
                GetItem();
            }
        }
        /// <summary>
        /// checking the attem what we delete
        /// </summary>
        private void GetItem()
        {
            comboBoxDelete.Items.Clear();
            var items = DbHandler.GetItems();
            foreach (var item in items)
            {
                comboBoxDelete.Items.Add(item.Name);
            }
        }
        /// <summary>
        /// Add a new item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var ID = comboBoxDelete.Text;
            if (ID != "")
            {
                DbHandler.DeleteInvoice(ID);
                getItemID();
            }
        }
        /// <summary>
        /// checking the item waht will addd
        /// </summary>
        private void getItemID()
        {
            comboBoxDelete.Items.Clear();
            var items = DbHandler.GetItems();
            foreach (var item in items)

            {
                comboBoxDelete.Items.Add(item.Id);
            }
        }
    }
}



