using System.Collections.Generic;
using Bug;
using Core.Services;
using States;
using StaticData;

namespace Game
{
	public class GameCheckFinishService : IService
	{
		private readonly IStaticDataService _staticDataService;
		private readonly GameState _gameState;
		private const float CLOSE_DISTANCE = 0.01f;

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

			foreach (var bug in bugs)
			{
				var deltaPos = finishPosition -  bug.Position;
				if (deltaPos.sqrMagnitude > CLOSE_DISTANCE)
					return;
			}

			_gameState.ExitGame();
		}
	}
}