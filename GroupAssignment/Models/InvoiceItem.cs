namespace GroupAssignment.Models
{
    public class InvoiceItem : Item
    {
        public int InvoiceItemId { get; set; }
        public InvoiceItem(int invoiceItemId, int id, string name, decimal cost) : base(id, name, cost)
        {
            InvoiceItemId = invoiceItemId;
        }
    }
}
