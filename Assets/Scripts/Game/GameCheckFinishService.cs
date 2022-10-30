using System.Collections.Generic;
using System.Linq;
using Core;
using Factories.Bug;
using States;
using StaticData;

namespace Game
{
	public class GameCheckFinishService : IService
	{
		private readonly IStaticDataService _staticDataService;
		private readonly GameState _gameState;

		public GameCheckFinishService(IStaticDataService staticDataService, GameState gameState)
		{
			_staticDataService = staticDataService;
			_gameState = gameState;
		}

		public void Execute(IReadOnlyCollection<IBugPresenter> bugs)
		{
			if (bugs.Count == 0)
				return;

			var gameRulesData = _staticDataService.GetGameRulesData();
			var finishPosition = gameRulesData.finishPosition;

			if (bugs.Any(bug => bug.Position != finishPosition))
				return;

			_gameState.ExitGame();
		}
	}
}