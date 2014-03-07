namespace Checkout
{
    using System.Collections.Generic;
    using System.Linq;

    public interface IPricingEngine
    {
        IDictionary<char, ISkuPricingRule> SkuPricingRules { get; }

        int GetTotalPriceOfSkus(char[] skus);
    }

    public class PricingEngine : IPricingEngine
    {
        public IDictionary<char, ISkuPricingRule> SkuPricingRules { get; private set; }

        public PricingEngine(IDictionary<char, ISkuPricingRule> skuRules)
        {
            this.SkuPricingRules = skuRules;
        }

        public int GetTotalPriceOfSkus(char[] skus)
        {
            var rawPrice = skus.Sum(x => this.SkuPricingRules[x].SkuUnitPrice);
            var discountsToApply = 0;

            var linesInOrder = skus.Distinct();

            foreach (var skuLine in linesInOrder)
            {
                var discountRule = this.SkuPricingRules[skuLine].DiscountRule;

                if (discountRule == null)
                {
                    continue;
                }

                var noOfTimesToApplyDiscount = skus.Count(x => x == skuLine) / discountRule.NoOfSkus;

                discountsToApply += noOfTimesToApplyDiscount * discountRule.DiscountValue;
            }

            return rawPrice - discountsToApply;
        }
    }
}