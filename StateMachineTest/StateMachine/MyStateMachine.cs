using Stateless;
using Stateless.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace StateMachineTest.StateMachine
{
    public class MyStateMachine
    {
        public void StartProcess(States state)
        {
            var machine = GetStateMachine(state);

            machine.Fire(Events.StartProcess);
            machine.Fire(Events.StartStages);
            machine.Fire(Events.EndStage);
            var state1 = machine.State;
            machine.Fire(Events.EndStage);
            var info = machine.GetInfo();

            machine.Fire(Events.EndStage);


            machine.Fire(Events.EndStages);

            //_myProcess.Start();
            //_myProcess.Save(this);
            //Thread.Sleep(1000);
            //_myProcess.Fire(Events.EndStage);
            //Thread.Sleep(1000);
            //_myProcess.Fire(Events.EndStage);
            //_myProcess.Stop();
            //var definition1 = GetStateMachineDefinition();
            //_myProcess = definition1.CreatePassiveStateMachine();
            //Thread.Sleep(1000);
            //Console.WriteLine("Load");
            //_myProcess.Load(this);
            //_myProcess.Start();
            //_myProcess.Fire(Events.EndStage);
            //_myProcess.Fire(Events.EndStages);
            //_myProcess.Save(this);
            //var r = 1;
        }

        private States EndStage(StateMachine<States, Events> machine)
        {
            var r = machine.CanFire(Events.EndStage);

            machine.Fire(Events.EndStage);
            var stage = machine.State;

            return stage;
        }

        private Dictionary<States, bool> _innerStates = new Dictionary<States, bool>
        {
            {States.State1, false },
            {States.State2, true },
            {States.State3, false }
        };

        private StateMachine<States, Events> GetStateMachine(States state)
        {
            var machine = new StateMachine<States, Events>(state);

            machine.Configure(States.State1)
                .SubstateOf(States.Active)
                .OnEntry(() => MessageOnStart(machine.State))
                .OnExit(() => MessageOnEnd(machine.State))
                .Permit(Events.EndStage, States.State2);

            machine.Configure(States.State2)
                .SubstateOf(States.Active)
                .OnEntry((t) => MessageOnStart(t.Destination, t.Source, t.Trigger))
                .OnExit((t) => MessageOnEnd(t.Destination, t.Source, t.Trigger))
                .Permit(Events.EndStage, States.State3);

            machine.Configure(States.State3)
                .SubstateOf(States.Active)
                .OnEntry((t) => MessageOnStart(t.Destination, t.Source, t.Trigger))
                .OnExit(() => MessageOnEnd(machine.State))
                .Permit(Events.EndStage, States.Active);

            machine.Configure(States.Ready)
                .OnEntry((t) => MessageOnStart(t.Destination, t.Source, t.Trigger))
                .OnExit((t) => MessageOnEnd(t.Destination, t.Source, t.Trigger))
                .Permit(Events.StartProcess, States.Active);

            machine.Configure(States.Active)
                .OnEntry(() => { MessageOnStart(machine.State); })
                .OnExit(() => { MessageOnStopProcess(machine.State); })
                .Permit(Events.StartStages, States.State1)
                .Permit(Events.EndStages, States.Ready);

            return machine;
        }

        private void MessageOnStart(States dest, States source, Events trigger)
        {
            Console.WriteLine($"Start from {source} to {dest} via {trigger}");
        }

        private void MessageOnEnd(States dest, States source, Events trigger)
        {
            Console.WriteLine($"End from {source} to {dest} via {trigger}");
        }

        private void MessageOnStart(States states)
        {
            Console.WriteLine($"Start {states}");
        }

        private void MessageOnEnd(States states)
        {
            Console.WriteLine($"End {states}");
        }

        private void MessageOnStopProcess(States states)
        {
            MessageOnEnd(states);
            Console.WriteLine($"Stop Process");
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
        StartStages,
        EndStages,
        EndStage,
        Confirmed
    }
}
