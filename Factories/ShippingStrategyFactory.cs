using System;
using System.Collections.Generic;
using lab21.Strategies;

namespace lab21.Factories
{
    public static class ShippingStrategyFactory
    {
        private static readonly Dictionary<string, IShippingStrategy> strategies = new()
        {
            { "Standard", new StandardShippingStrategy() },
            { "Express", new ExpressShippingStrategy() },
            { "International", new InternationalShippingStrategy() },
            { "Night", new NightShippingStrategy() }
        };

        public static IShippingStrategy CreateStrategy(string deliveryType)
        {
            if (strategies.ContainsKey(deliveryType))
                return strategies[deliveryType];
            throw new ArgumentException("Невідомий тип доставки");
        }
    }
}
