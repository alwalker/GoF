Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.GangOfFour.Memento.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Memento Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            Dim s As New SalesProspect()
            s.Name = "Noel van Halen"
            s.Phone = "(412) 256-0990"
            s.Budget = 25000.0

            ' Store internal state
            Dim m As New ProspectMemory()
            m.Memento = s.SaveMemento()

            ' Continue changing originator
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

		' Stores memento
		Public Function SaveMemento() As Memento
			Console.WriteLine(Constants.vbLf & "Saving state --" & Constants.vbLf)
			Return New Memento(_name, _phone, _budget)
		End Function

		' Restores memento
		Public Sub RestoreMemento(ByVal memento As Memento)
			Console.WriteLine(Constants.vbLf & "Restoring state --" & Constants.vbLf)
			Me.Name = memento.Name
			Me.Phone = memento.Phone
			Me.Budget = memento.Budget
		End Sub
	End Class

	''' <summary>
	''' The 'Memento' class
	''' </summary>
	Friend Class Memento
		Private _name As String
		Private _phone As String
		Private _budget As Double

		' Constructor
		Public Sub New(ByVal name As String, ByVal phone As String, ByVal budget As Double)
			Me._name = name
			Me._phone = phone
			Me._budget = budget
		End Sub

		' Gets or sets name
		Public Property Name() As String
			Get
				Return _name
			End Get
			Set(ByVal value As String)
				_name = value
			End Set
		End Property

		' Gets or set phone
		Public Property Phone() As String
			Get
				Return _phone
			End Get
			Set(ByVal value As String)
				_phone = value
			End Set
		End Property

		' Gets or sets budget
		Public Property Budget() As Double
			Get
				Return _budget
			End Get
			Set(ByVal value As Double)
				_budget = value
			End Set
		End Property
	End Class

	''' <summary>
	''' The 'Caretaker' class
	''' </summary>
	Friend Class ProspectMemory
		Private _memento As Memento

		' Property
		Public Property Memento() As Memento
			Set(ByVal value As Memento)
				_memento = value
			End Set
			Get
				Return _memento
			End Get
		End Property
	End Class
End Namespace
