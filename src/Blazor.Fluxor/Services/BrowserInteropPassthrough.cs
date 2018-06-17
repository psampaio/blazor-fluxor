using System;

namespace Blazor.Fluxor.Services
{
	public class BrowserInteropPassthrough : IBrowserInteropService
	{
		public event EventHandler PageLoaded
		{
			add => BrowserInterop.PageLoaded += value;
			remove => BrowserInterop.PageLoaded -= value;
		}

		public string GetClientScripts()
		{
			return BrowserInterop.GetClientScripts();
		}
	}
}
