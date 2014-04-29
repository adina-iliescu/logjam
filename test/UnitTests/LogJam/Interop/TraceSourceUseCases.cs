﻿// ------------------------------------------------------------------------------------------------------------
// <copyright company="Crim Consulting" file="TraceSourceUseCases.cs">
// Copyright (c) 2011-2012 Crim Consulting.  
// </copyright>
// Licensed under the <a href="http://logjam.codeplex.com/license">Apache License, Version 2.0</a>;
// you may not use this file except in compliance with the License.
// ------------------------------------------------------------------------------------------------------------
namespace LogJam.UnitTests.Interop
{
	using System.Configuration;
	using System.Diagnostics;

	/// <summary>
	/// Exercise <see cref="TraceSource"/> use cases, which are used both to compare <c>LogJam.Tracer</c> to <c>TraceSource</c>,
	/// and to test forwarding of <c>TraceSource</c> messages.
	/// </summary>
	public class TraceSourceUseCases
	{
		#region Fields

		private readonly TraceSource _traceSource;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="TraceSourceUseCases"/> class.
		/// </summary>
		public TraceSourceUseCases()
		{
			//ConfigurationManager.OpenMappedExeConfiguration()
			_traceSource = new TraceSource(GetType().FullName, SourceLevels.Information);
		}

		#endregion
	}
}