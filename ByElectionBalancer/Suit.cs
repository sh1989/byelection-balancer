using System;

namespace ByElectionBalancer
{
    public class Suit
    {
        private readonly String name;
        public String Name { get { return name; } }

        private readonly int baseValue;
        public int BaseValue { get { return baseValue; } }

        private readonly Card[] cards;
        public Card[] Cards { get { return cards; } }

        public Suit(String name, int baseValue, Card[] cards)
        {
            this.name = name;
            this.baseValue = baseValue;
            this.cards = cards;
        }
    }
}
