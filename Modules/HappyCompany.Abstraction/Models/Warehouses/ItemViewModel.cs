namespace HappyCompany.Abstraction.Models.Warehouses
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string SKUCode { get; set; }
        public int Qty { get; set; }
        public double CostPrice { get; set; }
        public double MSRPPrice { get; set; }
    }
}