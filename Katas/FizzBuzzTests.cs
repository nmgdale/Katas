using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Katas
{
    public class FizzBuzzTests
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(2, "2")]
        [InlineData(3, "Fizz")]
        [InlineData(4, "4")]
        [InlineData(5, "Buzz")]
        [InlineData(6, "Fizz")]
        [InlineData(10, "Buzz")]
        [InlineData(15, "FizzBuzz")]
        public void WhenSpeakingANumberCheckTheReply(int number, string reply)
            => Assert.Equal(reply, new FizzBuzzGame().Speak(number));
    }

    public class FizzBuzzGame
    {
        private readonly Dictionary<int, string> _rules = new()
        {
            {3, "Fizz"},
            {5, "Buzz"}
        };

        public string Speak(int number)
        {
            return string.Join("", _rules
                .Where(rule => number % rule.Key == 0)
                .Select(rule => rule.Value)
                .DefaultIfEmpty(number.ToString()));
        }
    }
}