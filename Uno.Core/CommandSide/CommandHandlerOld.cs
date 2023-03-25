// public class Card
// {
//     public int Number { get; set; }
//     public CardColor Color { get; set; }
//     public bool IsActionCard { get; set; }
//     public bool IsWildCard { get; set; }
//     public bool IsWildDrawFourCard { get; set; }
// }

// public enum CardColor
// {
//     Red,
//     Yellow,
//     Green,
//     Blue
// }

// public enum GameState
// {
//     NotStarted,
//     InProgress,
//     Ended
// }

// public enum GameDirection
// {
//     Clockwise,
//     Counterclockwise
// }

// public class CardPlayed : Event
// {
//     public Guid PlayerId { get; set; }
//     public Card Card { get; set; }
// }

// public class PlayerDrewCard : Event
// {
//     public Guid PlayerId { get; set; }
// }

// public class GameEnded : Event
// {
//     public Guid WinningPlayerId { get; set; }
// }

// public class Game : Aggregate
// {
//     private List<Guid> _playerIds = new List<Guid>();
//     private List<Card> _deck = new List<Card>();
//     private List<Card> _discardPile = new List<Card>();
//     private Guid _currentPlayerId;
//     private GameDirection _direction;
//     private GameState _state;

//     public void PlayCard(Guid playerId, Card card)
//     {
//         if (_state != GameState.InProgress)
//         {
//             throw new InvalidOperationException("Cannot play card when game is not in progress.");
//         }

//         if (playerId != _currentPlayerId)
//         {
//             throw new ArgumentException("Player cannot play card out of turn.");
//         }

//         if (!CanPlayCard(card))
//         {
//             throw new ArgumentException("Cannot play this card.");
//         }

//         RemoveCardFromPlayer(playerId, card);
//         AddCardToDiscardPile(card);

//         AddEvent(new CardPlayed { AggregateId = Id, PlayerId = playerId, Card = card });

//         if (HasPlayerWon(playerId))
//         {
//             AddEvent(new GameEnded { AggregateId = Id, WinningPlayerId = playerId });
//             _state = GameState.Ended;
//         }
//         else
//         {
//             MoveToNextPlayer();
//         }
//     }

//     public void DrawCard(Guid playerId)
//     {
//         if (_state != GameState.InProgress)
//         {
//             throw new InvalidOperationException("Cannot draw card when game is not in progress.");
//         }

//         if (playerId != _currentPlayerId)
//         {
//             throw new ArgumentException("Player cannot draw card out of turn.");
//         }

//         var card = DrawCard();

//         AddCardToPlayer(playerId, card);

//         AddEvent(new PlayerDrewCard { AggregateId = Id, PlayerId = playerId });
//     }

//     private void AddCardToDiscardPile(Card card)
//     {
//         _discardPile.Add(card);
//     }

//     private void AddCardToPlayer(Guid playerId, Card card)
//     {
//         throw new NotImplementedException();
//     }

//     private void RemoveCardFromPlayer(Guid playerId, Card card)
//     {
//         throw new NotImplementedException();
//     }

//     private Card DrawCard()
//     {
//         throw new NotImplementedException();
//     }

//     private bool CanPlayCard(Card card)
//     {
//         throw new NotImplementedException();
//     }

//     private bool HasPlayerWon(Guid playerId)
//     {
//         throw new NotImplementedException();
//     }

//     private void MoveToNextPlayer()
//     {
//         throw new NotImplementedException();
//     }
// }

// public class Program
// {
//     public static void Main()
//     {
//         var commandHandler = new CommandHandler();

//         commandHandler.RegisterHandler<CreateGame>(command =>
//         {
//             var game = new Game();
//             game.Id = command.AggregateId;
//             game.CreateGame(command.NumberOfPlayers);
//         });

//         commandHandler.RegisterHandler<JoinGame>(command =>
//         {
//             var game = (Game)commandHandler.GetAggregate(command.AggregateId);
//             game.JoinGame(command.PlayerId, command.PlayerName);
//         });

//         commandHandler.RegisterHandler<StartGame>(command =>
//         {
//             var game = (Game)commandHandler.GetAggregate(command.AggregateId);
//             game.StartGame(command.FirstPlayerId);
//         });

//         commandHandler.RegisterHandler<PlayCard>(command =>
//         {
//             var game = (Game)commandHandler.GetAggregate(command.AggregateId);
//             game.PlayCard(command.PlayerId, command.Card);
//         });

//         commandHandler.RegisterHandler<DrawCard>(command =>
//         {
//             var game = (Game)commandHandler.GetAggregate(command.AggregateId);
//             game.DrawCard(command.PlayerId);
//         });

//         var gameId = Guid.NewGuid();

//         var createGameCommand = new CreateGame { AggregateId = gameId, NumberOfPlayers = 2 };
//         commandHandler.HandleCommand(createGameCommand);

//         var joinGameCommand1 = new JoinGame { AggregateId = gameId, PlayerId = Guid.NewGuid(), PlayerName = "Alice" };
//         commandHandler.HandleCommand(joinGameCommand1);

//         var joinGameCommand2 = new JoinGame { AggregateId = gameId, PlayerId = Guid.NewGuid(), PlayerName = "Bob" };
//         commandHandler.HandleCommand(joinGameCommand2);

//         var startGameCommand = new StartGame { AggregateId = gameId, FirstPlayerId = joinGameCommand1.PlayerId };
//         commandHandler.HandleCommand(startGameCommand);

//         var playCardCommand = new PlayCard { AggregateId = gameId, PlayerId = joinGameCommand1.PlayerId, Card = new Card { Number = 7, Color = CardColor.Red } };
//         commandHandler.HandleCommand(playCardCommand);

//         var drawCardCommand = new DrawCard { AggregateId = gameId, PlayerId = joinGameCommand2.PlayerId };
//         commandHandler.HandleCommand(drawCardCommand);
//     }
// }
