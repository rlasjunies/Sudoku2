using Fluxor.Extensions;
using Fluxor.UnsupportedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fluxor
{
    /// <see cref="IFeature{TState}"/>
    public abstract class Feature<TState> : IFeature<TState>
    {
        /// <see cref="IFeature.MaximumStateChangedNotificationsPerSecond"/>
        public byte MaximumStateChangedNotificationsPerSecond { get; set; }

        public event EventHandler<Exceptions.UnhandledExceptionEventArgs> UnhandledException;

        /// <see cref="IFeature.GetName"/>
        public abstract string GetName();

        /// <see cref="IFeature.GetState"/>
        public virtual object GetState() => State;

        /// <see cref="IFeature.RestoreState(object)"/>
        public virtual void RestoreState(object value) => State = (TState)value;

        /// <see cref="IFeature.GetStateType"/>
        public virtual Type GetStateType() => typeof(TState);

        /// <summary>
        /// Gets the initial state for the feature
        /// </summary>
        /// <returns>The initial state</returns>
        protected abstract TState GetInitialState();

        /// <summary>
        /// A list of reducers registered with this feature
        /// </summary>
        protected readonly List<IReducer<TState>> Reducers = new List<IReducer<TState>>();

        private SpinLock SpinLock = new SpinLock();
        private ThrottledInvoker TriggerStateChangedCallbacksThrottler;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        public Feature()
        {
            TriggerStateChangedCallbacksThrottler = new ThrottledInvoker(TriggerStateChangedCallbacks);
            State = GetInitialState();
        }

        private EventHandler untypedStateChanged;
        event EventHandler IFeature.StateChanged
        {
            add
            {
                SpinLock.ExecuteLocked(() => untypedStateChanged += value);
            }

            remove
            {
                SpinLock.ExecuteLocked(() => untypedStateChanged -= value);
            }
        }

        private TState _State;

        private EventHandler<TState> stateChanged;
        /// <summary>
        /// Event that is executed whenever the state changes
        /// </summary>
        public event EventHandler<TState> StateChanged
        {
            add
            {
                SpinLock.ExecuteLocked(() => stateChanged += value);
            }

            remove
            {
                SpinLock.ExecuteLocked(() => stateChanged -= value);
            }
        }

        /// <see cref="IFeature{TState}.State"/>
        public virtual TState State
        {
            get => _State;
            protected set
            {
                SpinLock.ExecuteLocked(() =>
                {
                    bool stateHasChanged = !Object.ReferenceEquals(_State, value);
                    _State = value;
                    if (stateHasChanged)
                        TriggerStateChangedCallbacksThrottler.Invoke(MaximumStateChangedNotificationsPerSecond);
                });
            }
        }

        /// <see cref="IFeature{TState}.AddReducer(IReducer{TState})"/>
        public virtual void AddReducer(IReducer<TState> reducer)
        {
            if (reducer == null)
                throw new ArgumentNullException(nameof(reducer));
            Reducers.Add(reducer);
        }

        /// <see cref="IFeature.ReceiveDispatchNotificationFromStore(object)"/>
        public virtual void ReceiveDispatchNotificationFromStore(object action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            IEnumerable<IReducer<TState>> applicableReducers = Reducers.Where(x => x.ShouldReduceStateForAction(action));
            TState newState = State;
            foreach (IReducer<TState> currentReducer in applicableReducers)
            {
                newState = currentReducer.Reduce(newState, action);
            }
            State = newState;
        }

        public void TriggerEffectsWithState(object action, List<IEffectWithState> EffectWithStates, IDispatcher dispatcher)
        {
            var recordedExceptions = new List<Exception>();
            var effectsWithStateToExecute = EffectWithStates
                .Where(x => x.ShouldReactToAction(action))
                .ToArray();

            var executedEffects = new List<Task>();
            TState newState = State;

            Action<Exception> collectExceptions = e =>
            {
                if (e is AggregateException aggregateException)
                    recordedExceptions.AddRange(aggregateException.Flatten().InnerExceptions);
                else
                    recordedExceptions.Add(e);
            };

            foreach (IEffectWithState effect in effectsWithStateToExecute)
            {
                try
                {
                    executedEffects.Add(effect.HandleAsyncWithState(action, newState, dispatcher));
                }
                catch (Exception e)
                {
                    collectExceptions(e);
                }
            }
            Task.Run(async () =>
            {
                try
                {
                    await Task.WhenAll(executedEffects);
                }
                catch (Exception e)
                {
                    collectExceptions(e);
                }

                // Let the UI decide if it wishes to deal with any unhandled exceptions.
                // By default it should throw the exception if it is not handled.
                foreach (Exception exception in recordedExceptions)
                    UnhandledException?.Invoke(this, new Exceptions.UnhandledExceptionEventArgs(exception));
            });
        }


        private void TriggerStateChangedCallbacks()
        {
            stateChanged?.Invoke(this, State);
            untypedStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
