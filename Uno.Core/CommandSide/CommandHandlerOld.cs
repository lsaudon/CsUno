//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Uno.Core.CommandSide.Commands;
//using Uno.Core.CommandSide.Events;

//namespace Uno.Core.CommandSide
//{
//	public class CommandHandlerOld
//	{
//		private readonly IRepository<Game> _repository;

//		public CommandHandlerOld(IRepository<Game> repository)
//		{
//			_repository = repository;
//		}

//		public void Handle(CreateGame command)
//		{
//			var game = new Game(command.Id, command.Name, new List<Card>(), new List<Card>(), new List<Player>(), default, false);

//			_repository.Save(game, new GameCreated(game.Name));
//		}

//		public void Handle(JoinGame command)
//		{
//			var game = _repository.GetById(command.GameId);

//			game.Players.Add(new Player(command.PlayerName, new List<Card>()));
//			_repository.Save(game, new PlayerJoined(command.PlayerName));
//		}

//		public void Handle(PlayCard command)
//		{
//			var game = _repository.GetById(command.GameId);

//			var player = game.Players.FirstOrDefault(p => p.Name == command.PlayerName);

//			if (player == null)
//			{
//				throw new InvalidOperationException("Player not found.");
//			}

//			var card = player.Hand.FirstOrDefault(c => c.Color == command.Color && c.Value == command.Value);

//			if (card == null)
//			{
//				throw new InvalidOperationException("Card not found in player's hand.");
//			}

//			player.Hand.Remove(card);
//			game.DiscardPile.Add(card);

//			_repository.Save(game, new CardPlayed(card));

//			// Check for game over condition
//			if (player.Hand.Count == 0)
//			{
//				_repository.Save(game, new GameOver(player.Name));
//			}
//			else
//			{
//				// Change turn
//				var currentPlayerIndex = game.Players.IndexOf(game.CurrentPlayer);
//				var nextPlayerIndex = (currentPlayerIndex + 1) % game.Players.Count;
//				game.CurrentPlayer = game.Players[nextPlayerIndex];

//				_repository.Save(game, new PlayerTurnChanged(game.CurrentPlayer.Name));
//			}
//		}
//	}
//}