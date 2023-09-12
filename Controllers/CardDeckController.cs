namespace PlayingCardService.CardDeck
{
    using Microsoft.AspNetCore.Mvc;
    using PlayingCardService.EventFeed;

    [ApiController]
    [Route("/cards")]
    public class CardDeckController : ControllerBase
    {
        private readonly ICardDeck cardDeck;
        private readonly IEventStore eventStore;

        public CardDeckController(ICardDeck cardDeck, IEventStore eventStore)
        {
            this.cardDeck = cardDeck;
            this.eventStore = eventStore;
        }

        [HttpGet("draw")]
        public Card Draw()
        {
            return cardDeck.Draw(this.eventStore);
        }
    }
}
