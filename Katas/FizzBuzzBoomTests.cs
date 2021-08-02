using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Katas
{
    public class FizzBuzzBoomTests
    {
        [Theory]
        [InlineData(1, "1")]
        [InlineData(2, "2")]
        [InlineData(3, "Fizz")]
        [InlineData(5, "Buzz")]
        [InlineData(6, "Fizz")]
        [InlineData(10, "Boom")]
        [InlineData(30, "Boom")]
        [InlineData(15, "FizzBuzz")]
        public void WhenSpeakingANumberCheckTheReply(int number, string answer)
            => Assert.Equal(answer, new FizzBuzzBoomGame().Speak(number));
    }

    public class FizzBuzzBoomGame
    {
        private readonly IEnumerable<(int Number, string Word)> _overrideRules = new[]
        {
            (10, "Boom")
        };

        private readonly IEnumerable<(int Number, string Word)> _rules = new[]
        {
            (3, "Fizz"),
            (5, "Buzz")
        };

        public string Speak(int number)
        {
            return _overrideRules
                .Where(rule => number % rule.Number == 0)
                .Select(rule => rule.Word)
                .DefaultIfEmpty(string.Join("", _rules
                    .Where(rule => number % rule.Number == 0)
                    .Select(rule => rule.Word)
                    .DefaultIfEmpty(number.ToString())))
                .First();
        }
    }
}