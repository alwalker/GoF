using System;

namespace DoFactory.HeadFirst.Command.SimpleRemote
{
    class RemoteControlTest
    {
        static void Main(string[] args)
        {
            SimpleRemoteControl remote = new SimpleRemoteControl();

            Light light = new Light();
            LightOnCommand lightOn = new LightOnCommand(light);

            GarageDoor garageDoor = new GarageDoor();
            GarageDoorOpenCommand garageOpen = new GarageDoorOpenCommand(garageDoor);

            remote.Command = lightOn;
            remote.ButtonWasPressed();

            remote.Command = garageOpen;
            remote.ButtonWasPressed();

            // Wait for user
            Console.ReadKey();
        }
    }

    #region SimpleRemoteControl

    // The invoker

    public class SimpleRemoteControl
    {
        private ICommand _slot;

        public SimpleRemoteControl() { }

        public ICommand Command
        {
            set { _slot = value; }
        }

        public void ButtonWasPressed()
        {
            _slot.Execute();
        }
    }

    #endregion

    #region Command

    public interface ICommand
    {
        void Execute();
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

    public class GarageDoorOpenCommand : ICommand
    {
        private GarageDoor _garageDoor;

        public GarageDoorOpenCommand(GarageDoor garageDoor)
        {
            this._garageDoor = garageDoor;
        }

        public void Execute()
        {
            _garageDoor.Up();
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

    #endregion

    #region Light, GarageDoor

    public class Light
    {
        public void On()
        {
            Console.WriteLine("Light is On");
        }

        public void Off()
        {
            Console.WriteLine("Light is Off");
        }
    }

    public class GarageDoor
    {
        public void Up()
        {
            Console.WriteLine("Garage door is Open");
        }

        public void Down()
        {
            Console.WriteLine("Garage door is Closed");
        }

        public void Stop()
        {
            Console.WriteLine("Garage door is Stopped");
        }

        public void LightOn()
        {
            Console.WriteLine("Garage light is on");
        }

        public void LightOff()
        {
            Console.WriteLine("Garage light is off");
        }
    }
    #endregion
}
