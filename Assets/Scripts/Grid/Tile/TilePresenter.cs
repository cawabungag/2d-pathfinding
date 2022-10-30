using Grid.Tile;
using UnityEngine;

public class TilePresenter : ITilePresenter
{
	private readonly TileView _tileView;
	
	private static readonly Color NormalColor = Color.white;
	private static readonly Color ObstacleColor = Color.black;
	private static readonly Color StartColor = Color.blue;
	private static readonly Color FinishColor = Color.red;
	private static readonly Color PathColor = Color.green;

	private bool _isMarkerTile;
	
	private Color SetColor { set => _tileView.Renderer.color = value; }

	public TilePresenter(TileView tileView) => _tileView = tileView;

	
	public void SetStart()
	{
		SetColor = StartColor;
		_isMarkerTile = true;
	}

	public void SetFinish()
	{
		SetColor = FinishColor;
		_isMarkerTile = true;
	}

	public void SetNormal()
	{
		if (_isMarkerTile)
			return;
		
		SetColor = NormalColor;
	}

	public void SetPath()
	{
		if (_isMarkerTile)
			return;

		SetColor = PathColor;
	}

	public void SetObstacle()
	{
		if (_isMarkerTile)
			return;
		
		SetColor = ObstacleColor;
	}
}