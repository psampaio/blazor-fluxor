using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Fluxor
{
	public abstract class Middleware : IMiddleware
	{
		private int BeginMiddlewareChangeCount;

		protected IStore Store { get; private set; }
		protected bool IsInsideMiddlewareChange => BeginMiddlewareChangeCount > 0;
		protected virtual void OnInternalMiddlewareChangeEnding() { }

		public virtual string GetClientScripts() => null;
		public virtual void AfterInitializeAllMiddlewares() { }
		public virtual bool MayDispatchAction(IAction action) => true;
		public virtual void BeforeDispatch(IAction action) { }
        public virtual IEnumerable<IAction> AfterDispatch(IAction action) => Enumerable.Empty<IAction>();

		public virtual void Initialize(IStore store)
		{
			Store = store;
		}

		IDisposable IMiddleware.BeginInternalMiddlewareChange()
		{
			BeginMiddlewareChangeCount++;
			return new DisposableCallback(() =>
			{
				if (BeginMiddlewareChangeCount == 1)
					OnInternalMiddlewareChangeEnding();
				BeginMiddlewareChangeCount--;
			});
		}
	}
}
