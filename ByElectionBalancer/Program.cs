﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ByElectionBalancer
{
    public class Program
    {
        private static readonly Card[] phCards =
        {
            new Card(5, false),
            new Card(-5, false),
            new Card(-2, false),
            new Card(-3, true),
            new Card(-3, true),
            new Card(-2, true),
            new Card(2, true),
            new Card(5, true),
            new Card(3, true)
        };

        private static readonly Card[] dtdCards =
        {
            new Card(2, false),
            new Card(5, false),
            new Card(-2, false),
            new Card(-3, false),
            new Card(-2, true),
            new Card(-5, true),
            new Card(-3, true),
            new Card(5, true),
            new Card(3, true)
        };

        private static readonly Card[] iCards =
        {
            new Card(3, false),
            new Card(2, false),
            new Card(5, false),
            new Card(-4, false),
            new Card(-3, false),
            new Card(-3, false),
            new Card(-3, true),
            new Card(-2, true),
            new Card(3, true)
        };

        private static readonly Card[] hCards =
        {
            new Card(5, false),
            new Card(2, false),
            new Card(-3, false),
            new Card(-3, false),
            new Card(-2, true),
            new Card(-2, true),
            new Card(-5, true),
            new Card(3, true),
            new Card(5, true)
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
                Console.WriteLine(suit.Name);
                Console.WriteLine("");

                var results = new List<Result>();

                var permutations = AllPermutations(suit.Cards);
                foreach (var combo in permutations)
                {
                    var tally = new Result(suit.BaseValue);
                    tally.Add(combo.Item1);
                    tally.Add(combo.Item2);
                    tally.PrintResult();
                    results.Add(tally);
                }

                Console.WriteLine("Average votes won: {0}", Average(x => x.VotesScored, results));
                Console.WriteLine("Average votes stolen from deck: {0}", Average(x => x.StolenFromThisDeck, results));
                Console.WriteLine("Average votes stolen from other players: {0}", Average(x => x.StolenFromOthers, results));

                Console.WriteLine("");
            }

            Console.ReadKey();

        }

        private static IEnumerable<Tuple<Card, Card>> AllPermutations(IList<Card> cards)
        {
            var perms = new List<Tuple<Card, Card>>();
            for (var i = 0; i < cards.Count - 1; i++) // loop through all but the last
            {
                var first = cards[i];
                for (var j = i + 1; j < cards.Count; j++) // loop from the next to the last
                {
                    perms.Add(new Tuple<Card, Card>(first, cards[j]));
                }
            }
            return perms;
        }

        private static int Average(Func<Result, int> valueToSum, List<Result> results)
        {
            return results.Sum(valueToSum) / results.Count;
        }

    }
}
