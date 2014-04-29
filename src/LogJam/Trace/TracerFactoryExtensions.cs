﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TracerFactoryExtensions.cs" company="Crim Consulting">
// Copyright (c) 2011-2014 Crim Consulting.  
// </copyright>
// Licensed under the <a href="http://logjam.codeplex.com/license">Apache License, Version 2.0</a>;
// you may not use this file except in compliance with the License.
// --------------------------------------------------------------------------------------------------------------------

namespace LogJam.Trace
{
	using System;
	using System.Diagnostics.Contracts;


	/// <summary>
	/// Extension methods for <see cref="ITracerFactory"/>.
	/// </summary>
	public static class TracerFactoryExtensions
	{

		public static Tracer GetTracer(this ITracerFactory tracerFactory, Type type)
		{
			Contract.Requires<ArgumentNullException>(tracerFactory != null);
			Contract.Requires<ArgumentNullException>(type != null);

			// Convert generic types to their generic type definition - so the same
			// Tracer is used for ArrayList<T> regardless of the type parameter T.
			if (type.IsGenericType)
			{
				type = type.GetGenericTypeDefinition();
			}

			return tracerFactory.GetTracer(type.FullName);
		}

		public static Tracer TracerFor(this ITracerFactory tracerFactory, object traceSource)
		{
			Contract.Requires<ArgumentNullException>(tracerFactory != null);
			Contract.Requires<ArgumentNullException>(traceSource != null);

			return tracerFactory.GetTracer(traceSource.GetType().FullName);
		}

		public static Tracer TracerFor<T>(this ITracerFactory tracerFactory)
		{
			Contract.Requires<ArgumentNullException>(tracerFactory != null);

			return tracerFactory.GetTracer(typeof(T).FullName);
		}

	}

}
