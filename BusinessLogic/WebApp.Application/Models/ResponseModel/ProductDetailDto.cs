namespace WebApp.Application.Models.ResponseModel
{
    public class ProductDetailDto 
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string ShopName { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public double? ReviewProd { get; set; }
        public int Price { get; set; }
        public int PriceSale { get; set; }
        public double? Amount { get; set; }
        public string? Note { get; set; }
    }
}
