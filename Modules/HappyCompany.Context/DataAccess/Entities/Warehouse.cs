namespace HappyCompany.Context.DataAccess.Entities
{
    public class Warehouse : EntityBase
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}