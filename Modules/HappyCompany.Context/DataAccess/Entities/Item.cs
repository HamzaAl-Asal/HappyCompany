namespace HappyCompany.Context.DataAccess.Entities
{
    public class Item : EntityBase
    {
        public string SKUCode { get; set; }
        public int Qty { get; set; }
        public double CostPrice { get; set; }
        public double MSRPPrice { get; set; }

        public ICollection<Warehouse> Warehouses { get; set; }
    }
}