using System;

// Replacement for the Java 'Swing example'
namespace DoFactory.HeadFirst.Observer.DotNet
{
    class DotNetObserverExample
    {
        static void Main()
        {
            // Create listeners
            ActionListener angel = new ActionListener("Angel");
            ActionListener devil = new ActionListener("Devil");

            // Create Button and attach listeners
            Button button = new Button("Click Me");
            button.Attach(angel);
            button.Attach(devil);

            // Simulate clicks on button
            button.Push(1, 3);
            button.Push(5, 4);
            button.Push(8, 5);

            // Wait for user
            Console.ReadKey();
        }
    }

    #region EventArgs

    // Custom event arguments
    public class ClickEventArgs : EventArgs
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        // Constructor
        public ClickEventArgs(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    #endregion

    #region Controls

    // Base class for UI controls

    abstract class Control
    {
        protected string text;

        // Constructor
        public Control(string text)
        {
            this.text = text;
        }

        // Event
        public event EventHandler<ClickEventArgs> Click;

        // Invoke the Click  event
        public virtual void OnClick(ClickEventArgs e)
        {
            if (Click != null)
            {
                Click(this, e);
            }
        }

        public void Attach(ActionListener listener)
        {
            Click += listener.Update;
        }

        public void Detach(ActionListener listener)
        {
            Click -= listener.Update;
        }

        // Use this method to simulate push (click) events
        public void Push(int x, int y)
        {
            OnClick(new ClickEventArgs(x, y));
            Console.WriteLine("");
        }
    }

    // Button control

    class Button : Control
    {
        // Constructor
        public Button(string text)
            : base(text)
        {
        }
    }

    #endregion

    #region ActionListener

    interface IActionListener
    {
        void Update(object sender, ClickEventArgs e);
    }

    class ActionListener : IActionListener
    {
        private string _name;

        // Constructor
        public ActionListener(string name)
        {
            this._name = name;
        }

        public void Update(object sender, ClickEventArgs e)
        {
            Console.WriteLine("Notified {0} of click at ({1},{2})",
                _name, e.X, e.Y);
        }
    }
    #endregion
}
