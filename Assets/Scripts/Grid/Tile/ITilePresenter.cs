namespace Grid.Tile
{
	public interface ITilePresenter
	{
		void SetNormal();
		void SetStart();
		void SetFinish();
		void SetPath();
		void SetObstacle();
	}
}