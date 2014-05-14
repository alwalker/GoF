using System;

namespace DoFactory.HeadFirst.Combined.MVC
{
    /// <summary>
    /// IBeatable interface. A class that wants to 
    /// listen to Beat events must implement this interface
    /// </summary>
    public interface IBeatable
    {
        void Beat(Model sender, BeatEventArgs e);
    }
}
