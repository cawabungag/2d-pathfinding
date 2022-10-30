using Grid.Tile;
using UnityEngine;

public class TilePresenter : ITilePresenter
{
	private readonly TileView _tileView;
	private static readonly Color NormalColor = Color.white;
	private static readonly Color ObstacleColor = Color.black;
	private static readonly Color StartColor = Color.blue;
	private static readonly Color FinishColor = Color.red;
	private Color SetColor { set => _tileView.Renderer.color = value; }

	public TilePresenter(TileView tileView) => _tileView = tileView;

	public void SetNormal() => SetColor = NormalColor;
	public void SetStart() => SetColor = StartColor;
	public void SetFinish() => SetColor = FinishColor;

	public void SetPath()
	{
		throw new System.NotImplementedException();
	}

	public void SetObstacle() => SetColor = ObstacleColor;
}