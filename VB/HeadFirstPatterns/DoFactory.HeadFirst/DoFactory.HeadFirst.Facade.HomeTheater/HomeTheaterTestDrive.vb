Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.HeadFirst.Facade.HomeTheater
	Friend Class HomeTheaterTestDrive
        Shared Sub Main(ByVal args() As String)

            Dim amp As New Amplifier("Top-O-Line Amplifier")
            Dim tuner As New Tuner("Top-O-Line AM/FM Tuner", amp)
            Dim dvd As New DvdPlayer("Top-O-Line DVD Player", amp)
            Dim cd As New CdPlayer("Top-O-Line CD Player", amp)

            Dim projector As New Projector("Top-O-Line Projector", dvd)
            Dim lights As New TheaterLights("Theater Ceiling Lights")
            Dim screen As New Screen("Theater Screen")
            Dim popper As New PopcornPopper("Popcorn Popper")

            Dim homeTheater As New HomeTheaterFacade(amp, tuner, dvd, cd, projector, screen, lights, popper)

            homeTheater.WatchMovie("Raiders of the Lost Ark")
            homeTheater.EndMovie()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Facade"

	Public Class HomeTheaterFacade
		Private _amp As Amplifier
		Private _tuner As Tuner
		Private _dvd As DvdPlayer
		Private _cd As CdPlayer
		Private _projector As Projector
		Private _lights As TheaterLights
		Private _screen As Screen
		Private _popper As PopcornPopper

		Public Sub New(ByVal amp As Amplifier, ByVal tuner As Tuner, ByVal dvd As DvdPlayer, ByVal cd As CdPlayer, ByVal projector As Projector, ByVal screen As Screen, ByVal lights As TheaterLights, ByVal popper As PopcornPopper)

			Me._amp = amp
			Me._tuner = tuner
			Me._dvd = dvd
			Me._cd = cd
			Me._projector = projector
			Me._screen = screen
			Me._lights = lights
			Me._popper = popper
		End Sub

		Public Sub WatchMovie(ByVal movie As String)
			Console.WriteLine("Get ready to watch a movie...")
			_popper.On()
			_popper.Pop()
			_lights.Dim(10)
			_screen.Down()
			_projector.On()
			_projector.WideScreenMode()
			_amp.On()
			_amp.SetDvd(_dvd)
			_amp.SetSurroundSound()
			_amp.SetVolume(5)
			_dvd.On()
			_dvd.Play(movie)
		End Sub

		Public Sub EndMovie()
			Console.WriteLine(Constants.vbLf & "Shutting movie theater down...")
			_popper.Off()
			_lights.On()
			_screen.Up()
			_projector.Off()
			_amp.Off()
			_dvd.Stop()
			_dvd.Eject()
			_dvd.Off()
		End Sub

		Public Sub ListenToCd(ByVal cdTitle As String)
			Console.WriteLine("Get ready for an audiopile experence...")
			_lights.On()
			_amp.On()
			_amp.SetVolume(5)
			_amp.SetCd(_cd)
			_amp.SetStereoSound()
			_cd.On()
			_cd.Play(cdTitle)
		End Sub

		Public Sub EndCd()
			Console.WriteLine("Shutting down CD...")
			_amp.Off()
			_amp.SetCd(_cd)
			_cd.Eject()
			_cd.Off()
		End Sub

		Public Sub ListenToRadio(ByVal frequency As Double)
			Console.WriteLine("Tuning in the airwaves...")
			_tuner.On()
			_tuner.SetFrequency(frequency)
			_amp.On()
			_amp.SetVolume(5)
			_amp.SetTuner(_tuner)
		End Sub

		Public Sub EndRadio()
			Console.WriteLine("Shutting down the tuner...")
			_tuner.Off()
			_amp.Off()
		End Sub
	End Class

	#End Region

	#Region "Subsystem Components"

	Public Class Tuner
		Private _description As String
		Private _amplifier As Amplifier
		Private _frequency As Double

		Public Sub New(ByVal description As String, ByVal amplifier As Amplifier)
			Me._description = description
			Me._amplifier = amplifier
		End Sub

		Public Sub [On]()
			Console.WriteLine(_description & " on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine(_description & " off")
		End Sub

		Public Sub SetFrequency(ByVal frequency As Double)
			Console.WriteLine(_description & " setting frequency to " & frequency)
			Me._frequency = frequency
		End Sub

		Public Sub SetAm()
			Console.WriteLine(_description & " setting AM mode")
		End Sub

		Public Sub SetFm()
			Console.WriteLine(_description & " setting FM mode")
		End Sub

		Public Overrides Function ToString() As String
			Return _description
		End Function
	End Class

	Public Class Amplifier
		Private _description As String
		Private _tuner As Tuner
		Private _dvd As DvdPlayer
		Private _cd As CdPlayer

		Public Sub New(ByVal description As String)
			Me._description = description
		End Sub

		Public Sub [On]()
			Console.WriteLine(_description & " on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine(_description & " off")
		End Sub

		Public Sub SetStereoSound()
			Console.WriteLine(_description & " stereo mode on")
		End Sub

		Public Sub SetSurroundSound()
			Console.WriteLine(_description & " surround sound on (5 speakers, 1 subwoofer)")
		End Sub

		Public Sub SetVolume(ByVal level As Integer)
			Console.WriteLine(_description & " setting volume to " & level)
		End Sub

		Public Sub SetTuner(ByVal tuner As Tuner)
            Console.WriteLine(_description & " setting tuner to " & _dvd.ToString())
			Me._tuner = tuner
		End Sub

		Public Sub SetDvd(ByVal dvd As DvdPlayer)
            Console.WriteLine(_description & " setting DVD player to " & dvd.ToString())
			Me._dvd = dvd
		End Sub

		Public Sub SetCd(ByVal cd As CdPlayer)
            Console.WriteLine(_description & " setting CD player to " & cd.ToString())
			Me._cd = cd
		End Sub

		Public Overrides Function ToString() As String
			Return _description
		End Function
	End Class
	Public Class TheaterLights
		Private _description As String

		Public Sub New(ByVal description As String)
			Me._description = description
		End Sub

		Public Sub [On]()
			Console.WriteLine(_description & " on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine(_description & " off")
		End Sub

		Public Sub [Dim](ByVal level As Integer)
			Console.WriteLine(_description & " dimming to " & level & "%")
		End Sub

		Public Overrides Function ToString() As String
			Return _description
		End Function
	End Class

	Public Class Screen
		Private _description As String

		Public Sub New(ByVal description As String)
			Me._description = description
		End Sub

		Public Sub Up()
			Console.WriteLine(_description & " going up")
		End Sub

		Public Sub Down()
			Console.WriteLine(_description & " going down")
		End Sub


		Public Overrides Function ToString() As String
			Return _description
		End Function
	End Class

	Public Class Projector
		Private _description As String
		Private _dvdPlayer As DvdPlayer

		Public Sub New(ByVal description As String, ByVal dvdPlayer As DvdPlayer)
			Me._description = description
			Me._dvdPlayer = dvdPlayer
		End Sub

		Public Sub [On]()
			Console.WriteLine(_description & " on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine(_description & " off")
		End Sub

		Public Sub WideScreenMode()
			Console.WriteLine(_description & " in widescreen mode (16x9 aspect ratio)")
		End Sub

		Public Sub TvMode()
			Console.WriteLine(_description & " in tv mode (4x3 aspect ratio)")
		End Sub

		Public Overrides Function ToString() As String
			Return _description
		End Function
	End Class

	Public Class PopcornPopper
		Private _description As String

		Public Sub New(ByVal description As String)
			Me._description = description
		End Sub

		Public Sub [On]()
			Console.WriteLine(_description & " on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine(_description & " off")
		End Sub

		Public Sub Pop()
			Console.WriteLine(_description & " popping popcorn!")
		End Sub
		Public Overrides Function ToString() As String
			Return _description
		End Function
	End Class
	Public Class DvdPlayer
		Private _description As String
		Private _currentTrack As Integer
		Private _amplifier As Amplifier
		Private _movie As String

		Public Sub New(ByVal description As String, ByVal amplifier As Amplifier)
			Me._description = description
			Me._amplifier = amplifier
		End Sub

		Public Sub [On]()
			Console.WriteLine(_description & " on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine(_description & " off")
		End Sub

		Public Sub Eject()
			_movie = Nothing
			Console.WriteLine(_description & " eject")
		End Sub

		Public Sub Play(ByVal movie As String)
			Me._movie = movie
			_currentTrack = 0
			Console.WriteLine(_description & " playing """ & movie & """")
		End Sub

		Public Sub Play(ByVal track As Integer)
			If _movie Is Nothing Then
				Console.WriteLine(_description & " can't play track " & track & " no dvd inserted")
			Else
				_currentTrack = track
				Console.WriteLine(_description & " playing track " & _currentTrack & " of """ & _movie & """")
			End If
		End Sub

		Public Sub [Stop]()
			_currentTrack = 0
			Console.WriteLine(_description & " stopped """ & _movie & """")
		End Sub

		Public Sub Pause()
			Console.WriteLine(_description & " paused """ & _movie & """")
		End Sub

		Public Sub SetTwoChannelAudio()
			Console.WriteLine(_description & " set two channel audio")
		End Sub

		Public Sub SetSurroundAudio()
			Console.WriteLine(_description & " set surround audio")
		End Sub

		Public Overrides Function ToString() As String
			Return _description
		End Function
	End Class

	Public Class CdPlayer
		Private _description As String
		Private _currentTrack As Integer
		Private _amplifier As Amplifier
		Private _title As String

		Public Sub New(ByVal description As String, ByVal amplifier As Amplifier)
			Me._description = description
			Me._amplifier = amplifier
		End Sub

		Public Sub [On]()
			Console.WriteLine(_description & " on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine(_description & " off")
		End Sub

		Public Sub Eject()
			_title = Nothing
			Console.WriteLine(_description & " eject")
		End Sub

		Public Sub Play(ByVal title As String)
			Me._title = title
			_currentTrack = 0
			Console.WriteLine(_description & " playing """ & title & """")
		End Sub

		Public Sub Play(ByVal track As Integer)
			If _title Is Nothing Then
				Console.WriteLine(_description & " can't play track " & _currentTrack & ", no cd inserted")
			Else
				_currentTrack = track
				Console.WriteLine(_description & " playing track " & _currentTrack)
			End If
		End Sub

		Public Sub [Stop]()
			_currentTrack = 0
			Console.WriteLine(_description & " stopped")
		End Sub

		Public Sub Pause()
			Console.WriteLine(_description & " paused """ & _title & """")
		End Sub

		Public Overrides Function ToString() As String
			Return _description
		End Function
	End Class
	#End Region
End Namespace
