namespace GroupAssignment.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }

        public Item(int id, string name, decimal cost)
        {
            Id = id;
            Name = name;
            Cost = cost;
        }

        public override string ToString()
        {
            return $"{Id} - {Name} - ${Cost}";
        }
    }
}
