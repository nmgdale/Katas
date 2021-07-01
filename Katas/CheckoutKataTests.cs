using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Katas
{
    public class CheckoutKataTests
    {
        private readonly Checkout _checkout;

        public CheckoutKataTests() => _checkout = new Checkout();

        [Theory]
        [InlineData("A", 10)]
        [InlineData("B", 20)]
        public void WhenScanningAnIndividualProduct(string productSku, int amount)
        {
            _checkout.Scan(new Product(productSku));

            _checkout.Total().Should().Be(new Money(amount));
        }

        [Fact]
        public void WhenScanningOneOfEachProductTheTotalIsCorrect()
        {
            _checkout.Scan(new Product("A"));
            _checkout.Scan(new Product("B"));

            _checkout.Total().Should().Be(new Money(30));
        }

        [Fact]
        public void WhenScanningTwoATheDiscountIsApplied()
        {
            _checkout.Scan(new Product("A"));
            _checkout.Scan(new Product("A"));

            _checkout.Total().Should().Be(new Money(15));
        }

        [Fact]
        public void WhenScanningThreeBTheDiscountIsApplied()
        {
            _checkout.Scan(new Product("B"));
            _checkout.Scan(new Product("B"));
            _checkout.Scan(new Product("B"));

            _checkout.Total().Should().Be(new Money(50));
        }

        [Fact]
        public void WhenScanningFourATheDiscountIsAppliedTwice()
        {
            _checkout.Scan(new Product("A"));
            _checkout.Scan(new Product("A"));
            _checkout.Scan(new Product("A"));
            _checkout.Scan(new Product("A"));

            _checkout.Total().Should().Be(new Money(30));
        }

        [Fact]
        public void WhenScanningThreeATheDiscountIsAppliedOnce()
        {
            _checkout.Scan(new Product("A"));
            _checkout.Scan(new Product("A"));
            _checkout.Scan(new Product("A"));

            _checkout.Total().Should().Be(new Money(25));
        }
    }

    public class Checkout
    {
        private readonly Dictionary<Product, Money> _prices = new()
        {
            {new Product("A"), new Money(10)},
            {new Product("B"), new Money(20)},
        };

        private readonly IEnumerable<Discount> _discounts = new List<Discount>
        {
            new (new Product("A"), 2, new Money(-5)),
            new (new Product("B"), 3, new Money(-10)),
        };

        private readonly List<Product> _basket = new();

        public void Scan(Product product) => _basket.Add(product);

        public Money Total()
        {
            var total = _basket.Aggregate(new Money(0), (money, product) => money.Add(_prices[product]));

            return _discounts.SelectMany(discount => discount.Trigger(_basket))
                .Aggregate(total, (original, amount) => original.Add(amount));
        }
    }

    public record Money(int Amount)
    {
        public Money Add(Money other) => new(Amount + other.Amount);
    }

    public record Product(string Sku);

    public record Discount(Product Product, int Quantity, Money Amount)
    {
        public IEnumerable<Money> Trigger(List<Product> basket) =>
            Enumerable.Range(0, NumberOfDiscountsRequired(basket) / Quantity)
                .Select(_ => Amount);

        private int NumberOfDiscountsRequired(IEnumerable<Product> basket)
            => basket.Count(product => product.Equals(Product));
    }
}