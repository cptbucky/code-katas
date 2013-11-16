namespace Checkout
{
    public interface ISkuPricingRule
    {
        int SkuUnitPrice { get; set; }

        IDiscountRule DiscountRule { get; set; }
    }

    public class SkuPricingRule : ISkuPricingRule
    {
        public int SkuUnitPrice { get; set; }
        public IDiscountRule DiscountRule { get; set; }

        public SkuPricingRule(int unitPrice)
        {
            SkuUnitPrice = unitPrice;
        }
    }
}