Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.HeadFirst.Template.Barista
	Friend Class BeverageTestDrive
        Shared Sub Main(ByVal args() As String)

            Console.WriteLine(Constants.vbLf & "Making tea...")
            Dim tea As New Tea()
            tea.PrepareRecipe()

            Console.WriteLine(Constants.vbLf & "Making coffee...")
            Dim coffee As New Coffee()
            coffee.PrepareRecipe()

            ' Hooked on Template (page 292)

            Console.WriteLine(Constants.vbLf & "Making tea...")
            Dim teaHook As New TeaWithHook()
            teaHook.PrepareRecipe()

            Console.WriteLine(Constants.vbLf & "Making coffee...")
            Dim coffeeHook As New CoffeeWithHook()
            coffeeHook.PrepareRecipe()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	#Region "Coffee and Tea"

	Public MustInherit Class CaffeineBeverage
		Public Sub PrepareRecipe()
			BoilWater()
			Brew()
			PourInCup()
			AddCondiments()
		End Sub

		Public MustOverride Sub Brew()

		Public MustOverride Sub AddCondiments()

		Private Sub BoilWater()
			Console.WriteLine("Boiling water")
		End Sub

		Private Sub PourInCup()
			Console.WriteLine("Pouring into cup")
		End Sub
	End Class

	Public Class Coffee
		Inherits CaffeineBeverage
		Public Overrides Sub Brew()
			Console.WriteLine("Dripping Coffee through filter")
		End Sub
		Public Overrides Sub AddCondiments()
			Console.WriteLine("Adding Sugar and Milk")
		End Sub
	End Class

	Public Class Tea
		Inherits CaffeineBeverage
		Public Overrides Sub Brew()
			Console.WriteLine("Steeping the tea")
		End Sub
		Public Overrides Sub AddCondiments()
			Console.WriteLine("Adding Lemon")
		End Sub
	End Class

	#End Region

	#Region "Coffee and Tea with Hook"

	Public MustInherit Class CaffeineBeverageWithHook
		Public Sub PrepareRecipe()
			BoilWater()
			Brew()
			PourInCup()
			If CustomerWantsCondiments() Then
				AddCondiments()
			End If
		End Sub

		Public MustOverride Sub Brew()

		Public MustOverride Sub AddCondiments()

		Public Sub BoilWater()
			Console.WriteLine("Boiling water")
		End Sub

		Public Sub PourInCup()
			Console.WriteLine("Pouring into cup")
		End Sub

		Public Overridable Function CustomerWantsCondiments() As Boolean
			Return True
		End Function
	End Class

	Public Class CoffeeWithHook
		Inherits CaffeineBeverageWithHook
		Public Overrides Sub Brew()
			Console.WriteLine("Dripping Coffee through filter")
		End Sub

		Public Overrides Sub AddCondiments()
			Console.WriteLine("Adding Sugar and Milk")
		End Sub

		Public Overrides Function CustomerWantsCondiments() As Boolean
			Dim answer As String = GetUserInput()

			If answer.ToLower().StartsWith("y") Then
				Return True
			Else
				Return False
			End If
		End Function

		Public Function GetUserInput() As String
			Dim answer As String = Nothing
			Console.WriteLine("Would you like milk and sugar with your coffee (y/n)? ")

			Try
				answer = Console.ReadLine()
			Catch
				Console.WriteLine("IO error trying to read your answer")
			End Try

			If answer Is Nothing Then
				Return "no"
			End If
			Return answer
		End Function
	End Class

	Public Class TeaWithHook
		Inherits CaffeineBeverageWithHook

		Public Overrides Sub Brew()
			Console.WriteLine("Steeping the tea")
		End Sub

		Public Overrides Sub AddCondiments()
			Console.WriteLine("Adding Lemon")
		End Sub

		Public Overrides Function CustomerWantsCondiments() As Boolean
			Dim answer As String = GetUserInput()

			If answer.ToLower().StartsWith("y") Then
				Return True
			Else
				Return False
			End If
		End Function

		Private Function GetUserInput() As String
			' get the user's response
			Dim answer As String = Nothing

			Console.WriteLine("Would you like lemon with your tea (y/n)? ")

			Try
				answer = Console.ReadLine()
			Catch
				Console.WriteLine("IO error trying to read your answer")
			End Try

			If answer Is Nothing Then
				Return "no"
			End If
			Return answer
		End Function
	End Class
	#End Region
End Namespace
