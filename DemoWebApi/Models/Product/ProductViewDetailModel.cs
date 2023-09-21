namespace DemoWebApi.Models.Product
{
    public class ProductViewDetailModel
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string ShopName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double? ReviewProd { get; set; }
        public int Price { get; set; }
        public int PriceSale { get; set; }
        public double? Amount { get; set; }
    }
}
