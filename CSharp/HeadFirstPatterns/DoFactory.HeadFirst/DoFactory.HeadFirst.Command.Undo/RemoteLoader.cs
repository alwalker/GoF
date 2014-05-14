using System;
using System.Text;

namespace DoFactory.HeadFirst.Command.Undo
{
    class RemoteLoader
    {
        static void Main(string[] args)
        {
            RemoteControlWithUndo remoteControl = new RemoteControlWithUndo();
 
            Light livingRoomLight = new Light("Living Room");
 
            LightOnCommand livingRoomLightOn = new LightOnCommand(livingRoomLight);
            LightOffCommand livingRoomLightOff = new LightOffCommand(livingRoomLight);
 
            remoteControl.SetCommand(0, livingRoomLightOn, livingRoomLightOff);
 
            remoteControl.OnButtonWasPushed(0);
            remoteControl.OffButtonWasPushed(0);

            Console.WriteLine(remoteControl);

            remoteControl.UndoButtonWasPushed();
            remoteControl.OffButtonWasPushed(0);
            remoteControl.OnButtonWasPushed(0);

            Console.WriteLine(remoteControl);

            remoteControl.UndoButtonWasPushed();

            CeilingFan ceilingFan = new CeilingFan("Living Room");
   
            CeilingFanMediumCommand ceilingFanMedium = new CeilingFanMediumCommand(ceilingFan);
            CeilingFanHighCommand ceilingFanHigh = new CeilingFanHighCommand(ceilingFan);
            CeilingFanOffCommand ceilingFanOff = new CeilingFanOffCommand(ceilingFan);
  
            remoteControl.SetCommand(0, ceilingFanMedium, ceilingFanOff);
            remoteControl.SetCommand(1, ceilingFanHigh, ceilingFanOff);
   
            remoteControl.OnButtonWasPushed(0);
            remoteControl.OffButtonWasPushed(0);

            Console.WriteLine(remoteControl);

            remoteControl.UndoButtonWasPushed();
            remoteControl.OnButtonWasPushed(1);

            Console.WriteLine(remoteControl);

            remoteControl.UndoButtonWasPushed();

            // Wait for user
            Console.ReadKey();
        }    
    }

    #region Remote Control

    public class RemoteControlWithUndo 
    {
        private ICommand[] _onCommands;
        private ICommand[] _offCommands;
        private ICommand _undoCommand;
 
        // Constructor
        public RemoteControlWithUndo() 
        {
            _onCommands = new ICommand[7];
            _offCommands = new ICommand[7];
 
            ICommand noCommand = new NoCommand();
            for (int i = 0; i < 7 ;i++) 
            {
                _onCommands [i] = noCommand;
                _offCommands[i] = noCommand;
            }
            _undoCommand = noCommand;
        }
  
        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand) 
        {
            _onCommands [slot] = onCommand;
            _offCommands[slot] = offCommand;
        }
 
        public void OnButtonWasPushed(int slot) 
        {
            _onCommands[slot].Execute();
            _undoCommand = _onCommands[slot];
        }
 
        public void OffButtonWasPushed(int slot) 
        {
            _offCommands[slot].Execute();
            _undoCommand = _offCommands[slot];
        }
 
        public void UndoButtonWasPushed() 
        {
            _undoCommand.Undo();
        }
  
        public override string ToString() 
        {
            StringBuilder stringBuff = new StringBuilder();
            stringBuff.Append("\n------ Remote Control -------\n");
            for (int i = 0; i < _onCommands.Length; i++) 
            {
                stringBuff.Append("[slot " + i + "] " + _onCommands[i].GetType().Name
                    + "    " + _offCommands[i].GetType().Name + "\n");
            }
            stringBuff.Append("[undo] " + _undoCommand.GetType().Name + "\n");
            return stringBuff.ToString();
        }
    }

    #endregion

    #region Commands

    public interface ICommand 
    {
        void Execute();
        void Undo();
    }

    public class NoCommand : ICommand 
    {
        public void Execute() { }
        public void Undo() { }
    }

    public class LightOnCommand : ICommand 
    {
        private Light _light;
 
        public LightOnCommand(Light light) 
        {
            this._light = light;
        }
 
        public void Execute() 
        {
            _light.On();
        }
 
        public void Undo() 
        {
            _light.Off();
        }
    }

    public class LightOffCommand : ICommand 
    {
        private Light _light;
 
        public LightOffCommand(Light light) 
        {
            this._light = light;
        }
 
        public void Execute() 
        {
            _light.Off();
        }
 
        public void Undo() 
        {
            _light.On();
        }
    }

    public class DimmerLightOnCommand : ICommand 
    {
        private Light _light;
        private int _previousLevel;

        public DimmerLightOnCommand(Light light) 
        {
            this._light = light;
        }

        public void Execute() 
        {
            _previousLevel = _light.Level;
            _light.Dim(75);
        }

        public void Undo() 
        {
            _light.Dim(_previousLevel);
        }
    }

    public class DimmerLightOffCommand : ICommand 
    {
        private Light _light;
        private int _previousLevel = 100;

        public DimmerLightOffCommand(Light light) 
        {
            this._light = light;
        }

        public void Execute() 
        {
            _previousLevel = _light.Level;
            _light.Off();
        }

        public void Undo() 
        {
            _light.Dim(_previousLevel);
        }
    }

    public class CeilingFanOffCommand : ICommand 
    {
        private CeilingFan _ceilingFan;
        private CeilingFanSpeed _previousSpeed;
  
        public CeilingFanOffCommand(CeilingFan ceilingFan) 
        {
            this._ceilingFan = ceilingFan;
        }
 
        public void Execute() 
        {
            _previousSpeed = _ceilingFan.Speed;
            _ceilingFan.Off();
        }
 
        public void Undo() 
        {
            switch(_previousSpeed)
            {
                case CeilingFanSpeed.High:
                    _ceilingFan.High();
                    break;
                case CeilingFanSpeed.Medium:
                    _ceilingFan.Medium();
                    break;
                case CeilingFanSpeed.Low:
                    _ceilingFan.Low();
                    break;
                case CeilingFanSpeed.Off:
                    _ceilingFan.Off();
                    break;
            }
        }
    }

    public class CeilingFanMediumCommand : ICommand 
    {
        private CeilingFan _ceilingFan;
        private CeilingFanSpeed _previousSpeed;
  
        public CeilingFanMediumCommand(CeilingFan ceilingFan) 
        {
            this._ceilingFan = ceilingFan;
        }
 
        public void Execute() 
        {
            _previousSpeed = _ceilingFan.Speed;
            _ceilingFan.Medium();
        }
 
        public void Undo() 
        {
            switch(_previousSpeed)
            {
                case CeilingFanSpeed.High:
                    _ceilingFan.High();
                    break;
                case CeilingFanSpeed.Medium:
                    _ceilingFan.Medium();
                    break;
                case CeilingFanSpeed.Low:
                    _ceilingFan.Low();
                    break;
                case CeilingFanSpeed.Off:
                    _ceilingFan.Off();
                    break;
            }
        }
    }

    public class CeilingFanLowCommand : ICommand 
    {
        private CeilingFan _ceilingFan;
        private CeilingFanSpeed _previousSpeed;
  
        public CeilingFanLowCommand(CeilingFan ceilingFan) 
        {
            this._ceilingFan = ceilingFan;
        }
 
        public void Execute() 
        {
            _previousSpeed = _ceilingFan.Speed;
            _ceilingFan.Low();
        }
 
        public void Undo() 
        {
            switch(_previousSpeed)
            {
                case CeilingFanSpeed.High:
                    _ceilingFan.High();
                    break;
                case CeilingFanSpeed.Medium:
                    _ceilingFan.Medium();
                    break;
                case CeilingFanSpeed.Low:
                    _ceilingFan.Low();
                    break;
                case CeilingFanSpeed.Off:
                    _ceilingFan.Off();
                    break;
            }
        }
    }

    public class CeilingFanHighCommand : ICommand 
    {
        private CeilingFan _ceilingFan;
        private CeilingFanSpeed _previousSpeed;
  
        public CeilingFanHighCommand(CeilingFan ceilingFan) 
        {
            this._ceilingFan = ceilingFan;
        }
 
        public void Execute() 
        {
            _previousSpeed = _ceilingFan.Speed;
            _ceilingFan.High();
        }
 
        public void Undo() 
        {
            switch(_previousSpeed)
            {
                case CeilingFanSpeed.High:
                    _ceilingFan.High();
                    break;
                case CeilingFanSpeed.Medium:
                    _ceilingFan.Medium();
                    break;
                case CeilingFanSpeed.Low:
                    _ceilingFan.Low();
                    break;
                case CeilingFanSpeed.Off:
                    _ceilingFan.Off();
                    break;
            }
        }
    }

    #endregion

    #region Light and CeilingFan

    public class Light 
    {
        private string _location;

        public Light(string location) 
        {
            this._location = location;
        }

        public void On() 
        {
            Level = 100;
            Console.WriteLine("Light is on");
        }

        public void Off() 
        {
            Level = 0;
            Console.WriteLine("Light is off");
        }

        public void Dim(int level) 
        {
            Level = level;
            if (Level == 0) 
            {
                Off();
            }
            else 
            {
                Console.WriteLine("Light is dimmed to " + Level + "%");
            }
        }

        public int Level { get; private set; }
    }

    public class CeilingFan 
    {
        private string _location;
 
        public CeilingFan(string location) 
        {
            this._location = location;
            Speed = CeilingFanSpeed.Off;
        }
  
        public void High() 
        {
            Speed = CeilingFanSpeed.High;
            Console.WriteLine(_location + " ceiling fan is on high");
        } 
 
        public void Medium() 
        {
            Speed = CeilingFanSpeed.Medium;
            Console.WriteLine(_location + " ceiling fan is on medium");
        }
 
        public void Low() 
        {
            Speed = CeilingFanSpeed.Low;
            Console.WriteLine(_location + " ceiling fan is on low");
        }
  
        public void Off() 
        {
            Speed = CeilingFanSpeed.Off;
            Console.WriteLine(_location + " ceiling fan is off");
        }
  
        public CeilingFanSpeed Speed { get; private set; }
    }

    public enum CeilingFanSpeed
    {
        High,
        Medium,
        Low,
        Off
    }
    #endregion
}
