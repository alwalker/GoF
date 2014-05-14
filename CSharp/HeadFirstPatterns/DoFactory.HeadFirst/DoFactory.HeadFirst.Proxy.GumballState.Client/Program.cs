using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DoFactory.HeadFirst.Proxy.GumballState.Client.GumballProxy;

namespace DoFactory.HeadFirst.Proxy.GumballState.Client
{
    class Program
    {
        static void Main()
        {
            // Create proxy object
            GumballMachineClient proxy = new GumballMachineClient();

            proxy.StartWithQuarters(5);
            proxy.InsertQuarter();
            proxy.TurnCrank();

            Console.WriteLine(proxy.GetReport());

            proxy.InsertQuarter();
            proxy.EjectQuarter();
            proxy.TurnCrank();

            Console.WriteLine(proxy.GetReport());

            proxy.InsertQuarter();
            proxy.TurnCrank();
            proxy.InsertQuarter();
            proxy.TurnCrank();
            proxy.EjectQuarter();

            Console.WriteLine(proxy.GetReport());

            proxy.InsertQuarter();
            proxy.InsertQuarter();
            proxy.TurnCrank();
            proxy.InsertQuarter();
            proxy.TurnCrank();
            proxy.InsertQuarter();
            proxy.TurnCrank();

            Console.WriteLine(proxy.GetReport());

            // Wait for user
            Console.ReadKey();
        }
    }
}
