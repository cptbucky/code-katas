namespace Checkout
{
    public interface IDiscountRule
    {
        int NoOfSkus { get; set; }

        int DiscountValue { get; set; }
    }

    public class DiscountRule : IDiscountRule
    {
        public int NoOfSkus { get; set; }
        public int DiscountValue { get; set; }

        public DiscountRule(int noOfSkus, int discountToApply)
        {
            NoOfSkus = noOfSkus;
            DiscountValue = discountToApply;
        }
    }
}