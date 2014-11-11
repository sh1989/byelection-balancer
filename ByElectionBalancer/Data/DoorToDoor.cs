
namespace ByElectionBalancer.Data
{
    public class DoorToDoor : Suit
    {
        private static readonly Card[] cards =
        {
            new IncreasesVotesCard(6),
            new IncreasesVotesCard(5),
            new IncreasesVotesCard(4),

            new DecreasesVotesCard(7),
            new DecreasesVotesCard(6),
            new DecreasesVotesCard(4),
            new DecreasesVotesCard(3),

            new OtherPlayerStealsVotesCard(5),
            new OtherPlayerStealsVotesCard(2)
        };

        public DoorToDoor()
            : base("Door to door", 7, cards)
        { }
    }
}
