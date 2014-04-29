﻿// ------------------------------------------------------------------------------------------------------------
// <copyright company="Crim Consulting" file="TracerConfig.cs">
// Copyright (c) 2011-2012 Crim Consulting.  
// </copyright>
// Licensed under the <a href="http://logjam.codeplex.com/license">Apache License, Version 2.0</a>;
// you may not use this file except in compliance with the License.
// ------------------------------------------------------------------------------------------------------------
namespace LogJam.Trace.Config
{
	using LogJam.Trace;
	using LogJam.Util;
	using System;
	using System.Diagnostics.Contracts;

	/// <summary>
	/// A configuration element used to configure all <see cref="Tracer"/>s that match this <see cref="NamePrefix"/>.
	/// </summary>
	/// <remarks>
	/// <c>TracerConfig</c> instances are immutable.
	/// </remarks>
	public class TracerConfig : NamePrefixTreeNode<TracerConfig>
	{
		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TracerConfig"/> class. 
		/// Creates a new <see cref="TracerConfig"/>.
		/// </summary>
		/// <param name="namePrefix">
		/// Matched against <see cref="Tracer.Name"/>s.  The <see cref="TracerConfig"/> that best matches each 
		/// <see cref="Tracer.Name"/> is used to configure that <see cref="Tracer"/>.
		/// </param>
		/// <param name="traceSwitch">
		/// The <see cref="ITraceSwitch"/> instance used for matching <see cref="Tracer"/>s.
		/// </param>
		/// <param name="traceWriter">
		/// The <see cref="ITraceWriter"/> instance used for matching <see cref="Tracer"/>s.
		/// </param>
		public TracerConfig(string namePrefix, ITraceSwitch traceSwitch, ITraceWriter traceWriter)
		{
			Contract.Requires<ArgumentNullException>(namePrefix != null);
			Contract.Requires<ArgumentNullException>(traceSwitch != null);
			Contract.Requires<ArgumentNullException>(traceWriter != null);

			// namePrefix cannot end with a '.' - strip them if present
			namePrefix = namePrefix.TrimEnd('.', ' ');

			NamePrefix = namePrefix;
			TraceSwitch = traceSwitch;
			TraceWriter = traceWriter;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Gets the <see cref="ITraceSwitch"/> instance used for matching <see cref="Tracer"/>s.
		/// </summary>
		/// <value>
		/// The trace switch.
		/// </value>
		public ITraceSwitch TraceSwitch { get; private set; }

		/// <summary>
		/// Gets the <see cref="ITraceWriter"/> instance used for matching <see cref="Tracer"/>s.
		/// </summary>
		/// <value>
		/// The message collector.
		/// </value>
		public ITraceWriter TraceWriter { get; private set; }

		#endregion

		/// <summary>
		/// TODO The copy settings from.
		/// </summary>
		/// <param name="other">
		/// TODO The other.
		/// </param>
		public void CopySettingsFrom(TracerConfig other)
		{
			TraceSwitch = other.TraceSwitch;
			TraceWriter = other.TraceWriter;
		}

	}
}