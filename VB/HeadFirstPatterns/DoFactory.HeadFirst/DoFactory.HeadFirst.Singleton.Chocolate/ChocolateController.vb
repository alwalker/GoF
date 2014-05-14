Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.HeadFirst.Singleton.Chocolate
	Friend Class ChocolateController
        Shared Sub Main(ByVal args() As String)

            Dim boiler As ChocolateBoiler = ChocolateBoiler.GetInstance()
            boiler.Fill()
            boiler.Boil()
            boiler.Drain()

            ' will return the existing instance
            Dim boiler2 As ChocolateBoiler = ChocolateBoiler.GetInstance()

            ' Are they the same?
            If boiler Is boiler2 Then
                Console.WriteLine("Same instances")
            End If

            Dim s1 As Singleton = Singleton.GetInstance()
            Dim s2 As Singleton = Singleton.GetInstance()
            Dim s3 As Singleton = Singleton.GetInstance()

            If s1 Is s2 AndAlso s2 Is s3 Then
                Console.WriteLine("Same instances")
            End If

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "ChocolateBoiler"

	Public Class ChocolateBoiler
		Private Shared _uniqueInstance As ChocolateBoiler

		Private _empty As Boolean
		Private _boiled As Boolean

		' *Private* constructor
		Private Sub New()
			_empty = True
			_boiled = False
		End Sub

		Public Shared Function GetInstance() As ChocolateBoiler
			If _uniqueInstance Is Nothing Then
				Console.WriteLine("Creating unique instance of Chocolate Boiler")
				_uniqueInstance = New ChocolateBoiler()
			End If

			Console.WriteLine("Returning instance of Chocolate Boiler")
			Return _uniqueInstance
		End Function

		Public Sub Fill()
			If Empty Then
				_empty = False
				_boiled = False
				' fill the boiler with a milk/chocolate mixture
			End If
		End Sub

		Public Sub Drain()
			If Empty AndAlso Boiled Then
				' drain the boiled milk and chocolate
				_empty = True
			End If
		End Sub

		Public Sub Boil()
			If (Not Empty) AndAlso (Not Boiled) Then
				' bring the contents to a boil
				_boiled = True
			End If
		End Sub

		' Properties 
		Public ReadOnly Property Empty() As Boolean
			Get
				Return _empty
			End Get
		End Property

		Public ReadOnly Property Boiled() As Boolean
			Get
				Return _boiled
			End Get
		End Property
	End Class
	#End Region

	#Region "Singleton"

	Public Class Singleton
		Private Shared _uniqueInstance As Singleton

		' other useful instance variables here

		' Constructor
		Private Sub New()
		End Sub

		Private Shared ReadOnly _syncLock As Object = New Object()

		Public Shared Function GetInstance() As Singleton
			' Double checked locking
			If _uniqueInstance Is Nothing Then
				SyncLock _syncLock
					If _uniqueInstance Is Nothing Then
						_uniqueInstance = New Singleton()
					End If
				End SyncLock
			End If
			Return _uniqueInstance
		End Function

		' other useful methods here
	End Class
	#End Region
End Namespace
