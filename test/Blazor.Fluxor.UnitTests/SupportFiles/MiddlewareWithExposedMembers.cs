using System;

namespace Blazor.Fluxor.UnitTests.SupportFiles
{
	public class MiddlewareWithExposedMembers : Middleware
	{
		public int _BeginMiddlewareChangeCount => BeginMiddlewareChangeCount;
		public bool _IsInsideMiddlewareChange => IsInsideMiddlewareChange;
		public IDisposable _BeginInternalMiddlewareChange() =>
			((IMiddleware)this).BeginInternalMiddlewareChange();
	}
}
