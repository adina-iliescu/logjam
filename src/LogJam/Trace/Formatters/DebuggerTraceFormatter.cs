﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DebuggerTraceFormatter.cs">
// Copyright (c) 2011-2014 logjam.codeplex.com.  
// </copyright>
// Licensed under the <a href="http://logjam.codeplex.com/license">Apache License, Version 2.0</a>;
// you may not use this file except in compliance with the License.
// --------------------------------------------------------------------------------------------------------------------

namespace LogJam.Trace.Formatters
{
	using System;
	using System.Diagnostics.Contracts;
	using System.Text;

	using LogJam.Trace.Util;


	/// <summary>
	/// The debugger trace formatter.
	/// </summary>
	public class DebuggerTraceFormatter : ITraceFormatter
	{
		#region Fields

		private TimeZoneInfo _outputTimeZone = TimeZoneInfo.Local;

		#endregion

		#region Public Properties

		public bool IncludeTimestamp { get; set; }

		public TimeZoneInfo OutputTimeZone
		{
			get { return _outputTimeZone; }
			set
			{
				Contract.Requires<ArgumentNullException>(value != null);

				_outputTimeZone = value;
			}
		}

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Formats the trace message for debugger windows
		/// </summary>
		/// <param name="timestampUtc"></param>
		/// <param name="tracerName">
		/// The tracer name.
		/// </param>
		/// <param name="traceLevel">
		/// The trace level.
		/// </param>
		/// <param name="message">
		/// The message.
		/// </param>
		/// <param name="details">
		/// Trace message details, like an exception.
		/// </param>
		/// <returns>
		/// The <see cref="string"/>.
		/// </returns>
		public string FormatTrace(DateTime timestampUtc, string tracerName, TraceLevel traceLevel, string message, object details)
		{
			StringBuilder sb = new StringBuilder(255);
			int indentSpaces = 0;

			//if (TraceManager.Config.ActivityTracingEnabled)
			//{
			//	// Compute indent spaces based on current ActivityRecord scope
			//}

			sb.Append(' ', indentSpaces);

			sb.AppendFormat("{0,-7}\t", traceLevel);

			if (IncludeTimestamp)
			{
#if PORTABLE
				DateTime outputTime = TimeZoneInfo.ConvertTime(timestampUtc, _outputTimeZone);
#else
				DateTime outputTime = TimeZoneInfo.ConvertTimeFromUtc(timestampUtc, _outputTimeZone);
#endif
				// TODO: Implement own formatting to make this more efficient
				sb.Append(outputTime.ToString("HH:mm:ss.fff\t"));
			}

			sb.AppendFormat("{0,-50}     {1}", tracerName, message);
			sb.EnsureEndsWith(Environment.NewLine);

			if (details != null)
			{
				sb.AppendIndentLines(details.ToString(), indentSpaces);
				sb.EnsureEndsWith(Environment.NewLine);
			}

			return sb.ToString();
		}

		#endregion
	}
}