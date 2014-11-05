
namespace ByElectionBalancer
{
    public class Card
    {
        private readonly int value;
        public int Value { get { return value; } }

        private readonly bool stealsVotes;
        public bool StealsVotes { get { return stealsVotes; } }

        public Card(int value, bool stealsVotes)
        {
            this.value = value;
            this.stealsVotes = stealsVotes;
        }
    }
}
