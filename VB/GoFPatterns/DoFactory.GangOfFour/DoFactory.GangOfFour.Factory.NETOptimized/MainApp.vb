Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Factory.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Factory Method Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Note: document constructors call Factory Method
            Dim documents As List(Of Document) = New List(Of Document)(New Document() {New [Resume](), New Report()})

            ' Display document pages
            For Each document As Document In documents
                Console.WriteLine(document.ToString() & "--")
                For Each page As Page In document.Pages
                    Console.WriteLine(" " & page.ToString())
                Next page
                Console.WriteLine()
            Next document

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Product' abstract class
	''' </summary>
	Friend MustInherit Class Page
		' Override. Display class name
		Public Overrides Function ToString() As String
			Return Me.GetType().Name
		End Function
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
		' Constructor invokes Factory Method
		Public Sub New()
			Me.CreatePages()
		End Sub

		' Gets list of document pages
        Private _pages As List(Of Page)
        Public Property Pages() As List(Of Page)
            Get
                Return _pages
            End Get
            Protected Set(ByVal value As List(Of Page))
                _pages = value
            End Set
        End Property

		' Factory Method
		Public MustOverride Sub CreatePages()

		' Override
		Public Overrides Function ToString() As String
			Return Me.GetType().Name
		End Function
	End Class

	''' <summary>
	''' A 'ConcreteCreator' class
	''' </summary>
	Friend Class [Resume]
		Inherits Document
		' Factory Method implementation
		Public Overrides Sub CreatePages()
			Pages = New List(Of Page) (New Page() {New SkillsPage(), New EducationPage(), New ExperiencePage()})
		End Sub
	End Class

	''' <summary>
	''' A 'ConcreteCreator' class
	''' </summary>
	Friend Class Report
		Inherits Document
		' Factory Method implementation
		Public Overrides Sub CreatePages()
			Pages = New List(Of Page) (New Page() {New IntroductionPage(), New ResultsPage(), New ConclusionPage(), New SummaryPage(), New BibliographyPage()})
		End Sub
	End Class
End Namespace
