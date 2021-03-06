﻿using System.Threading.Tasks;

namespace Blazor.Fluxor
{
	public abstract class Effect<TAction> : IEffect<TAction>
	  where TAction : IAction
	{
		public abstract Task<IAction[]> HandleAsync(TAction action);

		Task<IAction[]> IEffect.HandleAsync(IAction action)
		{
			return HandleAsync((TAction)action);
		}
	}
}
