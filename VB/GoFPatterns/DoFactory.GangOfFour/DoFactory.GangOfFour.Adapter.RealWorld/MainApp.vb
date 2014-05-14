Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.GangOfFour.Adapter.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
	''' Adapter Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Non-adapted chemical compound
            Dim unknown As New Compound("Unknown")
            unknown.Display()

            ' Adapted chemical compounds
            Dim water As Compound = New RichCompound("Water")
            water.Display()

            Dim benzene As Compound = New RichCompound("Benzene")
            benzene.Display()

            Dim ethanol As Compound = New RichCompound("Ethanol")
            ethanol.Display()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Target' class
	''' </summary>
	Friend Class Compound
		Protected _chemical As String
		Protected _boilingPoint As Single
		Protected _meltingPoint As Single
		Protected _molecularWeight As Double
		Protected _molecularFormula As String

		' Constructor
		Public Sub New(ByVal chemical As String)
			Me._chemical = chemical
		End Sub

		Public Overridable Sub Display()
			Console.WriteLine(Constants.vbLf & "Compound: {0} ------ ", _chemical)
		End Sub
	End Class

	''' <summary>
	''' The 'Adapter' class
	''' </summary>
	Friend Class RichCompound
		Inherits Compound
		Private _bank As ChemicalDatabank

		' Constructor
		Public Sub New(ByVal name As String)
			MyBase.New(name)
		End Sub

		Public Overrides Sub Display()
			' The Adaptee
			_bank = New ChemicalDatabank()

			_boilingPoint = _bank.GetCriticalPoint(_chemical, "B")
			_meltingPoint = _bank.GetCriticalPoint(_chemical, "M")
			_molecularWeight = _bank.GetMolecularWeight(_chemical)
			_molecularFormula = _bank.GetMolecularStructure(_chemical)

			MyBase.Display()
			Console.WriteLine(" Formula: {0}", _molecularFormula)
			Console.WriteLine(" Weight : {0}", _molecularWeight)
			Console.WriteLine(" Melting Pt: {0}", _meltingPoint)
			Console.WriteLine(" Boiling Pt: {0}", _boilingPoint)
		End Sub
	End Class

	''' <summary>
	''' The 'Adaptee' class
	''' </summary>
	Friend Class ChemicalDatabank
		' The databank 'legacy API'
		Public Function GetCriticalPoint(ByVal compound As String, ByVal point As String) As Single
			' Melting Point
			If point = "M" Then
				Select Case compound.ToLower()
					Case "water"
						Return 0.0f
					Case "benzene"
						Return 5.5f
					Case "ethanol"
						Return -114.1f
					Case Else
						Return 0f
				End Select
			' Boiling Point
			Else
				Select Case compound.ToLower()
					Case "water"
						Return 100.0f
					Case "benzene"
						Return 80.1f
					Case "ethanol"
						Return 78.3f
					Case Else
						Return 0f
				End Select
			End If
		End Function

		Public Function GetMolecularStructure(ByVal compound As String) As String
			Select Case compound.ToLower()
				Case "water"
					Return "H20"
				Case "benzene"
					Return "C6H6"
				Case "ethanol"
					Return "C2H5OH"
				Case Else
					Return ""
			End Select
		End Function

		Public Function GetMolecularWeight(ByVal compound As String) As Double
			Select Case compound.ToLower()
				Case "water"
					Return 18.015
				Case "benzene"
					Return 78.1134
				Case "ethanol"
					Return 46.0688
				Case Else
					Return 0R
			End Select
		End Function
	End Class
End Namespace
