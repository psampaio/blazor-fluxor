using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Fluxor
{
	public abstract class Feature<TState> : IFeature<TState>
	{
		public abstract string GetName();
		public virtual TState State { get; protected set; }
		public virtual object GetState() => State;
		public virtual void RestoreState(object value) => State = (TState)value;
		public virtual Type GetStateType() => typeof(TState);

		protected abstract TState GetInitialState();
		protected readonly Dictionary<Type, List<object>> ReducersByActionType = new Dictionary<Type, List<Object>>();

	    protected Feature()
		{
			State = GetInitialState();
		}

		public virtual void AddReducer<TAction>(IReducer<TState, TAction> reducer)
		{
			if (reducer == null)
				throw new ArgumentNullException(nameof(reducer));

			var actionType = typeof(TAction);
			if (!ReducersByActionType.TryGetValue(actionType, out List<object> reducers))
			{
				reducers = new List<object>();
				ReducersByActionType[actionType] = reducers;
			}
			reducers.Add(reducer);
		}

		public virtual void ReceiveDispatchNotificationFromStore(IAction action)
		{
			if (action == null)
				throw new ArgumentNullException(nameof(action));

			var reducers = GetReducersForAction(action);
		    
			TState newState = State;
			if (reducers != null)
			{
				foreach (var reducerInvocation in reducers)
				{
				    var methodInfo = reducerInvocation.Type
				        .GetMethod("Reduce");
				    newState = (TState) methodInfo.Invoke(reducerInvocation.Reducer, new object[] { newState, action });
				}
			}
			State = newState;
		}

		private IEnumerable<ReducerInvocation> GetReducersForAction(IAction action)
		{
		    var actionType = action.GetType();
		    var targetTypes = new List<Type> {actionType};
            targetTypes.AddRange(actionType.GetInterfaces().Where(t => t != typeof(IAction)));

		    foreach (var type in targetTypes)
		    {
		        ReducersByActionType.TryGetValue(type, out List<object> reducers);
		        if (reducers != null)
		        {
		            var newType = typeof(IReducer<,>).MakeGenericType(typeof(TState), type);
		            foreach (var reducer in reducers)
		            {
		                yield return new ReducerInvocation
		                {
                            Reducer = reducer,
                            Type = newType
		                };
		            }

		            break;
		        }
		    }
		}

	    private class ReducerInvocation
	    {
	        public object Reducer { get; set; }
	        public Type Type { get; set; }
	    }

	}
}
