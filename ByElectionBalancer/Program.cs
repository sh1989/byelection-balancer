using System;
using System.Collections.Generic;
using System.Linq;

namespace ByElectionBalancer
{
    public class Program
    {
        private static readonly Card[] phCards =
        {
            new IncreasesVotesCard(5),

            new DecreasesVotesCard(5),
            new DecreasesVotesCard(2),

            new OtherPlayerStealsVotesCard(3),
            new OtherPlayerStealsVotesCard(3),
            new OtherPlayerStealsVotesCard(2),

            new StealsAdditionalVotesCard(2),
            new StealsAdditionalVotesCard(5),
            new StealsAdditionalVotesCard(3)
        };

        private static readonly Card[] dtdCards =
        {
            new IncreasesVotesCard(2),
            new IncreasesVotesCard(5),

            new DecreasesVotesCard(2),
            new DecreasesVotesCard(3),

            new OtherPlayerStealsVotesCard(2),
            new OtherPlayerStealsVotesCard(5),
            new OtherPlayerStealsVotesCard(3),

            new StealsAdditionalVotesCard(5),
            new StealsAdditionalVotesCard(3)
        };

        private static readonly Card[] iCards =
        {
            new IncreasesVotesCard(3),
            new IncreasesVotesCard(2),
            new IncreasesVotesCard(5),

            new DecreasesVotesCard(4),
            new DecreasesVotesCard(3),
            new DecreasesVotesCard(3),

            new OtherPlayerStealsVotesCard(3),
            new OtherPlayerStealsVotesCard(2),

            new StealsAdditionalVotesCard(3)
        };

        private static readonly Card[] hCards =
        {
            new IncreasesVotesCard(5),
            new IncreasesVotesCard(2),

            new DecreasesVotesCard(3),
            new DecreasesVotesCard(3),

            new OtherPlayerStealsVotesCard(2),
            new OtherPlayerStealsVotesCard(2),
            new OtherPlayerStealsVotesCard(5),

            new StealsAdditionalVotesCard(3),
            new StealsAdditionalVotesCard(5)
        };

        private static readonly Suit[] suits =
        {
            new Suit("Public Hearing", 25, phCards),
            new Suit("Door to door", 20, dtdCards),
            new Suit("Interview", 15, iCards),
            new Suit("Hustings", 20, hCards)
        };

        static void Main(string[] args)
        {
            foreach (var suit in suits)
            {
                Console.WriteLine("----");
                Console.WriteLine("{0} (base value: {1})", suit.Name, suit.BaseValue);
                Console.WriteLine("");

                var results = new List<Result>();

                IEnumerable<IEnumerable<Card>> permutations = suit.Cards.Permute(2);
                foreach (var combo in permutations)
                {
                    var tally = new Result(suit.BaseValue);
                    foreach (var result in combo)
                    {
                        tally.Add(result);
                    }
                    results.Add(tally);
                }

                var resultsInOrder = results.OrderByDescending(x => x.VotesScored);
                foreach (var result in resultsInOrder)
                {
                    Console.WriteLine(result);
                }
                Console.WriteLine("");

                Console.WriteLine("{0} permutations, {1:0.##}% are negative, {2:0.##}% are positive, {3:0.##}% score nothing",
                    results.Count,
                    Percentage(x => x.VotesScored < 0, results),
                    Percentage(x => x.VotesScored > 0, results),
                    Percentage(x => x.VotesScored == 0, results));

                Console.WriteLine("Votes won: min = {0}, max = {1}. Average = {2:0.##}, Mode = {3}",
                    results.Min(x => x.VotesScored),
                    results.Max(x => x.VotesScored),
                    results.Average(x => x.VotesScored),
                    String.Join(",", Modes(x => x.VotesScored, results)));

                Console.WriteLine("Stolen by others: min = {0}, max = {1}. Average = {2:0.##}, Mode = {3}",
                    results.Min(x => x.StolenFromThisDeck),
                    results.Max(x => x.StolenFromThisDeck),
                    results.Average(x => x.StolenFromThisDeck),
                    String.Join(",", Modes(x => x.StolenFromThisDeck, results)));

                Console.WriteLine("Stolen from others: min = {0}, max = {1}. Average = {2:0.##}, Mode = {3}",
                    results.Min(x => x.StolenFromOthers),
                    results.Max(x => x.StolenFromOthers),
                    results.Average(x => x.StolenFromOthers),
                    String.Join(",", Modes(x => x.StolenFromOthers, results)));

                Console.WriteLine("");
            }
            Console.ReadKey();
        }

        private static IEnumerable<int> Modes(Func<Result, int> predicate, IReadOnlyCollection<Result> results)
        {
            var modes = results
                .GroupBy(predicate)
                .Select(x => new {Value = x.Key, Frequency = x.Count()})
                .ToList();

            var maxFrequency = modes.Max(x => x.Frequency);

            return modes.Where(x => x.Frequency == maxFrequency && maxFrequency > 1)
                .Select(x => x.Value)
                .OrderBy(x => x);
        }

        private static double Percentage(Func<Result, bool> predicate, IReadOnlyCollection<Result> results)
        {
            var matches = (double) results.Count(predicate);
            return (matches / results.Count) * 100;
        }
    }
}
