using ByElectionBalancer.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ByElectionBalancer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var suits = new Suit[]
            {
                new PublicAppearance(),
                new DoorToDoor(),
                new Interview(),
                new Rally()
            };

            foreach (var suit in suits)
            {
                Console.WriteLine("----{0} (base value: {1})----", suit.Name, suit.BaseValue);

                var results = new List<Result>();

                var permutations = suit.Cards.Permute(3);
                foreach (var combo in permutations)
                {
                    var tally = new Result(suit.BaseValue);
                    foreach (var result in combo)
                    {
                        tally.Add(result);
                    }
                    results.Add(tally);
                }

                if (args.Contains("-v"))
                {
                    PrintPermutations(x => x.VotesScored, results);
                }

                Console.WriteLine("{0} permutations, {1:0.##}% are negative, {2:0.##}% are positive, {3:0.##}% score nothing",
                    results.Count,
                    Percentage(x => x.VotesScored < 0, results),
                    Percentage(x => x.VotesScored > 0, results),
                    Percentage(x => x.VotesScored == 0, results));

                PrintStats("Votes won", x => x.VotesScored, results);
                PrintStats("Votes stolen", x => x.StolenFromThisDeck, results);

                Console.WriteLine("");
            }
            Console.ReadKey();
        }

        private static void PrintPermutations(Func<Result, int> predicate, IEnumerable<Result> results)
        {
            var resultsInOrder = results.OrderByDescending(predicate);
            foreach (var result in resultsInOrder)
            {
                Console.WriteLine(result);
            }
            Console.WriteLine("");
        }

        private static void PrintStats(string title, Func<Result, int> predicate, IEnumerable<Result> results)
        {
            Console.WriteLine("{0}:, min = {1}, max = {2}. Average = {3:0.##}, Mode = {4}",
                title,
                results.Min(predicate),
                results.Max(predicate),
                results.Average(predicate),
                string.Join(",", Modes(predicate, results)));

        }

        private static IEnumerable<int> Modes(Func<Result, int> predicate, IEnumerable<Result> results)
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
