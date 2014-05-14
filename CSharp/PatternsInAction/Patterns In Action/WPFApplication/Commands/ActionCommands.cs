using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Input;

namespace WPFApplication.Commands
{
    /// <summary>
    /// Class that holds static commands  
    /// </summary>
    public class ActionCommands
    {
        // Static routed commands
        public static RoutedUICommand LoginCommand { private set; get; }
        public static RoutedUICommand LogoutCommand { private set; get; }
        public static RoutedUICommand ExitCommand { private set; get; }

        public static RoutedUICommand AddCommand { private set; get; }
        public static RoutedUICommand EditCommand { private set; get; }
        public static RoutedUICommand DeleteCommand { private set; get; }
        public static RoutedUICommand ViewOrdersCommand { private set; get; }

        public static RoutedUICommand HowDoICommand { private set; get; }
        public static RoutedUICommand IndexCommand { private set; get; }
        public static RoutedUICommand AboutCommand { private set; get; }

        /// <summary>
        /// Static constructor. 
        /// Creates several Routed UI commands with and without shortcut keys.
        /// </summary>
        static ActionCommands()
        {
            // Initialize static commands
            LoginCommand = MakeRoutedUICommand("Login", Key.I, "Ctrl+I");
            LogoutCommand = MakeRoutedUICommand("Logout", Key.O, "Ctrl+O");
            ExitCommand = MakeRoutedUICommand("Exit");

            AddCommand = MakeRoutedUICommand("Add", Key.A, "Ctrl+A");
            EditCommand = MakeRoutedUICommand("Edit", Key.E, "Ctrl+E");
            DeleteCommand = MakeRoutedUICommand("Delete", Key.Delete, "Del");

            ViewOrdersCommand = MakeRoutedUICommand("View Orders");

            HowDoICommand = MakeRoutedUICommand("How Do I", Key.H, "Ctrl+D");
            IndexCommand = MakeRoutedUICommand("Index", Key.N, "Ctrl+N");
            AboutCommand = MakeRoutedUICommand("About");
        }

        /// <summary>
        /// Creates a routed command instance without shortcut key.
        /// </summary>
        /// <param name="name">Given name.</param>
        /// <returns>The routed UI command.</returns>
        private static RoutedUICommand MakeRoutedUICommand(string name)
        {
            return new RoutedUICommand(name, name, typeof(ActionCommands));
        }

        /// <summary>
        /// Creates a routed command instance with a shortcut key.
        /// </summary>
        /// <param name="name">Given name.</param>
        /// <param name="key">Shortcut key.</param>
        /// <param name="displayString">Display string.</param>
        /// <returns>The Routed UI command.</returns>
        private static RoutedUICommand MakeRoutedUICommand(string name, Key key, string displayString)
        {
            InputGestureCollection gestures = new InputGestureCollection();
            gestures.Add(new KeyGesture(key, ModifierKeys.Control, displayString));

            return new RoutedUICommand(name, name, typeof(ActionCommands), gestures);
        }
    }
}
