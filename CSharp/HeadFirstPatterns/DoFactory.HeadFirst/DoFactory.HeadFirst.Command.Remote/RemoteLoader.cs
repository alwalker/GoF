using System;
using System.Text;

namespace DoFactory.HeadFirst.Command.Remote
{
    class RemoteLoader
    {
        static void Main(string[] args)
        {
            RemoteControl remoteControl = new RemoteControl();

            Light livingRoomLight = new Light("Living Room");
            Light kitchenLight = new Light("Kitchen");
            CeilingFan ceilingFan = new CeilingFan("Living Room");
            GarageDoor garageDoor = new GarageDoor("");
            Stereo stereo = new Stereo("Living Room");

            LightOnCommand livingRoomLightOn =
                new LightOnCommand(livingRoomLight);

            LightOffCommand livingRoomLightOff =
                new LightOffCommand(livingRoomLight);

            LightOnCommand kitchenLightOn =
                new LightOnCommand(kitchenLight);

            LightOffCommand kitchenLightOff =
                new LightOffCommand(kitchenLight);

            CeilingFanOnCommand ceilingFanOn =
                new CeilingFanOnCommand(ceilingFan);

            CeilingFanOffCommand ceilingFanOff =
                new CeilingFanOffCommand(ceilingFan);

            GarageDoorUpCommand garageDoorUp =
                new GarageDoorUpCommand(garageDoor);

            GarageDoorDownCommand garageDoorDown =
                new GarageDoorDownCommand(garageDoor);

            StereoOnWithCDCommand stereoOnWithCD =
                new StereoOnWithCDCommand(stereo);

            StereoOffCommand stereoOff =
                new StereoOffCommand(stereo);

            remoteControl.SetCommand(0, livingRoomLightOn, livingRoomLightOff);
            remoteControl.SetCommand(1, kitchenLightOn, kitchenLightOff);
            remoteControl.SetCommand(2, ceilingFanOn, ceilingFanOff);
            remoteControl.SetCommand(3, stereoOnWithCD, stereoOff);

            Console.WriteLine(remoteControl);

            remoteControl.OnButtonWasPushed(0);
            remoteControl.OffButtonWasPushed(0);
            remoteControl.OnButtonWasPushed(1);
            remoteControl.OffButtonWasPushed(1);
            remoteControl.OnButtonWasPushed(2);
            remoteControl.OffButtonWasPushed(2);
            remoteControl.OnButtonWasPushed(3);
            remoteControl.OffButtonWasPushed(3);

            // Wait for user
            Console.ReadKey();
        }
    }

    #region RemoteControl

    // This is the invoker

    public class RemoteControl
    {
        private ICommand[] _onCommands;
        private ICommand[] _offCommands;

        public RemoteControl()
        {
            _onCommands = new ICommand[7];
            _offCommands = new ICommand[7];

            ICommand noCommand = new NoCommand();
            for (int i = 0; i < 7; i++)
            {
                _onCommands[i] = noCommand;
                _offCommands[i] = noCommand;
            }
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            _onCommands[slot] = onCommand;
            _offCommands[slot] = offCommand;
        }

        public void OnButtonWasPushed(int slot)
        {
            _onCommands[slot].Execute();
        }

        public void OffButtonWasPushed(int slot)
        {
            _offCommands[slot].Execute();
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

            return sb.ToString();
        }
    }
    #endregion

    #region Commands

    public interface ICommand
    {
        void Execute();
    }

    public class NoCommand : ICommand
    {
        public void Execute() { }
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
    }
    public class GarageDoorUpCommand : ICommand
    {
        private GarageDoor _garageDoor;

        public GarageDoorUpCommand(GarageDoor garageDoor)
        {
            this._garageDoor = garageDoor;
        }

        public void Execute()
        {
            _garageDoor.up();
        }
    }

    public class GarageDoorDownCommand : ICommand
    {
        private GarageDoor _garageDoor;

        public GarageDoorDownCommand(GarageDoor garageDoor)
        {
            this._garageDoor = garageDoor;
        }

        public void Execute()
        {
            _garageDoor.up();
        }
    }

    public class CeilingFanOnCommand : ICommand
    {
        private CeilingFan _ceilingFan;

        public CeilingFanOnCommand(CeilingFan ceilingFan)
        {
            this._ceilingFan = ceilingFan;
        }
        public void Execute()
        {
            _ceilingFan.High();
        }
    }

    public class CeilingFanOffCommand : ICommand
    {
        private CeilingFan _ceilingFan;

        public CeilingFanOffCommand(CeilingFan ceilingFan)
        {
            this._ceilingFan = ceilingFan;
        }
        public void Execute()
        {
            _ceilingFan.Off();
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
            _hottub.Heat();
            _hottub.BubblesOn();
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
            _hottub.Cool();
            _hottub.Off();
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
    }

    #endregion

    #region Light, TV, GarageDoor, CeilingFan, etc

    public class Light
    {
        private string _location;

        public Light(string location)
        {
            this._location = location;
        }

        public void On()
        {
            Console.WriteLine(_location + " light is on");
        }

        public void Off()
        {
            Console.WriteLine(_location + " light is off");
        }
    }

    public class GarageDoor
    {
        private string _location;

        public GarageDoor(string location)
        {
            this._location = location;
        }

        public void up()
        {
            Console.WriteLine(_location + " garage Door is Up");
        }

        public void down()
        {
            Console.WriteLine(_location + " garage Door is Down");
        }

        public void stop()
        {
            Console.WriteLine(_location + " garage Door is Stopped");
        }

        public void lightOn()
        {
            Console.WriteLine(_location + " garage light is on");
        }

        public void lightOff()
        {
            Console.WriteLine(_location + " garage light is off");
        }
    }

    public class CeilingFan
    {
        private string _location = "";
        private int _level;

        public static readonly int HIGH = 2;
        public static readonly int MEDIUM = 1;
        public static readonly int LOW = 0;

        public CeilingFan(string location)
        {
            this._location = location;
        }

        public void High()
        {
            // turns the ceiling fan on to high
            _level = HIGH;
            Console.WriteLine(_location + " ceiling fan is on high");
        }

        public void Medium()
        {
            // turns the ceiling fan on to medium
            _level = MEDIUM;
            Console.WriteLine(_location + " ceiling fan is on medium");
        }

        public void Low()
        {
            // turns the ceiling fan on to low
            _level = LOW;
            Console.WriteLine(_location + " ceiling fan is on low");
        }

        public void Off()
        {
            // turns the ceiling fan off
            _level = 0;
            Console.WriteLine(_location + " ceiling fan is off");
        }

        public int getSpeed()
        {
            return _level;
        }
    }

    public class Hottub
    {
        private bool _on;
        private int _temperature;

        public Hottub()
        {
        }

        public void On()
        {
            _on = true;
        }

        public void Off()
        {
            _on = false;
        }

        public void BubblesOn()
        {
            if (_on)
            {
                Console.WriteLine("Hottub is bubbling!");
            }
        }

        public void BubblesOff()
        {
            if (_on)
            {
                Console.WriteLine("Hottub is not bubbling");
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
            this._temperature = temperature;
        }

        public void Heat()
        {
            _temperature = 105;
            Console.WriteLine("Hottub is heating to a steaming 105 degrees");
        }

        public void Cool()
        {
            _temperature = 98;
            Console.WriteLine("Hottub is cooling to 98 degrees");
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
            Console.WriteLine("TV is on");
        }

        public void Off()
        {
            Console.WriteLine("TV is off");
        }

        public void SetInputChannel()
        {
            this._channel = 3;
            Console.WriteLine("Channel " + _channel + " is set for VCR");
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

        public void SetDVD()
        {
            Console.WriteLine(_location + " stereo is set for DVD input");
        }

        public void SetRadio()
        {
            Console.WriteLine(_location + " stereo is set for Radio");
        }

        public void SetVolume(int volume)
        {
            // Code to set the volume
            // Valid range: 1-11 (after all 11 is better than 10, right?)
            Console.WriteLine(_location + " Stereo volume set to " + volume);
        }
    }
    #endregion
}
