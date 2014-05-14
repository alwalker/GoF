Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Decorator.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Decorator Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create book
            Dim book As New Book("Worley", "Inside ASP.NET", 10)
            book.Display()

            ' Create video
            Dim video As New Video("Spielberg", "Jaws", 23, 92)
            video.Display()

            ' Make video borrowable, then borrow and display
            Console.WriteLine(Constants.vbLf & "Making video borrowable:")

            Dim borrowvideo As New Borrowable(video)
            borrowvideo.BorrowItem("Customer #1")
            borrowvideo.BorrowItem("Customer #2")

            borrowvideo.Display()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Component' abstract class
	''' </summary>
	Friend MustInherit Class LibraryItem
		Private _numCopies As Integer

		' Property
		Public Property NumCopies() As Integer
			Get
				Return _numCopies
			End Get
			Set(ByVal value As Integer)
				_numCopies = value
			End Set
		End Property

		Public MustOverride Sub Display()
	End Class

	''' <summary>
	''' The 'ConcreteComponent' class
	''' </summary>
	Friend Class Book
		Inherits LibraryItem
		Private _author As String
		Private _title As String

		' Constructor
		Public Sub New(ByVal author As String, ByVal title As String, ByVal numCopies As Integer)
			Me._author = author
			Me._title = title
			Me.NumCopies = numCopies
		End Sub

		Public Overrides Sub Display()
			Console.WriteLine(Constants.vbLf & "Book ------ ")
			Console.WriteLine(" Author: {0}", _author)
			Console.WriteLine(" Title: {0}", _title)
			Console.WriteLine(" # Copies: {0}", NumCopies)
		End Sub
	End Class

	''' <summary>
	''' The 'ConcreteComponent' class
	''' </summary>
	Friend Class Video
		Inherits LibraryItem
		Private _director As String
		Private _title As String
		Private _playTime As Integer

		' Constructor
		Public Sub New(ByVal director As String, ByVal title As String, ByVal numCopies As Integer, ByVal playTime As Integer)
			Me._director = director
			Me._title = title
			Me.NumCopies = numCopies
			Me._playTime = playTime
		End Sub

		Public Overrides Sub Display()
			Console.WriteLine(Constants.vbLf & "Video ----- ")
			Console.WriteLine(" Director: {0}", _director)
			Console.WriteLine(" Title: {0}", _title)
			Console.WriteLine(" # Copies: {0}", NumCopies)
			Console.WriteLine(" Playtime: {0}" & Constants.vbLf, _playTime)
		End Sub
	End Class

	''' <summary>
	''' The 'Decorator' abstract class
	''' </summary>
	Friend MustInherit Class Decorator
		Inherits LibraryItem
		Protected libraryItem As LibraryItem

		' Constructor
		Public Sub New(ByVal libraryItem As LibraryItem)
			Me.libraryItem = libraryItem
		End Sub

		Public Overrides Sub Display()
			libraryItem.Display()
		End Sub
	End Class

	''' <summary>
	''' The 'ConcreteDecorator' class
	''' </summary>
	Friend Class Borrowable
		Inherits Decorator
		Protected borrowers As List(Of String) = New List(Of String)()

		' Constructor
		Public Sub New(ByVal libraryItem As LibraryItem)
			MyBase.New(libraryItem)
		End Sub

		Public Sub BorrowItem(ByVal name As String)
			borrowers.Add(name)
			libraryItem.NumCopies -= 1
		End Sub

		Public Sub ReturnItem(ByVal name As String)
			borrowers.Remove(name)
			libraryItem.NumCopies += 1
		End Sub

		Public Overrides Sub Display()
			MyBase.Display()

			For Each borrower As String In borrowers
				Console.WriteLine(" borrower: " & borrower)
			Next borrower
		End Sub
	End Class
End Namespace
