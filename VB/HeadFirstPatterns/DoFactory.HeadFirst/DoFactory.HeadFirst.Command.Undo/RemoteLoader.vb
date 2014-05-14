Imports Microsoft.VisualBasic
Imports System
Imports System.Text

Namespace DoFactory.HeadFirst.Command.Undo
	Friend Class RemoteLoader
        Shared Sub Main(ByVal args() As String)

            Dim remoteControl As New RemoteControlWithUndo()

            Dim livingRoomLight As New Light("Living Room")

            Dim livingRoomLightOn As New LightOnCommand(livingRoomLight)
            Dim livingRoomLightOff As New LightOffCommand(livingRoomLight)

            remoteControl.SetCommand(0, livingRoomLightOn, livingRoomLightOff)

            remoteControl.OnButtonWasPushed(0)
            remoteControl.OffButtonWasPushed(0)

            Console.WriteLine(remoteControl)

            remoteControl.UndoButtonWasPushed()
            remoteControl.OffButtonWasPushed(0)
            remoteControl.OnButtonWasPushed(0)

            Console.WriteLine(remoteControl)

            remoteControl.UndoButtonWasPushed()

            Dim ceilingFan As New CeilingFan("Living Room")

            Dim ceilingFanMedium As New CeilingFanMediumCommand(ceilingFan)
            Dim ceilingFanHigh As New CeilingFanHighCommand(ceilingFan)
            Dim ceilingFanOff As New CeilingFanOffCommand(ceilingFan)

            remoteControl.SetCommand(0, ceilingFanMedium, ceilingFanOff)
            remoteControl.SetCommand(1, ceilingFanHigh, ceilingFanOff)

            remoteControl.OnButtonWasPushed(0)
            remoteControl.OffButtonWasPushed(0)

            Console.WriteLine(remoteControl)

            remoteControl.UndoButtonWasPushed()
            remoteControl.OnButtonWasPushed(1)

            Console.WriteLine(remoteControl)

            remoteControl.UndoButtonWasPushed()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Remote Control"

	Public Class RemoteControlWithUndo
		Private _onCommands() As ICommand
		Private _offCommands() As ICommand
		Private _undoCommand As ICommand

		' Constructor
		Public Sub New()
			_onCommands = New ICommand(6){}
			_offCommands = New ICommand(6){}

			Dim noCommand As ICommand = New NoCommand()
			For i As Integer = 0 To 6
				_onCommands (i) = noCommand
				_offCommands(i) = noCommand
			Next i
			_undoCommand = noCommand
		End Sub

		Public Sub SetCommand(ByVal slot As Integer, ByVal onCommand As ICommand, ByVal offCommand As ICommand)
			_onCommands (slot) = onCommand
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
			Dim stringBuff As New StringBuilder()
			stringBuff.Append(Constants.vbLf & "------ Remote Control -------" & Constants.vbLf)
			For i As Integer = 0 To _onCommands.Length - 1
				stringBuff.Append("[slot " & i & "] " & _onCommands(i).GetType().Name & "    " & _offCommands(i).GetType().Name + Constants.vbLf)
			Next i
			stringBuff.Append("[undo] " & CType(_undoCommand, Object).GetType().Name + Constants.vbLf)
			Return stringBuff.ToString()
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

	Public Class DimmerLightOnCommand
		Implements ICommand
		Private _light As Light
		Private _previousLevel As Integer

		Public Sub New(ByVal light As Light)
			Me._light = light
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_previousLevel = _light.Level
			_light.Dim(75)
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			_light.Dim(_previousLevel)
		End Sub
	End Class

	Public Class DimmerLightOffCommand
		Implements ICommand
		Private _light As Light
		Private _previousLevel As Integer = 100

		Public Sub New(ByVal light As Light)
			Me._light = light
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_previousLevel = _light.Level
			_light.Off()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			_light.Dim(_previousLevel)
		End Sub
	End Class

	Public Class CeilingFanOffCommand
		Implements ICommand
		Private _ceilingFan As CeilingFan
		Private _previousSpeed As CeilingFanSpeed

		Public Sub New(ByVal ceilingFan As CeilingFan)
			Me._ceilingFan = ceilingFan
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_previousSpeed = _ceilingFan.Speed
			_ceilingFan.Off()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			Select Case _previousSpeed
				Case CeilingFanSpeed.High
					_ceilingFan.High()
				Case CeilingFanSpeed.Medium
					_ceilingFan.Medium()
				Case CeilingFanSpeed.Low
					_ceilingFan.Low()
				Case CeilingFanSpeed.Off
					_ceilingFan.Off()
			End Select
		End Sub
	End Class

	Public Class CeilingFanMediumCommand
		Implements ICommand
		Private _ceilingFan As CeilingFan
		Private _previousSpeed As CeilingFanSpeed

		Public Sub New(ByVal ceilingFan As CeilingFan)
			Me._ceilingFan = ceilingFan
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_previousSpeed = _ceilingFan.Speed
			_ceilingFan.Medium()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			Select Case _previousSpeed
				Case CeilingFanSpeed.High
					_ceilingFan.High()
				Case CeilingFanSpeed.Medium
					_ceilingFan.Medium()
				Case CeilingFanSpeed.Low
					_ceilingFan.Low()
				Case CeilingFanSpeed.Off
					_ceilingFan.Off()
			End Select
		End Sub
	End Class

	Public Class CeilingFanLowCommand
		Implements ICommand
		Private _ceilingFan As CeilingFan
		Private _previousSpeed As CeilingFanSpeed

		Public Sub New(ByVal ceilingFan As CeilingFan)
			Me._ceilingFan = ceilingFan
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_previousSpeed = _ceilingFan.Speed
			_ceilingFan.Low()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			Select Case _previousSpeed
				Case CeilingFanSpeed.High
					_ceilingFan.High()
				Case CeilingFanSpeed.Medium
					_ceilingFan.Medium()
				Case CeilingFanSpeed.Low
					_ceilingFan.Low()
				Case CeilingFanSpeed.Off
					_ceilingFan.Off()
			End Select
		End Sub
	End Class

	Public Class CeilingFanHighCommand
		Implements ICommand
		Private _ceilingFan As CeilingFan
		Private _previousSpeed As CeilingFanSpeed

		Public Sub New(ByVal ceilingFan As CeilingFan)
			Me._ceilingFan = ceilingFan
		End Sub

		Public Sub Execute() Implements ICommand.Execute
			_previousSpeed = _ceilingFan.Speed
			_ceilingFan.High()
		End Sub

		Public Sub Undo() Implements ICommand.Undo
			Select Case _previousSpeed
				Case CeilingFanSpeed.High
					_ceilingFan.High()
				Case CeilingFanSpeed.Medium
					_ceilingFan.Medium()
				Case CeilingFanSpeed.Low
					_ceilingFan.Low()
				Case CeilingFanSpeed.Off
					_ceilingFan.Off()
			End Select
		End Sub
	End Class

	#End Region

	#Region "Light and CeilingFan"

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
			Level = level
			If Level = 0 Then
				[Off]()
			Else
				Console.WriteLine("Light is dimmed to " & Level & "%")
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
			Speed = CeilingFanSpeed.Off
		End Sub

		Public Sub High()
			Speed = CeilingFanSpeed.High
			Console.WriteLine(_location & " ceiling fan is on high")
		End Sub

		Public Sub Medium()
			Speed = CeilingFanSpeed.Medium
			Console.WriteLine(_location & " ceiling fan is on medium")
		End Sub

		Public Sub Low()
			Speed = CeilingFanSpeed.Low
			Console.WriteLine(_location & " ceiling fan is on low")
		End Sub

		Public Sub [Off]()
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
