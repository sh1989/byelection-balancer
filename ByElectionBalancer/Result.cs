using System;

namespace ByElectionBalancer
{
    public class Result
    {
        private readonly int baseValue;
        private int modifications;

        public Result(int baseValue)
        {
            this.baseValue = baseValue;
        }

        public void Add(Card c)
        {
            if (c.StealsVotes)
            {
                if (c.Value > 0)
                {
                    StolenFromOthers += c.Value;
                }
                else
                {
                    StolenFromThisDeck += (-c.Value);
                }
            }

            modifications += c.Value;
        }

        public int VotesScored
        {
            get { return baseValue + modifications; }
        }

        public int StolenFromThisDeck { get; private set; }

        public int StolenFromOthers { get; private set; }

        public void PrintResult()
        {
            var baseResult = string.Format("{0} votes", baseValue + modifications);
            var stolenFromDeckResult = "";
            var stolenFromOthersResult = "";

            if (StolenFromThisDeck > 0)
            {
                stolenFromDeckResult = string.Format(" ({0} stolen from this deck", StolenFromThisDeck);
            }
            if (StolenFromOthers > 0)
            {
                stolenFromOthersResult = string.Format(" ({0} stolen from another player", StolenFromOthers);
            }

            Console.WriteLine(baseResult + stolenFromDeckResult + stolenFromOthersResult);
        }
    }
}
