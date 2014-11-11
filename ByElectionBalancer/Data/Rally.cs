﻿
namespace ByElectionBalancer.Data
{
    public class Rally : Suit
    {
        private static readonly Card[] cards =
        {
            new IncreasesVotesCard(7),
            new IncreasesVotesCard(5),
            new IncreasesVotesCard(4), 

            new DecreasesVotesCard(5),
            new DecreasesVotesCard(3),

            new OtherPlayerStealsVotesCard(4),
            new OtherPlayerStealsVotesCard(7),
            new OtherPlayerStealsVotesCard(4),
            new OtherPlayerStealsVotesCard(4)
        };

        public Rally()
            : base("Rally", 7, cards)
        { }
    }
}
