using UnityEngine;

namespace Grid.Tile
{
	public class TileView : MonoBehaviour
	{
		[SerializeField] 
		private SpriteRenderer _renderer;
		public SpriteRenderer Renderer => _renderer;
	}
}