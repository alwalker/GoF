using System;
using System.Windows.Forms;

namespace DoFactory.HeadFirst.Combined.MVC
{
    /// <summary>
    /// BeatTextBox supports beat events
    /// </summary>
    public class BeatTextBox : TextBox , IBeatable
    {
        public void Beat(Model sender, BeatEventArgs e)
        {
            this.Text = e.BeatsPerMinute.ToString();
        }
    }
}
