namespace PlayingCardService.CardDeck
{
    // using System.Collections.Generic;
    using System;
    using EventFeed;

    public interface ICardDeck
    {
        Card Draw(IEventStore eventStore);
    }

    public class CardDeck : ICardDeck
    {
        private readonly List<Card> cards = new List<Card>();

        // seeding random with current time to make it random on each run.
        private readonly Random random = new Random();

        public CardDeck()
        {
            // this could have been a data file stored somewhere on the internet
            // and then implement a client to interface between the data and this service
            // but it felt like overengineering for this assignement.
            var suits = new[] { "Clubs", "Diamonds", "Hearts", "Spades" };
            foreach (var suit in suits)
            {
                for (var rank = 1; rank <= 13; rank++)
                {
                    cards.Add(new Card(suit, rank));
                }
            }
            cards.Add(new Card("Joker", 1));
            cards.Add(new Card("Joker", 1));
            cards.Add(new Card("Joker", 1));
        }

        public Card Draw(IEventStore eventStore)
        {
            var index = random.Next(cards.Count);
            var card = cards[index];
            // raise events if some dealer service wants to track the card deck
            eventStore.Raise("CardDrawn", new { Card = card });
            return card;
        }
    }

    public record Card(string Suit, int Rank);
}
