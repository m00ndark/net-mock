using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetMock.Utils
{
	public class ProtectedList<T> : IList<T>
	{
		private readonly List<T> _list = new List<T>();
		private readonly object _lock = new object();

		public int Count { get { lock (_lock) return _list.Count; } }

		bool ICollection<T>.IsReadOnly => false;

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<T> GetEnumerator()
		{
			lock (_lock)
				return _list.ToList().GetEnumerator();
		}

		public void ForEach(Action<T> action)
		{
			foreach (T item in this) // enumerating this will operate on a copy
				action(item);
		}

		public T this[int index]
		{
			get { lock (_lock) return _list[index]; }
			set { lock (_lock) _list[index] = value; }
		}

		public int IndexOf(T item)
		{
			lock (_lock)
				return _list.IndexOf(item);
		}

		public bool Contains(T item)
		{
			lock (_lock)
				return _list.Contains(item);
		}

		public void Clear()
		{
			lock (_lock)
				_list.Clear();
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			lock (_lock)
				_list.CopyTo(array, arrayIndex);
		}

		public void Add(T item)
		{
			lock (_lock)
				_list.Add(item);
		}

		public void AddRange(IEnumerable<T> collection)
		{
			lock (_lock)
				_list.AddRange(collection);
		}

		public void Insert(int index, T item)
		{
			lock (_lock)
				_list.Insert(index, item);
		}

		public bool Remove(T item)
		{
			lock (_lock)
				return _list.Remove(item);
		}

		public void RemoveAt(int index)
		{
			lock (_lock)
				_list.RemoveAt(index);
		}
	}
}
