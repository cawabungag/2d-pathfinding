using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.BinatyHeap
{
	internal class BinaryHeap<TKey, T> : IBinaryHeap<TKey, T>
		where TKey : IEquatable<TKey>
		where T : IComparable<T>
	{
		private readonly IDictionary<TKey, int> _map;
		private readonly IList<T> _collection;
		private readonly Func<T, TKey> _lookupFunc;

		public BinaryHeap(Func<T, TKey> lookupFunc, int capacity = 0)
		{
			_lookupFunc = lookupFunc;
			_collection = new List<T>(capacity);
			_map = new Dictionary<TKey, int>(capacity);
		}

		public int Count => _collection.Count;

		public void Enqueue(T item)
		{
			_collection.Add(item);
			var i = _collection.Count - 1;
			_map[_lookupFunc(item)] = i;
			while (i > 0)
			{
				var j = (i - 1) / 2;

				if (_collection[i].CompareTo(_collection[j]) > 0)
					break;

				Swap(i, j);
				i = j;
			}
		}

		public T Dequeue()
		{
			if (_collection.Count == 0) return default;

			var result = _collection.First();
			RemoveRoot();
			_map.Remove(_lookupFunc(result));
			return result;
		}

		public void Clear()
		{
			_collection.Clear();
			_map.Clear();
		}

		public bool TryGet(TKey key, out T value)
		{
			if (!_map.TryGetValue(key, out var index))
			{
				value = default;
				return false;
			}

			value = _collection[index];
			return true;
		}

		public void Modify(T value)
		{
			if (!_map.TryGetValue(_lookupFunc(value), out var index))
				throw new KeyNotFoundException(nameof(value));

			_collection[index] = value;
		}

		private void RemoveRoot()
		{
			_collection[0] = _collection.Last();
			_map[_lookupFunc(_collection[0])] = 0;
			_collection.RemoveAt(_collection.Count - 1);

			var i = 0;
			while (true)
			{
				var largest = LargestIndex(i);
				if (largest == i)
					return;

				Swap(i, largest);
				i = largest;
			}
		}

		private void Swap(int i, int j)
		{
			(_collection[i], _collection[j]) = (_collection[j], _collection[i]);
			_map[_lookupFunc(_collection[i])] = i;
			_map[_lookupFunc(_collection[j])] = j;
		}

		private int LargestIndex(int i)
		{
			var leftInd = 2 * i + 1;
			var rightInd = 2 * i + 2;
			var largest = i;

			if (leftInd < _collection.Count && _collection[leftInd].CompareTo(_collection[largest]) < 0) largest = leftInd;

			if (rightInd < _collection.Count && _collection[rightInd].CompareTo(_collection[largest]) < 0) largest = rightInd;

			return largest;
		}
	}
}