using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Infrastructure;
using Appccelerate.StateMachine.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachineTest.StateMachine
{
    public class StateMachineLoader<TState, TEvent> : IStateMachineLoader<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        public IInitializable<TState> LoadCurrentState()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<EventInformation<TEvent>> LoadEvents()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyDictionary<TState, TState> LoadHistoryStates()
        {
            throw new NotImplementedException();
        }
    }
}
