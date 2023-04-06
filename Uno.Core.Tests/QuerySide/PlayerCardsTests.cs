using System.Collections.Generic;
using Uno.Core.CommandSide;
using Uno.Core.CommandSide.Events;
using Uno.Core.QuerySide;
using Xunit;

namespace Uno.Core.Tests.QuerySide;

public class PlayerCardsTests
{
    [Fact]
    public void When_Receive_CardsDealt_Then_This_Cards_Dealt_Is_Added_In_Player_Cardss()
    {
        var repository = new List<PlayerCards>();
        var eventHandler = new PlayerCardsEventHandler(repository);

        CardsDealt cardsDealt = new(new GameId("1"),
                                    new PlayerId("1"),
                                    new List<Card> {
                                        new Card(CardColor.Red, new CardNumber(0)),
                                        new Card(CardColor.Red, new CardNumber(1)),
                                        new Card(CardColor.Red, new CardNumber(2)),
                                        new Card(CardColor.Red, new CardNumber(3)),
                                        new Card(CardColor.Red, new CardNumber(4)),
                                        new Card(CardColor.Red, new CardNumber(5)),
                                        new Card(CardColor.Red, new CardNumber(6)),
                                    });

        eventHandler.Handle(cardsDealt);

        Assert.Single(repository);
    }
}