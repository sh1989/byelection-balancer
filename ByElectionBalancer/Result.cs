using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByElectionBalancer
{
    public class Result
    {
        private readonly List<Card> cards; 
        private readonly int baseValue;
        private int modifications;

        public Result(int baseValue)
        {
            this.baseValue = baseValue;
            cards = new List<Card>();
        }

        public void Add(Card c)
        {
            if (c.StealsVotes)
            {
                StolenFromThisDeck += (-c.Value);
            }

            modifications += c.Value;
            cards.Add(c);
        }

        public int VotesScored
        {
            get { return baseValue + modifications; }
        }

        public int StolenFromThisDeck { get; private set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("{0} votes: ", baseValue + modifications);
            builder.Append(string.Join(", ", cards.Select(c => c.ToString())));
            return builder.ToString();
        }
    }
}
