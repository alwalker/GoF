Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Factory.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Factory Method Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
		Shared Sub Main()
			' Note: constructors call Factory Method
			Dim documents(1) As Document

			documents(0) = New [Resume]()
			documents(1) = New Report()

			' Display document pages
			For Each document As Document In documents
				Console.WriteLine(Constants.vbLf + document.GetType().Name & "--")
				For Each page As Page In document.Pages
					Console.WriteLine(" " & page.GetType().Name)
				Next page
			Next document

			' Wait for user
			Console.ReadKey()
		End Sub
	End Class

	''' <summary>
	''' The 'Product' abstract class
	''' </summary>
	Friend MustInherit Class Page
	End Class

	''' <summary>
	''' A 'ConcreteProduct' class
	''' </summary>
	Friend Class SkillsPage
		Inherits Page
	End Class

	''' <summary>
	''' A 'ConcreteProduct' class
	''' </summary>
	Friend Class EducationPage
		Inherits Page
	End Class

	''' <summary>
	''' A 'ConcreteProduct' class
	''' </summary>
	Friend Class ExperiencePage
		Inherits Page
	End Class

	''' <summary>
	''' A 'ConcreteProduct' class
	''' </summary>
	Friend Class IntroductionPage
		Inherits Page
	End Class

	''' <summary>
	''' A 'ConcreteProduct' class
	''' </summary>
	Friend Class ResultsPage
		Inherits Page
	End Class

	''' <summary>
	''' A 'ConcreteProduct' class
	''' </summary>
	Friend Class ConclusionPage
		Inherits Page
	End Class

	''' <summary>
	''' A 'ConcreteProduct' class
	''' </summary>
	Friend Class SummaryPage
		Inherits Page
	End Class

	''' <summary>
	''' A 'ConcreteProduct' class
	''' </summary>
	Friend Class BibliographyPage
		Inherits Page
	End Class

	''' <summary>
	''' The 'Creator' abstract class
	''' </summary>
	Friend MustInherit Class Document
		Private _pages As List(Of Page) = New List(Of Page)()

		' Constructor calls abstract Factory method
		Public Sub New()
			Me.CreatePages()
		End Sub

		Public ReadOnly Property Pages() As List(Of Page)
			Get
				Return _pages
			End Get
		End Property

		' Factory Method
		Public MustOverride Sub CreatePages()
	End Class

	''' <summary>
	''' A 'ConcreteCreator' class
	''' </summary>
	Friend Class [Resume]
		Inherits Document
		' Factory Method implementation
		Public Overrides Sub CreatePages()
			Pages.Add(New SkillsPage())
			Pages.Add(New EducationPage())
			Pages.Add(New ExperiencePage())
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteCreator' class
	''' </summary>
	Friend Class Report
		Inherits Document
		' Factory Method implementation
		Public Overrides Sub CreatePages()
			Pages.Add(New IntroductionPage())
			Pages.Add(New ResultsPage())
			Pages.Add(New ConclusionPage())
			Pages.Add(New SummaryPage())
			Pages.Add(New BibliographyPage())
		End Sub
	End Class
End Namespace
