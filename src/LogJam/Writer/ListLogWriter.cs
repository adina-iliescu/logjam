﻿// // --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListLogWriter.cs">
// Copyright (c) 2011-2014 logjam.codeplex.com.  
// </copyright>
// Licensed under the <a href="http://logjam.codeplex.com/license">Apache License, Version 2.0</a>;
// you may not use this file except in compliance with the License.
// --------------------------------------------------------------------------------------------------------------------


namespace LogJam.Writer
{
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;


	/// <summary>
	/// Appends all log entries to a <see cref="List{T}"/>.
	/// </summary>
	public sealed class ListLogWriter<TEntry> : ILogWriter<TEntry>, IEnumerable<TEntry>, IStartable 
		where TEntry : ILogEntry
	{

		private readonly IList<TEntry> _entryList;
		private readonly bool _isSynchronized;
		private bool _isStarted = false;

		/// <summary>
		/// Creates a new <see cref="ListLogWriter{TEntry}"/>.
		/// </summary>
		/// <param name="synchronize">If set to <c>true</c> (the default), writes are synchronized, meaning entries are only added to
		/// the list one thread at a time using a <c>lock</c>.  If <c>false</c>, writes are not synchronized by this class, so another 
		/// mechanism must be used to synchronize writes from multiple threads.</param>
		public ListLogWriter(bool synchronize = true)
		{
			_entryList = new List<TEntry>();
			_isSynchronized = synchronize;
		}

		#region ILogWriter

		/// <summary>
		/// Returns <c>true</c> until the logwriter is disposed.
		/// </summary>
		public bool Enabled { get { return _isStarted; } }

		/// <summary>
		/// Returns <c>true</c> if calls to this object's methods and properties are synchronized.
		/// </summary>
		public bool IsSynchronized { get { return _isSynchronized; } }

		/// <summary>
		/// Adds the <paramref name="entry"/> to the <see cref="List{TEntry}"/>.
		/// </summary>
		/// <param name="entry">A <typeparamref name="TEntry"/>.</param>
		public void Write(ref TEntry entry)
		{
			if (! _isSynchronized)
			{
				if (_isStarted)
				{
					_entryList.Add(entry);
				}
			}
			else
			{
				lock (this)
				{
					if (_isStarted)
					{
						_entryList.Add(entry);
					}
				}
			}
		}

		#endregion
		#region IStartable

		public void Start()
		{
			lock (this)
			{
				_isStarted = true;
			}
		}

		public void Stop()
		{
			lock (this)
			{
				_isStarted = false;
			}
		}

		public bool IsStarted
		{
			get { return _isStarted; }
		}

		#endregion

		public IEnumerator<TEntry> GetEnumerator()
		{
			IEnumerable<TEntry> enumerable;
			if (_isSynchronized)
			{
				lock (this)
				{
					enumerable = _entryList.ToArray();
				}
			}
			else
			{
				enumerable = _entryList.ToArray();
			}
			return enumerable.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		/// <summary>
		/// Returns the number of entries logged to this <see cref="ListLogWriter{TEntry}"/>.
		/// </summary>
		public int Count
		{ get { return _entryList.Count; } }

		/// <summary>
		/// Removes all entries that have been previously logged.
		/// </summary>
		public void Clear()
		{
			lock (this)
			{
				_entryList.Clear();
			}
		}
	}

}