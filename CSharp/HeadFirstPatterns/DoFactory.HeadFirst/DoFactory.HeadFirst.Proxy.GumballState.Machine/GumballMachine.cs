using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace DoFactory.HeadFirst.Proxy.GumballState.Machine
{
    [ServiceContract(Name = "GumballMachine", Namespace = "http://www.mycompany.com/headfirst/2008/07", SessionMode = SessionMode.Required)]
    public interface IGumballMachine
    {
        [OperationContract]
        void StartWithQuarters(int count);

        [OperationContract]
        void InsertQuarter();

        [OperationContract]
        void TurnCrank();

        [OperationContract]
        void EjectQuarter();

        [OperationContract]
        string GetReport();
    }

    public class GumballMachine : IGumballMachine
    {
        private GumballMachineState _state = GumballMachineState.SoldOut;
        private int _count = 0;

        private StringBuilder _log = new StringBuilder();

        public void StartWithQuarters(int count)
        {
            this._count = count;
            if (_count > 0)
            {
                _state = GumballMachineState.NoQuarter;
            }
        }

        public void InsertQuarter()
        {
            if (_state == GumballMachineState.HasQuarter)
            {
                _log.Append("You can't insert another quarter\n");
            }
            else if (_state == GumballMachineState.NoQuarter)
            {
                _state = GumballMachineState.HasQuarter;
                _log.Append("You inserted a quarter\n");
            }
            else if (_state == GumballMachineState.SoldOut)
            {
                _log.Append("You can't insert a quarter, the machine is sold out\n");
            }
            else if (_state == GumballMachineState.Sold)
            {
                _log.Append("Please wait, we're already giving you a gumball\n");
            }
        }

        public void TurnCrank()
        {
            if (_state == GumballMachineState.Sold)
            {
                _log.Append("Turning twice doesn't get you another gumball!\n");
            }
            else if (_state == GumballMachineState.NoQuarter)
            {
                _log.Append("You turned but there's no quarter\n");
            }
            else if (_state == GumballMachineState.SoldOut)
            {
                _log.Append("You turned, but there are no gumballs\n");
            }
            else if (_state == GumballMachineState.HasQuarter)
            {
                _log.Append("You turned...\n");
                _state = GumballMachineState.Sold;
                Dispense();
            }
        }

        public void EjectQuarter()
        {
            if (_state == GumballMachineState.HasQuarter)
            {
                _log.Append("Quarter returned\n");
                _state = GumballMachineState.NoQuarter;
            }
            else if (_state == GumballMachineState.NoQuarter)
            {
                _log.Append("You haven't inserted a quarter\n");
            }
            else if (_state == GumballMachineState.Sold)
            {
                _log.Append("Sorry, you already turned the crank\n");
            }
            else if (_state == GumballMachineState.SoldOut)
            {
                _log.Append("You can't eject, you haven't inserted a quarter yet\n");
            }
        }


        public void Dispense()
        {
            if (_state == GumballMachineState.Sold)
            {
                _log.Append("A gumball comes rolling out the slot\n");
                _count = _count - 1;
                if (_count == 0)
                {
                    _log.Append("Oops, out of gumballs!\n");
                    _state = GumballMachineState.SoldOut;
                }
                else
                {
                    _state = GumballMachineState.NoQuarter;
                }
            }
            else if (_state == GumballMachineState.NoQuarter)
            {
                _log.Append("You need to pay first\n");
            }
            else if (_state == GumballMachineState.SoldOut)
            {
                _log.Append("No gumball dispensed\n");
            }
            else if (_state == GumballMachineState.HasQuarter)
            {
                _log.Append("No gumball dispensed\n");
            }
        }

        public void Refill(int numGumBalls)
        {
            _count = numGumBalls;
            _state = GumballMachineState.NoQuarter;
        }

        public string GetReport()
        {
            StringBuilder result = new StringBuilder();
            result.Append("\nMighty Gumball, Inc.");
            result.Append("\n.NET3.5-enabled Standing Gumball Model #2104\n");
            result.Append("Inventory: " + _count + " gumball");
            if (_count != 1) result.Append("s");

            result.Append("\nMachine is ");
            if (_state == GumballMachineState.SoldOut)
            {
                result.Append("sold out");
            }
            else if (_state == GumballMachineState.NoQuarter)
            {
                result.Append("waiting for quarter");
            }
            else if (_state == GumballMachineState.HasQuarter)
            {
                result.Append("waiting for turn of crank");
            }
            else if (_state == GumballMachineState.Sold)
            {
                result.Append("delivering a gumball");
            }
            result.Append("\n");

            string ret = _log.ToString() + "\n" + result.ToString();
            _log = new StringBuilder();

            return ret.ToString();
        }
    }

    // State enumercation

    public enum GumballMachineState
    {
        SoldOut,
        NoQuarter,
        HasQuarter,
        Sold
    }

}
