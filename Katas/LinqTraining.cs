using System.Collections.Generic;
using System.Linq;
using Bogus;
using Xunit;

namespace Katas
{
    public class LinqTraining
    {
        private static readonly Faker Faker = new();

        private static readonly IEnumerable<Order> Orders = Enumerable.Range(0, Faker.Random.Number(100, 200))
            .Select(_ => new Order
            {
                Items = Enumerable.Range(0, Faker.Random.Number(1, 10))
                    .Select(_ => new Item
                    {
                        Type = Faker.PickRandom("Hardware", "Broadband", "Voice", "Domain"),
                        Price = new Money(Faker.Random.Number(0, 100))
                    })
            });

        [Fact]
        public void Test()
        {
            ;
        }
    }

    public class Order
    {
        public IEnumerable<Item> Items = new List<Item>();
    }

    public class Item
    {
        public string Type;
        public Money Price;
    }
}