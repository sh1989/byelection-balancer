
namespace ByElectionBalancer
{
    public abstract class Card
    {
        private readonly int value;
        public int Value { get { return value; } }

        private readonly bool stealsVotes;
        public bool StealsVotes { get { return stealsVotes; } }

        protected Card(int value, bool stealsVotes)
        {
            this.value = value;
            this.stealsVotes = stealsVotes;
        }
    }

    public class IncreasesVotesCard : Card
    {
        public IncreasesVotesCard(int howMuch)
            : base(howMuch, false) { }
    }

    public class DecreasesVotesCard : Card
    {
        public DecreasesVotesCard(int howMuch)
            : base(-howMuch, false) { }
    }

    public class OtherPlayerStealsVotesCard : Card
    {
        public OtherPlayerStealsVotesCard(int howMuch)
            : base(-howMuch, true) { }
    }

    public class StealsAdditionalVotesCard : Card
    {
        public StealsAdditionalVotesCard(int howMuch)
            : base(howMuch, true) { }
    }
}
