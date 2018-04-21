using System;
using System.Collections.Generic;

namespace GroupAssignment.Models
{
    public class Invoice
    {
        public DateTime Date { get; set; }
        public int Id { get; set; }
        public List<Item> Items { get; set; }

        public Invoice(DateTime date, int id)
        {
            Date = date;
            Id = id;
        }
    }
}
