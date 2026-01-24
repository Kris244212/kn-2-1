namespace lab21.Strategies
{
    public class NightShippingStrategy : IShippingStrategy
    {
        private readonly StandardShippingStrategy _standardStrategy = new StandardShippingStrategy();

        public decimal CalculateCost(decimal distance, decimal weight)
        {
            return _standardStrategy.CalculateCost(distance, weight) + 20m; // націнка за ніч
        }
    }
}
