﻿using Blazor.Fluxor.DependencyInjection;
using Blazor.Fluxor.Extensions;
using Blazor.Fluxor.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Fluxor
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds support to Blazor for the Fluxor library
		/// </summary>
		/// <param name="serviceCollection">The service collection</param>
		/// <param name="configure">A callback used to configure options</param>
		/// <returns>The service collection</returns>
		/// <example>
		///var serviceProvider = new BrowserServiceProvider(services =&gt;
		///{
		///	services.AddFluxor(options =&gt; options
		///		.UseDependencyInjection(typeof(Program).Assembly)
		///	);
		///});
		///</example>
		public static IServiceCollection AddFluxor(this IServiceCollection serviceCollection, Action<Options> configure)
		{
			if (configure == null)
				throw new ArgumentNullException(nameof(configure));

			// Add the browser interop service
			serviceCollection.AddSingleton<IBrowserInteropService, BrowserInteropPassthrough>();

			// We only use an instance so middleware can create extensions to the Options
			var options = new Options();
			configure(options);

			// Register all middleware types with dependency injection
			foreach (Type middlewareType in Options.MiddlewareTypes)
				serviceCollection.AddSingleton(middlewareType);

			IEnumerable<AssemblyScanSettings> scanWhitelist = Options.MiddlewareTypes
				.Select(t => new AssemblyScanSettings(t.Assembly, t.GetNamespace()));

			// Scan for features and effects
			if (Options.DependencyInjectionEnabled)
			{
				DependencyScanner.Scan(
					serviceCollection: serviceCollection,
					assembliesToScan: Options.DependencyInjectionAssembliesToScan,
					scanWhitelist: scanWhitelist);
				serviceCollection.AddSingleton(typeof(IState<>), typeof(State<>));
			}

			return serviceCollection;
		}

	}
}
