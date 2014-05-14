using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoFactory.HeadFirst.Combined.MVC
{
    /// <summary>
    /// BeatPanel supports Beat events
    /// </summary>
    public class BeatPanel : Panel, IBeatable
    {
        public void Beat(Model sender, BeatEventArgs e)
        {
            // Depending on beatsPerMinute set color anywhere 
            // between red and yellow
            int red = 255;
            int green = 255 - (e.BeatsPerMinute + 55);
            int blue = 0;

            this.BackColor = Color.FromArgb(red, green, blue);
        }
    }
}
