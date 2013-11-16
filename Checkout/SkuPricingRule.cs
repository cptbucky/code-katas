namespace Checkout
{
    public interface ISkuPricingRule
    {
        int UnitPrice { get; set; }

        IDiscountRule DiscountRule { get; set; }
    }

    public class SkuPricingRule : ISkuPricingRule
    {
        public int UnitPrice { get; set; }
        public IDiscountRule DiscountRule { get; set; }

        public SkuPricingRule(int unitPrice)
        {
            UnitPrice = unitPrice;
        }
    }
}