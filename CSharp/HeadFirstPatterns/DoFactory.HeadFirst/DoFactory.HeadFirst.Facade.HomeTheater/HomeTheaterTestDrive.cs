using System;

namespace DoFactory.HeadFirst.Facade.HomeTheater
{
    class HomeTheaterTestDrive
    {
        static void Main(string[] args)
        {
            Amplifier amp = new Amplifier("Top-O-Line Amplifier");
            Tuner tuner = new Tuner("Top-O-Line AM/FM Tuner", amp);
            DvdPlayer dvd = new DvdPlayer("Top-O-Line DVD Player", amp);
            CdPlayer cd = new CdPlayer("Top-O-Line CD Player", amp);

            Projector projector = new Projector("Top-O-Line Projector", dvd);
            TheaterLights lights = new TheaterLights("Theater Ceiling Lights");
            Screen screen = new Screen("Theater Screen");
            PopcornPopper popper = new PopcornPopper("Popcorn Popper");

            HomeTheaterFacade homeTheater =
                new HomeTheaterFacade(amp, tuner, dvd, cd,
                projector, screen, lights, popper);

            homeTheater.WatchMovie("Raiders of the Lost Ark");
            homeTheater.EndMovie();

            // Wait for user
            Console.ReadKey();
        }
    }

    #region Facade

    public class HomeTheaterFacade
    {
        private Amplifier _amp;
        private Tuner _tuner;
        private DvdPlayer _dvd;
        private CdPlayer _cd;
        private Projector _projector;
        private TheaterLights _lights;
        private Screen _screen;
        private PopcornPopper _popper;

        public HomeTheaterFacade(Amplifier amp,
            Tuner tuner,
            DvdPlayer dvd,
            CdPlayer cd,
            Projector projector,
            Screen screen,
            TheaterLights lights,
            PopcornPopper popper)
        {

            this._amp = amp;
            this._tuner = tuner;
            this._dvd = dvd;
            this._cd = cd;
            this._projector = projector;
            this._screen = screen;
            this._lights = lights;
            this._popper = popper;
        }

        public void WatchMovie(string movie)
        {
            Console.WriteLine("Get ready to watch a movie...");
            _popper.On();
            _popper.Pop();
            _lights.Dim(10);
            _screen.Down();
            _projector.On();
            _projector.WideScreenMode();
            _amp.On();
            _amp.SetDvd(_dvd);
            _amp.SetSurroundSound();
            _amp.SetVolume(5);
            _dvd.On();
            _dvd.Play(movie);
        }

        public void EndMovie()
        {
            Console.WriteLine("\nShutting movie theater down...");
            _popper.Off();
            _lights.On();
            _screen.Up();
            _projector.Off();
            _amp.Off();
            _dvd.Stop();
            _dvd.Eject();
            _dvd.Off();
        }

        public void ListenToCd(string cdTitle)
        {
            Console.WriteLine("Get ready for an audiopile experence...");
            _lights.On();
            _amp.On();
            _amp.SetVolume(5);
            _amp.SetCd(_cd);
            _amp.SetStereoSound();
            _cd.On();
            _cd.Play(cdTitle);
        }

        public void EndCd()
        {
            Console.WriteLine("Shutting down CD...");
            _amp.Off();
            _amp.SetCd(_cd);
            _cd.Eject();
            _cd.Off();
        }

        public void ListenToRadio(double frequency)
        {
            Console.WriteLine("Tuning in the airwaves...");
            _tuner.On();
            _tuner.SetFrequency(frequency);
            _amp.On();
            _amp.SetVolume(5);
            _amp.SetTuner(_tuner);
        }

        public void EndRadio()
        {
            Console.WriteLine("Shutting down the tuner...");
            _tuner.Off();
            _amp.Off();
        }
    }

    #endregion

    #region Subsystem Components

    public class Tuner
    {
        private string _description;
        private Amplifier _amplifier;
        private double _frequency;

        public Tuner(string description, Amplifier amplifier)
        {
            this._description = description;
            this._amplifier = amplifier;
        }

        public void On()
        {
            Console.WriteLine(_description + " on");
        }

        public void Off()
        {
            Console.WriteLine(_description + " off");
        }

        public void SetFrequency(double frequency)
        {
            Console.WriteLine(_description + " setting frequency to " + frequency);
            this._frequency = frequency;
        }

        public void SetAm()
        {
            Console.WriteLine(_description + " setting AM mode");
        }

        public void SetFm()
        {
            Console.WriteLine(_description + " setting FM mode");
        }

        public override string ToString()
        {
            return _description;
        }
    }

    public class Amplifier
    {
        private string _description;
        private Tuner _tuner;
        private DvdPlayer _dvd;
        private CdPlayer _cd;

        public Amplifier(string description)
        {
            this._description = description;
        }

        public void On()
        {
            Console.WriteLine(_description + " on");
        }

        public void Off()
        {
            Console.WriteLine(_description + " off");
        }

        public void SetStereoSound()
        {
            Console.WriteLine(_description + " stereo mode on");
        }

        public void SetSurroundSound()
        {
            Console.WriteLine(_description + " surround sound on (5 speakers, 1 subwoofer)");
        }

        public void SetVolume(int level)
        {
            Console.WriteLine(_description + " setting volume to " + level);
        }

        public void SetTuner(Tuner tuner)
        {
            Console.WriteLine(_description + " setting tuner to " + _dvd);
            this._tuner = tuner;
        }

        public void SetDvd(DvdPlayer dvd)
        {
            Console.WriteLine(_description + " setting DVD player to " + dvd);
            this._dvd = dvd;
        }

        public void SetCd(CdPlayer cd)
        {
            Console.WriteLine(_description + " setting CD player to " + cd);
            this._cd = cd;
        }

        public override string ToString()
        {
            return _description;
        }
    }
    public class TheaterLights
    {
        private string _description;

        public TheaterLights(string description)
        {
            this._description = description;
        }

        public void On()
        {
            Console.WriteLine(_description + " on");
        }

        public void Off()
        {
            Console.WriteLine(_description + " off");
        }

        public void Dim(int level)
        {
            Console.WriteLine(_description + " dimming to " + level + "%");
        }

        public override string ToString()
        {
            return _description;
        }
    }

    public class Screen
    {
        private string _description;

        public Screen(string description)
        {
            this._description = description;
        }

        public void Up()
        {
            Console.WriteLine(_description + " going up");
        }

        public void Down()
        {
            Console.WriteLine(_description + " going down");
        }


        public override string ToString()
        {
            return _description;
        }
    }

    public class Projector
    {
        private string _description;
        private DvdPlayer _dvdPlayer;

        public Projector(string description, DvdPlayer dvdPlayer)
        {
            this._description = description;
            this._dvdPlayer = dvdPlayer;
        }

        public void On()
        {
            Console.WriteLine(_description + " on");
        }

        public void Off()
        {
            Console.WriteLine(_description + " off");
        }

        public void WideScreenMode()
        {
            Console.WriteLine(_description + " in widescreen mode (16x9 aspect ratio)");
        }

        public void TvMode()
        {
            Console.WriteLine(_description + " in tv mode (4x3 aspect ratio)");
        }

        public override string ToString()
        {
            return _description;
        }
    }

    public class PopcornPopper
    {
        private string _description;

        public PopcornPopper(string description)
        {
            this._description = description;
        }

        public void On()
        {
            Console.WriteLine(_description + " on");
        }

        public void Off()
        {
            Console.WriteLine(_description + " off");
        }

        public void Pop()
        {
            Console.WriteLine(_description + " popping popcorn!");
        }
        public override string ToString()
        {
            return _description;
        }
    }
    public class DvdPlayer
    {
        private string _description;
        private int _currentTrack;
        private Amplifier _amplifier;
        private string _movie;

        public DvdPlayer(string description, Amplifier amplifier)
        {
            this._description = description;
            this._amplifier = amplifier;
        }

        public void On()
        {
            Console.WriteLine(_description + " on");
        }

        public void Off()
        {
            Console.WriteLine(_description + " off");
        }

        public void Eject()
        {
            _movie = null;
            Console.WriteLine(_description + " eject");
        }

        public void Play(string movie)
        {
            this._movie = movie;
            _currentTrack = 0;
            Console.WriteLine(_description + " playing \"" + movie + "\"");
        }

        public void Play(int track)
        {
            if (_movie == null)
            {
                Console.WriteLine(_description + " can't play track " + track + " no dvd inserted");
            }
            else
            {
                _currentTrack = track;
                Console.WriteLine(_description + " playing track " + _currentTrack + " of \"" + _movie + "\"");
            }
        }

        public void Stop()
        {
            _currentTrack = 0;
            Console.WriteLine(_description + " stopped \"" + _movie + "\"");
        }

        public void Pause()
        {
            Console.WriteLine(_description + " paused \"" + _movie + "\"");
        }

        public void SetTwoChannelAudio()
        {
            Console.WriteLine(_description + " set two channel audio");
        }

        public void SetSurroundAudio()
        {
            Console.WriteLine(_description + " set surround audio");
        }

        public override string ToString()
        {
            return _description;
        }
    }

    public class CdPlayer
    {
        private string _description;
        private int _currentTrack;
        private Amplifier _amplifier;
        private string _title;

        public CdPlayer(string description, Amplifier amplifier)
        {
            this._description = description;
            this._amplifier = amplifier;
        }

        public void On()
        {
            Console.WriteLine(_description + " on");
        }

        public void Off()
        {
            Console.WriteLine(_description + " off");
        }

        public void Eject()
        {
            _title = null;
            Console.WriteLine(_description + " eject");
        }

        public void Play(string title)
        {
            this._title = title;
            _currentTrack = 0;
            Console.WriteLine(_description + " playing \"" + title + "\"");
        }

        public void Play(int track)
        {
            if (_title == null)
            {
                Console.WriteLine(_description + " can't play track " + _currentTrack +
                    ", no cd inserted");
            }
            else
            {
                _currentTrack = track;
                Console.WriteLine(_description + " playing track " + _currentTrack);
            }
        }

        public void Stop()
        {
            _currentTrack = 0;
            Console.WriteLine(_description + " stopped");
        }

        public void Pause()
        {
            Console.WriteLine(_description + " paused \"" + _title + "\"");
        }

        public override string ToString()
        {
            return _description;
        }
    }
    #endregion
}
