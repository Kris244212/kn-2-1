namespace lab21.Strategies
{
    public interface IShippingStrategy
    {
        decimal CalculateCost(decimal distance, decimal weight);
    }
}
