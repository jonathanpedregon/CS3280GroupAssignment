using GroupAssignment.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAssignment
{
    public class DatabaseHandler
    {
        private DataAccessLayer DAL { get; set; }

        public DatabaseHandler()
        {
            DAL = new DataAccessLayer();
        }

        public List<Item> GetItems()
        {
            var query = "SELECT * FROM ITEMS";
            var results = DAL.ExecuteSQLStatement(query).Tables[0].Rows;
            return GetItemsFromDataRows(results);

        }

        public int AddInvoice(string invoiceDate)
        {
            var command = $"INSERT INTO Invoice (InvoiceDate) VALUES({invoiceDate})";
            DAL.ExecuteNonQuery(command);
            var query = "SELECT MAX(ID) FROM Invoice";
            var id = int.Parse(DAL.ExecuteSQLStatement(query).Tables[0].Rows[0][0].ToString());
            return id;
        }

        public List<Invoice> GetInvoices()
        {
            var query = "SELECT * FROM Invoice";
            var results = DAL.ExecuteSQLStatement(query).Tables[0].Rows;
            var invoices = new List<Invoice>();
            foreach (DataRow result in results)
            {
                var id = int.Parse(result.ItemArray[0].ToString());
                var splitDate = result.ItemArray[1].ToString().Split('/');
                var month = int.Parse(splitDate[0]);
                var day = int.Parse(splitDate[1]);
                var year = int.Parse(splitDate[2].Split(' ')[0]);
                var date = new DateTime(year, month, day);
                invoices.Add(new Invoice(date, id));
            }
            return invoices;
        }

        public void InsertItem(int itemId, int invoiceId)
        {

            AddInvoiceItem(itemId, invoiceId);
        }

        private void IncrementItemQuantity(int itemId, int invoiceId, int quantity)
        {
            var updateCommand = $"UPDATE InvoiceItems SET Quantity = {++quantity} WHERE InvoiceId = {invoiceId} AND ItemId = {itemId}";
            DAL.ExecuteNonQuery(updateCommand);
        }

        private void AddInvoiceItem(int itemId, int invoiceId)
        {
            var insertCommand = $"INSERT INTO InvoiceItems (InvoiceId, ItemId) VALUES({invoiceId}, {itemId})";
            DAL.ExecuteNonQuery(insertCommand);
        }

        public void AddInvoiceItem(string itemId, string invoiceId)
        {
            var insertCommand = $"INSERT INTO InvoiceItems (InvoiceId, ItemId) VALUES({invoiceId}, {itemId})";
            DAL.ExecuteNonQuery(insertCommand);
        }

        public List<InvoiceItem> GetInvoiceItems(string invoiceId)
        {
            var query = $"SELECT InvoiceItemId, Items.* FROM InvoiceItems, Items WHERE InvoiceItems.ItemId = Items.ID AND InvoiceId = {invoiceId}";
            var results = DAL.ExecuteSQLStatement(query).Tables[0].Rows;
            var invoiceItems = new List<InvoiceItem>();
            foreach (DataRow result in results)
            {
                var invoiceItemId = int.Parse(result.ItemArray[0].ToString());
                var itemId = int.Parse(result.ItemArray[1].ToString());
                var name = result.ItemArray[2].ToString();
                var cost = Math.Round(decimal.Parse(result.ItemArray[3].ToString()), 2);
                invoiceItems.Add(new InvoiceItem(invoiceItemId, itemId, name, cost));
            }
            return invoiceItems;
        }
        
        public List<Item> GetInvoiceDate(string invoiceDate)
        {
            var query = $"SELECT InvoiceDate FROM Invoice WHERE InvoiceDate = {invoiceDate}";
            var results = DAL.ExecuteSQLStatement(query).Tables[0].Rows;
            return GetItemsFromDataRows(results);
        }


        private List<Item> GetItemsFromDataRows(DataRowCollection results)
        {
            var items = new List<Item>();

            foreach (DataRow result in results)
            {
                var id = int.Parse(result.ItemArray[0].ToString());
                var name = result.ItemArray[1].ToString();
                var cost = Math.Round(decimal.Parse(result.ItemArray[2].ToString()), 2);
                items.Add(new Item(id, name, cost));
            }
            return items;
        }

        public void DeleteInvoice(string invoiceId)
        {
            var invoiceItemCommand = $"Delete * FROM InvoiceItems WHERE InvoiceId = {invoiceId}";
            DAL.ExecuteNonQuery(invoiceItemCommand);
            var invoiceCommand = $"DELETE * FROM Invoice WHERE ID = {invoiceId}";
            DAL.ExecuteNonQuery(invoiceCommand);
        }

        internal void DeleteInvoiceItem(string invoiceItemId)
        {
            var deleteCommand = $"Delete * FROM InvoiceItems WHERE InvoiceItemId = {invoiceItemId}";
            DAL.ExecuteNonQuery(deleteCommand);
        }
    }
}
