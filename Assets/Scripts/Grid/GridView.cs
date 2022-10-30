// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using Utils;
//
// namespace Grid
// {
// 	public class GridView : MonoBehaviour
// 	{
// 		void GenerateGrid() {
// 			foreach (var rVector2Int in _result)
// 			{
// 				// var asdasdasd = GetTileAtPosition(rVector2Int);
// 				// asdasdasd.SetPath();
// 			}
// 		}
// 		
//  
// 		public int currentWayPoint; 
// 		Vector2Int targetWayPoint;
//  
// 		public float speed = 1f;
// 		private IReadOnlyCollection<Vector2Int> _result;
//
// 		private void Update()
// 		{
// 			if (_result != null)
// 			{
// 				Update(_result.Reverse().ToArray());
// 			}
// 		}
//
// 		void Update (Vector2Int[] wayPointList) 
// 		{
// 			// check if we have somewere to walk
// 			if(currentWayPoint <= wayPointList.Length)
// 			{
// 				var asd = targetWayPoint.ToVector3();
// 				_bug.Rotator.Rotate(asd.normalized);
// 				// rotate towards the target
// 				transform.position = Vector3.MoveTowards(_bug.transform.position, asd, speed * Time.deltaTime);
// 				// _bug.Mover.Move(asd, speed);
// 				// move towards the target
// 				Debug.LogError($"Move: {asd}");
// 				
// 				if(_bug.transform.position == asd)
// 				{
// 					currentWayPoint++;
// 					targetWayPoint = wayPointList[currentWayPoint];
// 				}
// 			}
// 		}
//
// 		// private Tile GetTileAtPosition(Vector2Int pos) {
// 		// 	if (_tiles.TryGetValue(pos, out var tile)) return tile;
// 		// 	return null;
// 		// }
// 	}
// }