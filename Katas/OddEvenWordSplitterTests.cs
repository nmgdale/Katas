using System.Linq;
using Xunit;

namespace Katas
{
    public class OddEvenWordSplitterTests
    {
        [Theory]
        [InlineData("Nick", "Nc ik")]
        [InlineData("Nicholas", "Ncoa ihls")]
        [InlineData("N", "N")]
        public void Test(string word, string answer)
            => Assert.Equal(answer, new OddEvenWordSplitter().Split(word));
    }

    public class OddEvenWordSplitter
    {
        public string Split(string word)
        {
            return string.Join(" ", word
                .ToCharArray()
                .Select((character, i) => new { character, isEven = i % 2 == 0 })
                .GroupBy(character => character.isEven)
                .Select(grouping => string.Join("", grouping.Select(x => x.character))));
        }
    }
}