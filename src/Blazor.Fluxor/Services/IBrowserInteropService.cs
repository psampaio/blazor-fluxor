using System;

namespace Blazor.Fluxor.Services
{
	public interface IBrowserInteropService
	{
		/// <summary>
		/// <see cref="BrowserInterop.PageLoaded"/>
		/// </summary>
		event EventHandler PageLoaded;
		/// <summary>
		/// <see cref="BrowserInterop.GetClientScripts"/>
		/// </summary>
		/// <returns></returns>
		string GetClientScripts();
	}
}
