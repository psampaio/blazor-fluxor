using System;
using Blazor.Fluxor.Services;

namespace Blazor.Fluxor.UnitTests.SupportFiles
{
	public class BrowserInteropStub : IBrowserInteropService
	{
		public event EventHandler PageLoaded;
		public string _ClientScripts;

		public string GetClientScripts()
		{
			return _ClientScripts;
		}

		public void _TriggerPageLoaded()
		{
			PageLoaded?.Invoke(this, EventArgs.Empty);
		}
	}
}
