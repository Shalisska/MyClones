using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Infrastructure;
using Appccelerate.StateMachine.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachineTest.StateMachine
{
    public class StateMachineSaver<TState, TEvent> : IStateMachineSaver<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        public StateMachineSaver(IMyStateMachine<TState, TEvent> stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private IMyStateMachine<TState, TEvent> _stateMachine;

        public void SaveCurrentState(IInitializable<TState> currentStateId)
        {
            _stateMachine.SaveCurrentState(currentStateId);
        }

        public void SaveHistoryStates(IReadOnlyDictionary<TState, TState> historyStates)
        {
            //throw new NotImplementedException();
            return;
        }

        public void SaveEvents(IReadOnlyCollection<EventInformation<TEvent>> events)
        {
            //throw new NotImplementedException();
            return;
        }
    }
}
