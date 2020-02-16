using Appccelerate.StateMachine;
using Appccelerate.StateMachine.Infrastructure;
using Appccelerate.StateMachine.Machine;
using Appccelerate.StateMachine.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StateMachineTest.StateMachine
{
    public class MyStateMachine : IStateMachineSaver<States, Events>, IStateMachineLoader<States, Events>
    {
        public MyStateMachine()
        {
            var definition = GetStateMachineDefinition();
            _myProcess = definition.CreatePassiveStateMachine("Process");
            //_saver = new StateMachineSaver<States, Events>(this);
        }

        public void StartProcess()
        {
            _myProcess.Start();
            Thread.Sleep(1000);
            _myProcess.Fire(Events.EndStage);
            Thread.Sleep(1000);
            _myProcess.Fire(Events.EndStage);
            _myProcess.Save(this);
            _myProcess.Stop();
            var definition1 = GetStateMachineDefinition();
            _myProcess = definition1.CreatePassiveStateMachine("Process");
            Thread.Sleep(1000);
            Console.WriteLine("Load");
            _myProcess.Load(this);
            _myProcess.Start();
            _myProcess.Fire(Events.EndStage);
            _myProcess.Fire(Events.EndStages);
        }

        private PassiveStateMachine<States, Events> _myProcess;

        private IInitializable<States> _initializable;
        private IReadOnlyDictionary<States, States> _historyStates;
        private IReadOnlyCollection<EventInformation<Events>> _events;

        private Dictionary<States, bool> _innerStates = new Dictionary<States, bool>
        {
            {States.State1, false },
            {States.State2, true },
            {States.State3, false }
        };

        public void SaveCurrentState(IInitializable<States> currentStateId)
        {
            _initializable = currentStateId;
        }

        private StateMachineDefinition<States, Events> GetStateMachineDefinition()
        {
            var builder = new StateMachineDefinitionBuilder<States, Events>();

            builder.In(States.State1)
                .ExecuteOnEntry(() => MessageOnStart(States.State1))
                .ExecuteOnExit(() => MessageOnEnd(States.State1))
                .On(Events.EndStage).Goto(States.State2);

            builder.In(States.State2)
                .ExecuteOnEntry(() => MessageOnStart(States.State2))
                .ExecuteOnExit(() => MessageOnEnd(States.State2))
                .On(Events.EndStage).Goto(States.State3);

            builder.In(States.State3)
                .ExecuteOnEntry(() => MessageOnStart(States.State3))
                .On(Events.EndStage).Execute(() => { MessageOnEnd(States.State3); });

            builder.In(States.Ready)
                .ExecuteOnEntry(() => MessageOnStart(States.Ready))
                .ExecuteOnExit(() => MessageOnEnd(States.Ready))
                .On(Events.StartProcess).Goto(States.Active);

            builder.In(States.Active)
                .ExecuteOnEntry(() => MessageOnStart(States.Active))
                .On(Events.StartProcess).Goto(States.State1)
                .On(Events.EndStages).Execute(() => MessageOnStopProcess(States.Active));

            builder.DefineHierarchyOn(States.Active)
                .WithHistoryType(HistoryType.Shallow)
                .WithInitialSubState(States.State1)
                .WithSubState(States.State2)
                .WithSubState(States.State3);

            builder.WithInitialState(States.Active);

            var definition = builder.Build();

            return definition;
        }

        private void MessageOnStart(States states)
        {
            Console.WriteLine($"Start {states}");
            //_myProcess.Save(this);
            var currState = _initializable;
            var hist = _historyStates;
            var ev = _events;
            var r = 1;
        }

        private void MessageOnEnd(States states)
        {
            Console.WriteLine($"End {states}");
            //_myProcess.Save(this);
        }

        private void MessageOnStopProcess(States states)
        {
            MessageOnEnd(states);
            Console.WriteLine($"Stop Process");
            _myProcess.Stop();
        }

        public void SaveHistoryStates(IReadOnlyDictionary<States, States> historyStates)
        {
            _historyStates = historyStates;
        }

        public void SaveEvents(IReadOnlyCollection<EventInformation<Events>> events)
        {
            _events = events;
        }

        public IInitializable<States> LoadCurrentState()
        {
            return _initializable;
        }

        public IReadOnlyDictionary<States, States> LoadHistoryStates()
        {
            return _historyStates;
        }

        public IReadOnlyCollection<EventInformation<Events>> LoadEvents()
        {
            return _events;
        }
    }

    public enum States
    {
        Active,
        Ready,
        State1,
        State2,
        State3,
        WaitConfirm
    }

    public enum Events
    {
        StartProcess,
        EndStages,
        EndStage,
        Confirmed
    }
}
