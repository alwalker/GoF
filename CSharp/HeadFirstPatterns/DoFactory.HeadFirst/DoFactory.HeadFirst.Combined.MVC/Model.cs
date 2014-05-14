using System;
using System.Timers;

namespace DoFactory.HeadFirst.Combined.MVC
{
    /// <summary>
    /// The Model in the MVC pattern
    /// </summary>
    public class Model
    {
        private int _beatsPerMinute = 120;
        private int _interval = 500;
        private Timer _timer;

        public delegate void BeatHandler<T>(T sender, BeatEventArgs e);
        public event BeatHandler<Model> Beat;

        public Model()
        {
            _timer = new Timer();
            _timer.Interval = _interval;
            _timer.Elapsed += new ElapsedEventHandler(this.OnTimedEvent);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void OnTimedEvent(object o, ElapsedEventArgs e)
        {
            Console.Beep(2000, 10);
            
            // Let the observers know about this beat
            OnBeat(new BeatEventArgs(_beatsPerMinute));
        }

        public int BeatsPerMinute
        {
            set
            { 
                _beatsPerMinute = value; 

                // Interval is in milliseconds
                _interval = (int)((60D / ((double)_beatsPerMinute )) * 1000D);
                _timer.Interval = _interval;

                // Update display of observers
                OnBeat(new BeatEventArgs(_beatsPerMinute));
            }
            get
            { 
                return _beatsPerMinute; 
            }
        }

        public void Attach(IBeatable beatable)
        {
            Beat += new BeatHandler<Model>(beatable.Beat);
        }

        public void Detach(IBeatable beatable)
        {
            Beat -= new BeatHandler<Model>(beatable.Beat);
        }

        // Invoke the Beat event
        public virtual void OnBeat(BeatEventArgs e) 
        {
            if (Beat != null)
            {
                Beat(this, e);
            }
        }
    }
}
