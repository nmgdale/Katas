using System.Collections.Generic;
using Xunit;

namespace Katas
{
    public class TennisKataTests
    {
        [Theory]
        [InlineData("0-0", "15-0")]
        [InlineData("15-0", "30-0")]
        [InlineData("0-15", "15-15")]
        [InlineData("30-0", "40-0")]
        [InlineData("15-15", "30-15")]
        [InlineData("0-30", "15-30")]
        [InlineData("40-0", "One Wins")]
        [InlineData("30-15", "40-15")]
        [InlineData("15-30", "30-30")]
        [InlineData("0-40", "15-40")]
        [InlineData("40-15", "One Wins")]
        [InlineData("30-30", "40-30")]
        [InlineData("15-40", "30-40")]
        [InlineData("40-30", "One Wins")]
        [InlineData("30-40", "Deuce")]
        [InlineData("Adv One", "One Wins")]
        [InlineData("Adv Two", "Deuce")]
        [InlineData("Deuce", "Adv One")]
        public void PlayerOneScores(string currentScore, string expectedScore)
            => Assert.Equal(TennisGame.AtScore(expectedScore), TennisGame.AtScore(currentScore).PlayerOneScores());

        [Theory]
        [InlineData("0-0", "0-15")]
        [InlineData("15-0", "15-15")]
        [InlineData("0-15", "0-30")]
        [InlineData("30-0", "30-15")]
        [InlineData("15-15", "15-30")]
        [InlineData("0-30", "0-40")]
        [InlineData("40-0", "40-15")]
        [InlineData("30-15", "30-30")]
        [InlineData("15-30", "15-40")]
        [InlineData("0-40", "Two Wins")]
        [InlineData("40-15", "40-30")]
        [InlineData("30-30", "30-40")]
        [InlineData("15-40", "Two Wins")]
        [InlineData("40-30", "Deuce")]
        [InlineData("30-40", "Two Wins")]
        [InlineData("Adv One", "Deuce")]
        [InlineData("Adv Two", "Two Wins")]
        [InlineData("Deuce", "Adv Two")]
        public void PlayerTwoScores(string currentScore, string expectedScore)
            => Assert.Equal(TennisGame.AtScore(expectedScore), TennisGame.AtScore(currentScore).PlayerTwoScores());
    }

    public record TennisGame(string Score)
    {
        public static TennisGame AtScore(string score) => new(score);

        private static readonly Dictionary<TennisGame, (TennisGame PlayerOne, TennisGame PlayerTwo)> Outcomes = new()
        {
            { AtScore("0-0"), (AtScore("15-0"), AtScore("0-15")) },
            { AtScore("15-0"), (AtScore("30-0"), AtScore("15-15")) },
            { AtScore("0-15"), (AtScore("15-15"), AtScore("0-30")) },
            { AtScore("30-0"), (AtScore("40-0"), AtScore("30-15")) },
            { AtScore("15-15"), (AtScore("30-15"), AtScore("15-30")) },
            { AtScore("0-30"), (AtScore("15-30"), AtScore("0-40")) },
            { AtScore("40-0"), (AtScore("One Wins"), AtScore("40-15")) },
            { AtScore("30-15"), (AtScore("40-15"), AtScore("30-30")) },
            { AtScore("15-30"), (AtScore("30-30"), AtScore("15-40")) },
            { AtScore("0-40"), (AtScore("15-40"), AtScore("Two Wins")) },
            { AtScore("40-15"), (AtScore("One Wins"), AtScore("40-30")) },
            { AtScore("30-30"), (AtScore("40-30"), AtScore("30-40")) },
            { AtScore("15-40"), (AtScore("30-40"), AtScore("Two Wins")) },
            { AtScore("40-30"), (AtScore("One Wins"), AtScore("Deuce")) },
            { AtScore("30-40"), (AtScore("Deuce"), AtScore("Two Wins")) },
            { AtScore("Deuce"), (AtScore("Adv One"), AtScore("Adv Two")) },
            { AtScore("Adv One"), (AtScore("One Wins"), AtScore("Deuce")) },
            { AtScore("Adv Two"), (AtScore("Deuce"), AtScore("Two Wins")) },
        };

        public TennisGame PlayerOneScores() => Outcomes[this].PlayerOne;
        public TennisGame PlayerTwoScores() => Outcomes[this].PlayerTwo;
    }
}