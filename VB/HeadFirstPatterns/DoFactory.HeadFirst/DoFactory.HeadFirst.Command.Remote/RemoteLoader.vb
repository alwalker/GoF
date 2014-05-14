Imports Microsoft.VisualBasic
Imports System
Imports System.Text

Namespace DoFactory.HeadFirst.Command.Remote
	Friend Class RemoteLoader
        Shared Sub Main(ByVal args() As String)

            Dim remoteControl As New RemoteControl()

            Dim livingRoomLight As New Light("Living Room")
            Dim kitchenLight As New Light("Kitchen")
            Dim ceilingFan As New CeilingFan("Living Room")
            Dim garageDoor As New GarageDoor("")
            Dim stereo As New Stereo("Living Room")

            Dim livingRoomLightOn As New LightOnCommand(livingRoomLight)

            Dim livingRoomLightOff As New LightOffCommand(livingRoomLight)

            Dim kitchenLightOn As New LightOnCommand(kitchenLight)

            Dim kitchenLightOff As New LightOffCommand(kitchenLight)

            Dim ceilingFanOn As New CeilingFanOnCommand(ceilingFan)

            Dim ceilingFanOff As New CeilingFanOffCommand(ceilingFan)

            Dim garageDoorUp As New GarageDoorUpCommand(garageDoor)

            Dim garageDoorDown As New GarageDoorDownCommand(garageDoor)

            Dim stereoOnWithCD As New StereoOnWithCDCommand(stereo)

            Dim stereoOff As New StereoOffCommand(stereo)

            remoteControl.SetCommand(0, livingRoomLightOn, livingRoomLightOff)
            remoteControl.SetCommand(1, kitchenLightOn, kitchenLightOff)
            remoteControl.SetCommand(2, ceilingFanOn, ceilingFanOff)
            remoteControl.SetCommand(3, stereoOnWithCD, stereoOff)

            Console.WriteLine(remoteControl)

            remoteControl.OnButtonWasPushed(0)
            remoteControl.OffButtonWasPushed(0)
            remoteControl.OnButtonWasPushed(1)
            remoteControl.OffButtonWasPushed(1)
            remoteControl.OnButtonWasPushed(2)
            remoteControl.OffButtonWasPushed(2)
            remoteControl.OnButtonWasPushed(3)
            remoteControl.OffButtonWasPushed(3)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "RemoteControl"

	' This is the invoker

	Public Class RemoteControl
		Private _onCommands() As ICommand
		Private _offCommands() As ICommand

		Public Sub New()
			_onCommands = New ICommand(6){}
			_offCommands = New ICommand(6){}

			Dim noCommand As ICommand = New NoCommand()
			For i As Integer = 0 To 6
				_onCommands(i) = noCommand
				_offCommands(i) = noCommand
			Next i
		End Sub

		Public Sub SetCommand(ByVal slot As Integer, ByVal onCommand As ICommand, ByVal offCommand As ICommand)
			_onCommands(slot) = onCommand
			_offCommands(slot) = offCommand
		End Sub

		Public Sub OnButtonWasPushed(ByVal slot As Integer)
			_onCommands(slot).Execute()
		End Sub

		Public Sub OffButtonWasPushed(ByVal slot As Integer)
			_offCommands(slot).Execute()
		End Sub

		Public Overrides Function ToString() As String
			Dim sb As New StringBuilder()
			sb.Append(Constants.vbLf & "------ Remote Control -------" & Constants.vbLf)
			For i As Integer = 0 To _onCommands.Length - 1
				sb.Append("[slot " & i & "] " & _onCommands(i).GetType().Name & "    " & _offCommands(i).GetType().Name + Constants.vbLf)
			Next i

			Return sb.ToString()
		End Function
	End Class
	#End Region

	#Region "Commands"

	Public Interface ICommand
		Sub Execute()
	End Interface

	Public Class NoCommand
		Implements ICommand
		Public Sub Execute() Implements ICommand.Execute
		End Sub
	End Class

	Public Class LivingroomLightOnCommand
		Implements ICommand
		Private _light As Light

		Public Sub New(ByVal light As Light)
			Me._light = light
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_light.On()
		End Sub
	End Class

	Public Class LightOnCommand
		Implements ICommand
		Private _light As Light

		Public Sub New(ByVal light As Light)
			Me._light = light
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_light.On()
		End Sub
	End Class

	Public Class LightOffCommand
		Implements ICommand
		Private _light As Light

		Public Sub New(ByVal light As Light)
			Me._light = light
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_light.Off()
		End Sub
	End Class
	Public Class GarageDoorUpCommand
		Implements ICommand
		Private _garageDoor As GarageDoor

		Public Sub New(ByVal garageDoor As GarageDoor)
			Me._garageDoor = garageDoor
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_garageDoor.up()
		End Sub
	End Class

	Public Class GarageDoorDownCommand
		Implements ICommand
		Private _garageDoor As GarageDoor

		Public Sub New(ByVal garageDoor As GarageDoor)
			Me._garageDoor = garageDoor
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_garageDoor.up()
		End Sub
	End Class

	Public Class CeilingFanOnCommand
		Implements ICommand
		Private _ceilingFan As CeilingFan

		Public Sub New(ByVal ceilingFan As CeilingFan)
			Me._ceilingFan = ceilingFan
		End Sub
		Public Sub Execute() Implements ICommand.Execute
			_ceilingFan.High()
		End Sub
	End Class

	Public Class CeilingFanOffCommand
		Implements ICommand
		Private _ceilingFan As CeilingFan

		Public Sub New(ByVal ceilingFan As CeilingFan)
			Me._ceilingFan = ceilingFan
		End Sub
		Public Sub Execute() Implements ICommand.Execute
			_ceilingFan.Off()
		End Sub
	End Class

	Public Class LivingroomLightOffCommand
		Implements ICommand
		Private _light As Light

		Public Sub New(ByVal light As Light)
			Me._light = light
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_light.Off()
		End Sub
	End Class

	Public Class HottubOnCommand
		Implements ICommand
		Private _hottub As Hottub

		Public Sub New(ByVal hottub As Hottub)
			Me._hottub = hottub
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_hottub.On()
			_hottub.Heat()
			_hottub.BubblesOn()
		End Sub
	End Class

	Public Class HottubOffCommand
		Implements ICommand
		Private _hottub As Hottub

		Public Sub New(ByVal hottub As Hottub)
			Me._hottub = hottub
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_hottub.Cool()
			_hottub.Off()
		End Sub
	End Class

	Public Class StereoOnWithCDCommand
		Implements ICommand
		Private _stereo As Stereo

		Public Sub New(ByVal stereo As Stereo)
			Me._stereo = stereo
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_stereo.On()
			_stereo.SetCD()
			_stereo.SetVolume(11)
		End Sub
	End Class

	Public Class StereoOffCommand
		Implements ICommand
		Private _stereo As Stereo

		Public Sub New(ByVal stereo As Stereo)
			Me._stereo = stereo
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_stereo.Off()
		End Sub
	End Class

	#End Region

	#Region "Light, TV, GarageDoor, CeilingFan, etc"

	Public Class Light
		Private _location As String

		Public Sub New(ByVal location As String)
			Me._location = location
		End Sub

		Public Sub [On]()
			Console.WriteLine(_location & " light is on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine(_location & " light is off")
		End Sub
	End Class

	Public Class GarageDoor
		Private _location As String

		Public Sub New(ByVal location As String)
			Me._location = location
		End Sub

		Public Sub up()
			Console.WriteLine(_location & " garage Door is Up")
		End Sub

		Public Sub down()
			Console.WriteLine(_location & " garage Door is Down")
		End Sub

		Public Sub [stop]()
			Console.WriteLine(_location & " garage Door is Stopped")
		End Sub

		Public Sub lightOn()
			Console.WriteLine(_location & " garage light is on")
		End Sub

		Public Sub lightOff()
			Console.WriteLine(_location & " garage light is off")
		End Sub
	End Class

	Public Class CeilingFan
		Private _location As String = ""
		Private _level As Integer

		Public Shared ReadOnly HIGH_Renamed As Integer = 2
		Public Shared ReadOnly MEDIUM_Renamed As Integer = 1
		Public Shared ReadOnly LOW_Renamed As Integer = 0

		Public Sub New(ByVal location As String)
			Me._location = location
		End Sub

		Public Sub High()
			' turns the ceiling fan on to high
			_level = HIGH_Renamed
			Console.WriteLine(_location & " ceiling fan is on high")
		End Sub

		Public Sub Medium()
			' turns the ceiling fan on to medium
			_level = MEDIUM_Renamed
			Console.WriteLine(_location & " ceiling fan is on medium")
		End Sub

		Public Sub Low()
			' turns the ceiling fan on to low
			_level = LOW_Renamed
			Console.WriteLine(_location & " ceiling fan is on low")
		End Sub

		Public Sub [Off]()
			' turns the ceiling fan off
			_level = 0
			Console.WriteLine(_location & " ceiling fan is off")
		End Sub

		Public Function getSpeed() As Integer
			Return _level
		End Function
	End Class

	Public Class Hottub
		Private _on As Boolean
		Private _temperature As Integer

		Public Sub New()
		End Sub

		Public Sub [On]()
			_on = True
		End Sub

		Public Sub [Off]()
			_on = False
		End Sub

		Public Sub BubblesOn()
			If _on Then
				Console.WriteLine("Hottub is bubbling!")
			End If
		End Sub

		Public Sub BubblesOff()
			If _on Then
				Console.WriteLine("Hottub is not bubbling")
			End If
		End Sub

		Public Sub JetsOn()
			If _on Then
				Console.WriteLine("Hottub jets are on")
			End If
		End Sub

		Public Sub JetsOff()
			If _on Then
				Console.WriteLine("Hottub jets are off")
			End If
		End Sub

		Public Sub SetTemperature(ByVal temperature As Integer)
			Me._temperature = temperature
		End Sub

		Public Sub Heat()
			_temperature = 105
			Console.WriteLine("Hottub is heating to a steaming 105 degrees")
		End Sub

		Public Sub Cool()
			_temperature = 98
			Console.WriteLine("Hottub is cooling to 98 degrees")
		End Sub
	End Class

	Public Class TV
		Private _location As String
		Private _channel As Integer

		Public Sub New(ByVal location As String)
			Me._location = location
		End Sub

		Public Sub [On]()
			Console.WriteLine("TV is on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine("TV is off")
		End Sub

		Public Sub SetInputChannel()
			Me._channel = 3
			Console.WriteLine("Channel " & _channel & " is set for VCR")
		End Sub
	End Class

	Public Class Stereo
		Private _location As String

		Public Sub New(ByVal location As String)
			Me._location = location
		End Sub

		Public Sub [On]()
			Console.WriteLine(_location & " stereo is on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine(_location & " stereo is off")
		End Sub

		Public Sub SetCD()
			Console.WriteLine(_location & " stereo is set for CD input")
		End Sub

		Public Sub SetDVD()
			Console.WriteLine(_location & " stereo is set for DVD input")
		End Sub

		Public Sub SetRadio()
			Console.WriteLine(_location & " stereo is set for Radio")
		End Sub

		Public Sub SetVolume(ByVal volume As Integer)
			' Code to set the volume
			' Valid range: 1-11 (after all 11 is better than 10, right?)
			Console.WriteLine(_location & " Stereo volume set to " & volume)
		End Sub
	End Class
	#End Region
End Namespace
