using System;
using System.Collections.Generic;
using AI.A_Star;
using UnityEngine;

/// <summary>
/// Reusable A* path finder.
/// </summary>
public class Path : IPath
{
	private const int MAX_NEIGHBOURS = 8;
	private readonly PathNode[] _neighbours = new PathNode[MAX_NEIGHBOURS];

	private readonly int _maxSteps;
	private readonly IBinaryHeap<Vector2Int, PathNode> _frontier;
	private readonly HashSet<Vector2Int> _ignoredPositions;
	private readonly List<Vector2Int> _output;
	private readonly IDictionary<Vector2Int, Vector2Int> _links;

	/// <summary>
	/// Creation of new path finder.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public Path(int maxSteps = int.MaxValue, int initialCapacity = 0)
	{
		if (maxSteps <= 0) throw new ArgumentOutOfRangeException(nameof(maxSteps));
		if (initialCapacity < 0) throw new ArgumentOutOfRangeException(nameof(initialCapacity));

		_maxSteps = maxSteps;
		_frontier = new BinaryHeap<Vector2Int, PathNode>(a => a.Position, initialCapacity);
		_ignoredPositions = new HashSet<Vector2Int>(initialCapacity);
		_output = new List<Vector2Int>(initialCapacity);
		_links = new Dictionary<Vector2Int, Vector2Int>(initialCapacity);
	}

	/// <summary>
	/// Calculate a new path between two points.
	/// </summary>
	/// <exception cref="ArgumentNullException"></exception>
	public bool Calculate(Vector2Int start, Vector2Int target, IReadOnlyCollection<Vector2Int> obstacles,
		out IReadOnlyCollection<Vector2Int> path)
	{
		if (obstacles == null) throw new ArgumentNullException(nameof(obstacles));

		if (!GenerateNodes(start, target, obstacles))
		{
			path = Array.Empty<Vector2Int>();
			return false;
		}

		_output.Clear();
		_output.Add(target);

		while (_links.TryGetValue(target, out target)) _output.Add(target);
		path = _output;
		return true;
	}

	private bool GenerateNodes(Vector2Int start, Vector2Int target, IReadOnlyCollection<Vector2Int> obstacles)
	{
		_frontier.Clear();
		_ignoredPositions.Clear();
		_links.Clear();

		_frontier.Enqueue(new PathNode(start, target, 0));
		_ignoredPositions.UnionWith(obstacles);
		var step = 0;
		while (_frontier.Count > 0 && step++ <= _maxSteps)
		{
			var current = _frontier.Dequeue();
			_ignoredPositions.Add(current.Position);

			if (current.Position.Equals(target)) return true;

			GenerateFrontierNodes(current, target);
		}

		// All nodes analyzed - no path detected.
		return false;
	}

	private void GenerateFrontierNodes(PathNode parent, Vector2Int target)
	{
		_neighbours.Fill(parent, target);
		foreach (var newNode in _neighbours)
		{
			// Position is already checked or occupied by an obstacle.
			if (_ignoredPositions.Contains(newNode.Position)) continue;

			// Node is not present in queue.
			if (!_frontier.TryGet(newNode.Position, out var existingNode))
			{
				_frontier.Enqueue(newNode);
				_links[newNode.Position] = parent.Position;
			}

			// Node is present in queue and new optimal path is detected.
			else if (newNode.TraverseDistance < existingNode.TraverseDistance)
			{
				_frontier.Modify(newNode);
				_links[newNode.Position] = parent.Position;
			}
		}
	}
}