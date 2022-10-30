using Core;
using Pathfinding;

namespace Game
{
	public class GameCalculatePathService : IService
	{
		private readonly IPathfindingService _pathfindingService;

		public GameCalculatePathService(IPathfindingService pathfindingService)
		{
			_pathfindingService = pathfindingService;
		}

		public void Execute()
		{
			
		}
	}
}