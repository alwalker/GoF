Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Soap

Namespace DoFactory.GangOfFour.Memento.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Memento Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Init sales prospect through object initialization
            Dim s As SalesProspect = New SalesProspect With {.Name = "Joel van Halen", .Phone = "(412) 256-0990", .Budget = 25000.0}

            ' Store internal state
            Dim m As New ProspectMemory()
            m.Memento = s.SaveMemento()

            ' Change originator
            s.Name = "Leo Welch"
            s.Phone = "(310) 209-7111"
            s.Budget = 1000000.0

            ' Restore saved state
            s.RestoreMemento(m.Memento)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Originator' class
	''' </summary>
	<Serializable> _
	Friend Class SalesProspect
		Private _name As String
		Private _phone As String
		Private _budget As Double

		' Gets or sets name
		Public Property Name() As String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				_name = value
				Console.WriteLine("Name:   " & _name)
			End Set
		End Property

		' Gets or sets phone
		Public Property Phone() As String
			Get
				Return _phone
			End Get
			Set(ByVal value As String)
				_phone = value
				Console.WriteLine("Phone:  " & _phone)
			End Set
		End Property

		' Gets or sets budget
		Public Property Budget() As Double
			Get
				Return _budget
			End Get
			Set(ByVal value As Double)
				_budget = value
				Console.WriteLine("Budget: " & _budget)
			End Set
		End Property

		' Stores (serializes) memento
		Public Function SaveMemento() As Memento
			Console.WriteLine(Constants.vbLf & "Saving state --" & Constants.vbLf)

			Dim memento As New Memento()
			Return memento.Serialize(Me)
		End Function

		' Restores (deserializes) memento
		Public Sub RestoreMemento(ByVal memento As Memento)
			Console.WriteLine(Constants.vbLf & "Restoring state --" & Constants.vbLf)

			Dim s As SalesProspect = CType(memento.Deserialize(), SalesProspect)
			Me.Name = s.Name
			Me.Phone = s.Phone
			Me.Budget = s.Budget
		End Sub
	End Class

	''' <summary>
	''' The 'Memento' class
	''' </summary>
	Friend Class Memento
		Private _stream As New MemoryStream()
		Private _formatter As New SoapFormatter()

		Public Function Serialize(ByVal o As Object) As Memento
			_formatter.Serialize(_stream, o)
			Return Me
		End Function

		Public Function Deserialize() As Object
			_stream.Seek(0, SeekOrigin.Begin)
			Dim o As Object = _formatter.Deserialize(_stream)
			_stream.Close()

			Return o
		End Function
	End Class

	''' <summary>
	''' The 'Caretaker' class
	''' </summary>
	Friend Class ProspectMemory
        Private _memento As Memento
        Public Property Memento() As Memento
            Get
                Return _memento
            End Get
            Set(ByVal value As Memento)
                _memento = value
            End Set
        End Property
	End Class
End Namespace
