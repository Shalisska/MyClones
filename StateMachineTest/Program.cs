using StateMachineTest.StateMachine;
using System;

namespace StateMachineTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var fsm = new MyStateMachine();
            fsm.StartProcess(States.Ready);


            Console.ReadKey();
        }
    }
}
