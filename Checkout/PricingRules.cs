namespace Checkout
{
    public interface IPricingRules
    {
        int GetSkuPrice(char sku);
    }

    public class PricingRules : IPricingRules
    {
        public int GetSkuPrice(char sku)
        {
            if (sku == 'D')
            {
                return 15;
            }

            if (sku == 'C')
            {
                return 20;
            }

            if (sku == 'B')
            {
                return 30;
            }

            return 50;
        }
    }
}