Imports Microsoft.VisualBasic
Imports System
Imports System.Text

Namespace DoFactory.HeadFirst.Command.Party
	Friend Class RemoteLoader
        Shared Sub Main(ByVal args() As String)

            Dim remoteControl As New RemoteControl()

            Dim light As New Light("Living Room")
            Dim tv As New TV("Living Room")
            Dim stereo As New Stereo("Living Room")
            Dim hottub As New Hottub()

            Dim lightOn As New LightOnCommand(light)
            Dim stereoOn As New StereoOnCommand(stereo)
            Dim tvOn As New TVOnCommand(tv)
            Dim hottubOn As New HottubOnCommand(hottub)
            Dim lightOff As New LightOffCommand(light)
            Dim stereoOff As New StereoOffCommand(stereo)
            Dim tvOff As New TVOffCommand(tv)
            Dim hottubOff As New HottubOffCommand(hottub)

            Dim partyOn() As ICommand = {lightOn, stereoOn, tvOn, hottubOn}
            Dim partyOff() As ICommand = {lightOff, stereoOff, tvOff, hottubOff}

            Dim partyOnMacro As New MacroCommand(partyOn)
            Dim partyOffMacro As New MacroCommand(partyOff)

            remoteControl.SetCommand(0, partyOnMacro, partyOffMacro)

            Console.WriteLine(remoteControl)
            Console.WriteLine("--- Pushing Macro On---")
            remoteControl.OnButtonWasPushed(0)
            Console.WriteLine(Constants.vbLf & "--- Pushing Macro Off---")
            remoteControl.OffButtonWasPushed(0)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Remote Control"

	Public Class RemoteControl
		Private _onCommands() As ICommand
		Private _offCommands() As ICommand
		Private _undoCommand As ICommand

		' Constructor
		Public Sub New()
			_onCommands = New ICommand(6){}
			_offCommands = New ICommand(6){}

			Dim noCommand As ICommand = New NoCommand()
			For i As Integer = 0 To 6
				_onCommands(i) = noCommand
				_offCommands(i) = noCommand
			Next i
			_undoCommand = noCommand
		End Sub

		Public Sub SetCommand(ByVal slot As Integer, ByVal onCommand As ICommand, ByVal offCommand As ICommand)
			_onCommands(slot) = onCommand
			_offCommands(slot) = offCommand
		End Sub

		Public Sub OnButtonWasPushed(ByVal slot As Integer)
			_onCommands(slot).Execute()
			_undoCommand = _onCommands(slot)
		End Sub

		Public Sub OffButtonWasPushed(ByVal slot As Integer)
			_offCommands(slot).Execute()
			_undoCommand = _offCommands(slot)
		End Sub

		Public Sub UndoButtonWasPushed()
			_undoCommand.Undo()
		End Sub

		Public Overrides Function ToString() As String
			Dim sb As New StringBuilder()
			sb.Append(Constants.vbLf & "------ Remote Control -------" & Constants.vbLf)
			For i As Integer = 0 To _onCommands.Length - 1
				sb.Append("[slot " & i & "] " & _onCommands(i).GetType().Name & "    " & _offCommands(i).GetType().Name + Constants.vbLf)
			Next i
			sb.Append("[undo] " & CType(_undoCommand, Object).GetType().Name + Constants.vbLf)
			Return sb.ToString()
		End Function
	End Class

	#End Region

	#Region "Commands"

	Public Interface ICommand
		Sub Execute()
		Sub Undo()
	End Interface

	Public Class NoCommand
		Implements ICommand
		Public Sub Execute() Implements ICommand.Execute
		End Sub
		Public Sub Undo() Implements ICommand.Undo
		End Sub
	End Class

	Public Class MacroCommand
		Implements ICommand
		Private _commands() As ICommand

		Public Sub New(ByVal commands() As ICommand)
			Me._commands = commands
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			For i As Integer = 0 To _commands.Length - 1
				_commands(i).Execute()
			Next i
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			For i As Integer = 0 To _commands.Length - 1
				_commands(i).Undo()
			Next i
		End Sub
	End Class

	Public Class TVOnCommand
		Implements ICommand
		Private _tv As TV

		Public Sub New(ByVal tv As TV)
			Me._tv = tv
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_tv.On()
			_tv.SetInputChannel()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			_tv.Off()
		End Sub
	End Class

	Public Class TVOffCommand
		Implements ICommand
		Private _tv As TV

		Public Sub New(ByVal tv As TV)
			Me._tv= tv
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_tv.Off()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			_tv.On()
		End Sub
	End Class

	Public Class StereoOnCommand
		Implements ICommand
		Private _stereo As Stereo

		Public Sub New(ByVal stereo As Stereo)
			Me._stereo = stereo
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_stereo.On()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			_stereo.Off()
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

		Public Sub Undo() Implements ICommand.Undo
			_stereo.On()
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

		Public Sub Undo() Implements ICommand.Undo
			_stereo.Off()
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

		Public Sub Undo() Implements ICommand.Undo
			_light.Off()
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

		Public Sub Undo() Implements ICommand.Undo
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

		Public Sub Undo() Implements ICommand.Undo
			_light.Off()
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

		Public Sub Undo() Implements ICommand.Undo
			_light.On()
		End Sub
	End Class

	Public Class HottubOffCommand
		Implements ICommand
		Private _hottub As Hottub

		Public Sub New(ByVal hottub As Hottub)
			Me._hottub = hottub
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_hottub.SetTemperature(98)
			_hottub.Off()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			_hottub.On()
		End Sub
	End Class

	Public Class CeilingFanOffCommand
		Implements ICommand
		Private _ceilingFan As CeilingFan
		Private _prevSpeed As CeilingFanSpeed

		Public Sub New(ByVal ceilingFan As CeilingFan)
			Me._ceilingFan = ceilingFan
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_prevSpeed = _ceilingFan.Speed
			_ceilingFan.Off()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			Select Case _prevSpeed
				Case CeilingFanSpeed.High
					_ceilingFan.high()
				Case CeilingFanSpeed.Medium
					_ceilingFan.medium()
				Case CeilingFanSpeed.Low
					_ceilingFan.low()
				Case CeilingFanSpeed.Off
					_ceilingFan.Off()
			End Select
		End Sub
	End Class

	Public Class CeilingFanMediumCommand
		Implements ICommand
		Private _ceilingFan As CeilingFan
		Private _prevSpeed As CeilingFanSpeed

		Public Sub New(ByVal ceilingFan As CeilingFan)
			Me._ceilingFan = ceilingFan
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_prevSpeed = _ceilingFan.Speed
			_ceilingFan.medium()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			Select Case _prevSpeed
				Case CeilingFanSpeed.High
					_ceilingFan.high()
				Case CeilingFanSpeed.Medium
					_ceilingFan.medium()
				Case CeilingFanSpeed.Low
					_ceilingFan.low()
				Case CeilingFanSpeed.Off
					_ceilingFan.Off()
			End Select
		End Sub
	End Class

	Public Class CeilingFanHighCommand
		Implements ICommand
		Private _ceilingFan As CeilingFan
		Private _prevSpeed As CeilingFanSpeed

		Public Sub New(ByVal ceilingFan As CeilingFan)
			Me._ceilingFan = ceilingFan
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_prevSpeed = _ceilingFan.Speed
			_ceilingFan.high()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			Select Case _prevSpeed
				Case CeilingFanSpeed.High
					_ceilingFan.high()
				Case CeilingFanSpeed.Medium
					_ceilingFan.medium()
				Case CeilingFanSpeed.Low
					_ceilingFan.low()
				Case CeilingFanSpeed.Off
					_ceilingFan.Off()
			End Select
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
			_hottub.SetTemperature(104)
			_hottub.Circulate()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			_hottub.Off()
		End Sub
	End Class

	#End Region

	#Region "TV, Tub, CeilingFan, etc"

	Public Class Hottub
		Private _on As Boolean
		Private _temperature As Integer

		Public Sub [On]()
			_on = True
		End Sub

		Public Sub [Off]()
			_on = False
		End Sub

		Public Sub Circulate()
			If _on Then
				Console.WriteLine("Hottub is bubbling!")
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
			If temperature > Me._temperature Then
				Console.WriteLine("Hottub is heating to a steaming " & temperature & " degrees")
			Else
				Console.WriteLine("Hottub is cooling to " & temperature & " degrees")
			End If
			Me._temperature = temperature
		End Sub
	End Class


	Public Class TV
		Private _location As String
		Private _channel As Integer

		Public Sub New(ByVal location As String)
			Me._location = location
		End Sub

		Public Sub [On]()
			Console.WriteLine(_location & " TV is on")
		End Sub

		Public Sub [Off]()
			Console.WriteLine(_location & " TV is off")
		End Sub

		Public Sub SetInputChannel()
			Me._channel = 3
			Console.WriteLine(_location & " TV channel " & _channel & " is set for DVD")
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

		Public Sub setDVD()
			Console.WriteLine(_location & " stereo is set for DVD input")
		End Sub

		Public Sub SetRadio()
			Console.WriteLine(_location & " stereo is set for Radio")
		End Sub

		Public Sub SetVolume(ByVal volume As Integer)
			' code to set the volume
			' valid range: 1-11 (after all 11 is better than 10, right?)
			Console.WriteLine(_location & " Stereo volume set to " & volume)
		End Sub
	End Class

	Public Class Light
		Private _location As String

		Public Sub New(ByVal location As String)
			Me._location = location
		End Sub

		Public Sub [On]()
			Level = 100
			Console.WriteLine("Light is on")
		End Sub

		Public Sub [Off]()
			Level = 0
			Console.WriteLine("Light is off")
		End Sub

		Public Sub [Dim](ByVal level As Integer)
			Me.Level = level
			If Level = 0 Then
				[Off]()
			Else
				Console.WriteLine("Light is dimmed to " & level & "%")
			End If
		End Sub

		Private privateLevel As Integer
		Public Property Level() As Integer
			Get
				Return privateLevel
			End Get
			Private Set(ByVal value As Integer)
				privateLevel = value
			End Set
		End Property
	End Class

	Public Class CeilingFan
		Private _location As String

		Public Sub New(ByVal location As String)
			Me._location = location
		End Sub

		Public Sub high()
			' turns the ceiling fan on to high
			Speed = CeilingFanSpeed.High
			Console.WriteLine(_location & " ceiling fan is on high")
		End Sub

		Public Sub medium()
			' turns the ceiling fan on to medium
			Speed = CeilingFanSpeed.Medium
			Console.WriteLine(_location & " ceiling fan is on medium")
		End Sub

		Public Sub low()
			' turns the ceiling fan on to low
			Speed = CeilingFanSpeed.Low
			Console.WriteLine(_location & " ceiling fan is on low")
		End Sub

		Public Sub [Off]()
			' turns the ceiling fan off
			Speed = CeilingFanSpeed.Off
			Console.WriteLine(_location & " ceiling fan is off")
		End Sub

		Private privateSpeed As CeilingFanSpeed
		Public Property Speed() As CeilingFanSpeed
			Get
				Return privateSpeed
			End Get
			Private Set(ByVal value As CeilingFanSpeed)
				privateSpeed = value
			End Set
		End Property

	End Class

	Public Enum CeilingFanSpeed
		High
		Medium
		Low
		[Off]
	End Enum
	#End Region
End Namespace
