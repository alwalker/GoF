using System;
using System.Text;

namespace DoFactory.HeadFirst.Command.Party
{
    class RemoteLoader
    {
        static void Main(string[] args)
        {
            RemoteControl remoteControl = new RemoteControl();

            Light light = new Light("Living Room");
            TV tv = new TV("Living Room");
            Stereo stereo = new Stereo("Living Room");
            Hottub hottub = new Hottub();
 
            LightOnCommand lightOn = new LightOnCommand(light);
            StereoOnCommand stereoOn = new StereoOnCommand(stereo);
            TVOnCommand tvOn = new TVOnCommand(tv);
            HottubOnCommand hottubOn = new HottubOnCommand(hottub);
            LightOffCommand lightOff = new LightOffCommand(light);
            StereoOffCommand stereoOff = new StereoOffCommand(stereo);
            TVOffCommand tvOff = new TVOffCommand(tv);
            HottubOffCommand hottubOff = new HottubOffCommand(hottub);

            ICommand[] partyOn = { lightOn, stereoOn, tvOn, hottubOn};
            ICommand[] partyOff = { lightOff, stereoOff, tvOff, hottubOff};
  
            MacroCommand partyOnMacro = new MacroCommand(partyOn);
            MacroCommand partyOffMacro = new MacroCommand(partyOff);
 
            remoteControl.SetCommand(0, partyOnMacro, partyOffMacro);
  
            Console.WriteLine(remoteControl);
            Console.WriteLine("--- Pushing Macro On---");
            remoteControl.OnButtonWasPushed(0);
            Console.WriteLine("\n--- Pushing Macro Off---");
            remoteControl.OffButtonWasPushed(0);

            // Wait for user
            Console.ReadKey();
        }    
    }

    #region Remote Control

    public class RemoteControl 
    {
        private ICommand[] _onCommands;
        private ICommand[] _offCommands;
        private ICommand _undoCommand;
 
        // Constructor
        public RemoteControl() 
        {
            _onCommands  = new ICommand[7];
            _offCommands = new ICommand[7];
 
            ICommand noCommand = new NoCommand();
            for (int i = 0; i < 7 ;i++) 
            {
                _onCommands[i]  = noCommand;
                _offCommands[i] = noCommand;
            }
            _undoCommand = noCommand;
        }
  
        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand) 
        {
            _onCommands[slot]  = onCommand;
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
            StringBuilder sb = new StringBuilder();
            sb.Append("\n------ Remote Control -------\n");
            for (int i = 0; i < _onCommands.Length; i++) 
            {
                sb.Append("[slot " + i + "] " + _onCommands[i].GetType().Name
                    + "    " + _offCommands[i].GetType().Name + "\n");
            }
            sb.Append("[undo] " + _undoCommand.GetType().Name + "\n");
            return sb.ToString();
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

    public class MacroCommand : ICommand 
    {
        private ICommand[] _commands;
 
        public MacroCommand(ICommand[] commands) 
        {
            this._commands = commands;
        }
 
        public void Execute() 
        {
            for (int i = 0; i < _commands.Length; i++) 
            {
                _commands[i].Execute();
            }
        }
 
        public void Undo() 
        {
            for (int i = 0; i < _commands.Length; i++) 
            {
                _commands[i].Undo();
            }
        }
    }

    public class TVOnCommand : ICommand 
    {
        private TV _tv;

        public TVOnCommand(TV tv) 
        {
            this._tv = tv;
        }

        public void Execute() 
        {
            _tv.On();
            _tv.SetInputChannel();
        }

        public void Undo() 
        {
            _tv.Off();
        }
    }

    public class TVOffCommand : ICommand 
    {
        private TV _tv;

        public TVOffCommand(TV tv) 
        {
            this._tv= tv;
        }

        public void Execute() 
        {
            _tv.Off();
        }

        public void Undo() 
        {
            _tv.On();
        }
    }

    public class StereoOnCommand : ICommand 
    {
        private Stereo _stereo;

        public StereoOnCommand(Stereo stereo) 
        {
            this._stereo = stereo;
        }

        public void Execute() 
        {
            _stereo.On();
        }

        public void Undo() 
        {
            _stereo.Off();
        }
    }

    public class StereoOffCommand : ICommand 
    {
        private Stereo _stereo;
 
        public StereoOffCommand(Stereo stereo) 
        {
            this._stereo = stereo;
        }
 
        public void Execute() 
        {
            _stereo.Off();
        }

        public void Undo() 
        {
            _stereo.On();
        }
    }

    public class StereoOnWithCDCommand : ICommand 
    {
        private Stereo _stereo;
 
        public StereoOnWithCDCommand(Stereo stereo) 
        {
            this._stereo = stereo;
        }
 
        public void Execute() 
        {
            _stereo.On();
            _stereo.SetCD();
            _stereo.SetVolume(11);
        }

        public void Undo() 
        {
            _stereo.Off();
        }
    }

    public class LivingroomLightOnCommand : ICommand 
    {
        private Light _light;

        public LivingroomLightOnCommand(Light light) 
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

    public class LivingroomLightOffCommand : ICommand 
    {
        private Light _light;

        public LivingroomLightOffCommand(Light light) 
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

    public class HottubOffCommand : ICommand 
    {
        private Hottub _hottub;

        public HottubOffCommand(Hottub hottub) 
        {
            this._hottub = hottub;
        }

        public void Execute() 
        {
            _hottub.SetTemperature(98);
            _hottub.Off();
        }

        public void Undo() 
        {
            _hottub.On();
        }
    }

    public class CeilingFanOffCommand : ICommand 
    {
        private CeilingFan _ceilingFan;
        private CeilingFanSpeed _prevSpeed;

        public CeilingFanOffCommand(CeilingFan ceilingFan) 
        {
            this._ceilingFan = ceilingFan;
        }

        public void Execute() 
        {
            _prevSpeed = _ceilingFan.Speed;
            _ceilingFan.Off();
        }

        public void Undo() 
        {
            switch (_prevSpeed) 
            {
                case CeilingFanSpeed.High: _ceilingFan.high(); break;
                case CeilingFanSpeed.Medium: _ceilingFan.medium(); break;
                case CeilingFanSpeed.Low: _ceilingFan.low(); break;
                case CeilingFanSpeed.Off: _ceilingFan.Off(); break;
            }
        }
    }

    public class CeilingFanMediumCommand : ICommand 
    {
        private CeilingFan _ceilingFan;
        private CeilingFanSpeed _prevSpeed;

        public CeilingFanMediumCommand(CeilingFan ceilingFan) 
        {
            this._ceilingFan = ceilingFan;
        }

        public void Execute() 
        {
            _prevSpeed = _ceilingFan.Speed;
            _ceilingFan.medium();
        }

        public void Undo() 
        {
            switch (_prevSpeed) 
            {
                case CeilingFanSpeed.High: _ceilingFan.high(); break;
                case CeilingFanSpeed.Medium: _ceilingFan.medium(); break;
                case CeilingFanSpeed.Low: _ceilingFan.low(); break;
                case CeilingFanSpeed.Off: _ceilingFan.Off(); break;
            }
        }
    }

    public class CeilingFanHighCommand : ICommand 
    {
        private CeilingFan _ceilingFan;
        private CeilingFanSpeed _prevSpeed;

        public CeilingFanHighCommand(CeilingFan ceilingFan) 
        {
            this._ceilingFan = ceilingFan;
        }

        public void Execute() 
        {
            _prevSpeed = _ceilingFan.Speed;
            _ceilingFan.high();
        }

        public void Undo() 
        {
            switch (_prevSpeed) 
            {
                case CeilingFanSpeed.High: _ceilingFan.high(); break;
                case CeilingFanSpeed.Medium: _ceilingFan.medium(); break;
                case CeilingFanSpeed.Low: _ceilingFan.low(); break;
                case CeilingFanSpeed.Off: _ceilingFan.Off(); break;
            }
        }
    }

    public class HottubOnCommand : ICommand 
    {
        private Hottub _hottub;

        public HottubOnCommand(Hottub hottub) 
        {
            this._hottub = hottub;
        }

        public void Execute() 
        {
            _hottub.On();
            _hottub.SetTemperature(104);
            _hottub.Circulate();
        }

        public void Undo() 
        {
            _hottub.Off();
        }
    }

    #endregion

    #region TV, Tub, CeilingFan, etc

    public class Hottub 
    {
        private bool _on;
        private int _temperature;

        public void On() 
        {
            _on = true;
        }

        public void Off() 
        {
            _on = false;
        }

        public void Circulate() 
        {
            if (_on) 
            {
                Console.WriteLine("Hottub is bubbling!");
            }
        }

        public void JetsOn() 
        {
            if (_on) 
            {
                Console.WriteLine("Hottub jets are on");
            }
        }

        public void JetsOff() 
        {
            if (_on) 
            {
                Console.WriteLine("Hottub jets are off");
            }
        }

        public void SetTemperature(int temperature) 
        {
            if (temperature > this._temperature) 
            {
                Console.WriteLine("Hottub is heating to a steaming " + temperature + " degrees");
            }
            else 
            {
                Console.WriteLine("Hottub is cooling to " + temperature + " degrees");
            }
            this._temperature = temperature;
        }
    }


    public class TV 
    {
        private string _location;
        private int _channel;

        public TV(string location) 
        {
            this._location = location;
        }

        public void On() 
        {
            Console.WriteLine(_location + " TV is on");
        }

        public void Off() 
        {
            Console.WriteLine(_location + " TV is off");
        }

        public void SetInputChannel() 
        {
            this._channel = 3;
            Console.WriteLine(_location + " TV channel " + _channel + " is set for DVD");
        }
    }

    public class Stereo 
    {
        private string _location;

        public Stereo(string location) 
        {
            this._location = location;
        }

        public void On() 
        {
            Console.WriteLine(_location + " stereo is on");
        }

        public void Off() 
        {
            Console.WriteLine(_location + " stereo is off");
        }

        public void SetCD() 
        {
            Console.WriteLine(_location + " stereo is set for CD input");
        }

        public void setDVD() 
        {
            Console.WriteLine(_location + " stereo is set for DVD input");
        }

        public void SetRadio() 
        {
            Console.WriteLine(_location + " stereo is set for Radio");
        }

        public void SetVolume(int volume) 
        {
            // code to set the volume
            // valid range: 1-11 (after all 11 is better than 10, right?)
            Console.WriteLine(_location + " Stereo volume set to " + volume);
        }
    }

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
            this.Level = level;
            if (Level == 0) 
            {
                Off();
            }
            else 
            {
                Console.WriteLine("Light is dimmed to " + level + "%");
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
        }
  
        public void high() 
        {
            // turns the ceiling fan on to high
            Speed = CeilingFanSpeed.High;
            Console.WriteLine(_location + " ceiling fan is on high");
        } 

        public void medium() 
        {
            // turns the ceiling fan on to medium
            Speed = CeilingFanSpeed.Medium;
            Console.WriteLine(_location + " ceiling fan is on medium");
        }

        public void low() 
        {
            // turns the ceiling fan on to low
            Speed = CeilingFanSpeed.Low;
            Console.WriteLine(_location + " ceiling fan is on low");
        }
 
        public void Off() 
        {
            // turns the ceiling fan off
            Speed = CeilingFanSpeed.Off;
            Console.WriteLine(_location + " ceiling fan is off");
        }
 
        public CeilingFanSpeed Speed{ get; private set; }
        
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
