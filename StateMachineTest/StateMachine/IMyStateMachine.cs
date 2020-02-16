using Appccelerate.StateMachine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace StateMachineTest.StateMachine
{
    public interface IMyStateMachine<TState, TEvent>
        where TState : IComparable
        where TEvent : IComparable
    {
        void SaveCurrentState(IInitializable<TState> currentStateId);
    }
}
