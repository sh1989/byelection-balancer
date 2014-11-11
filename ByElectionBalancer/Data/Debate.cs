
namespace ByElectionBalancer.Data
{
    public class Debate : Suit
    {
        private static readonly Card[] cards =
        {
            new IncreasesVotesCard(6),
            new IncreasesVotesCard(5),
            new IncreasesVotesCard(4),
            new IncreasesVotesCard(4),

            new DecreasesVotesCard(6),
            new DecreasesVotesCard(6),
            new DecreasesVotesCard(4),
            new DecreasesVotesCard(4),
            new DecreasesVotesCard(4),

            new OtherPlayerStealsVotesCard(5),
            new OtherPlayerStealsVotesCard(3),
            new OtherPlayerStealsVotesCard(3),
            new OtherPlayerStealsVotesCard(4),      
            new OtherPlayerStealsVotesCard(3)
        };

        public Debate()
            : base("Debater", 7, cards)
        { }
    }
}
