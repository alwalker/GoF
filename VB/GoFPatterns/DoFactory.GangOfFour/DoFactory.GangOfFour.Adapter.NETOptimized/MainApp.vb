Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.GangOfFour.Adapter.NETOptimized
	''' <summary>
	''' MainApp startup class for the .NET optimized 
	''' Adapter Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Non-adapted chemical compound 
            Dim unknown As New Compound(Chemical.Unknown)
            unknown.Display()

            ' Adapted chemical compounds
            Dim water As Compound = New RichCompound(Chemical.Water)
            water.Display()

            Dim benzene As Compound = New RichCompound(Chemical.Benzene)
            benzene.Display()

            Dim ethanol As Compound = New RichCompound(Chemical.Ethanol)
            ethanol.Display()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Target' class
	''' </summary>
	Friend Class Compound
		' Constructor
		Public Sub New(ByVal chemical As Chemical)
			Me.Chemical = chemical
		End Sub

		' Automatic properties
        Private _chemical As Chemical
        Public Property Chemical() As Chemical
            Get
                Return _chemical
            End Get
            Set(ByVal value As Chemical)
                _chemical = value
            End Set
        End Property
        Private _boilingPoint As Single
        Public Property BoilingPoint() As Single
            Get
                Return _boilingPoint
            End Get
            Set(ByVal value As Single)
                _boilingPoint = value
            End Set
        End Property
        Private _meltingPoint As Single
        Public Property MeltingPoint() As Single
            Get
                Return _meltingPoint
            End Get
            Set(ByVal value As Single)
                _meltingPoint = value
            End Set
        End Property
        Private _molecularWeight As Double
        Public Property MolecularWeight() As Double
            Get
                Return _molecularWeight
            End Get
            Set(ByVal value As Double)
                _molecularWeight = value
            End Set
        End Property
        Private _molecularFormula As String
        Public Property MolecularFormula() As String
            Get
                Return _molecularFormula
            End Get
            Set(ByVal value As String)
                _molecularFormula = value
            End Set
        End Property

		Public Overridable Sub Display()
			Console.WriteLine(Constants.vbLf & "Compound: {0} -- ", Chemical)
		End Sub
	End Class

	''' <summary>
	''' The 'Adapter' class
	''' </summary>
	Friend Class RichCompound
		Inherits Compound
		Private _bank As ChemicalDatabank

		' Constructor
		Public Sub New(ByVal chemical As Chemical)
			MyBase.New(chemical)
		End Sub

		Public Overrides Sub Display()
			' The Adaptee
			_bank = New ChemicalDatabank()

			' Adaptee request methods
			BoilingPoint = _bank.GetCriticalPoint(Chemical, State.Boiling)
			MeltingPoint = _bank.GetCriticalPoint(Chemical, State.Melting)
			MolecularWeight = _bank.GetMolecularWeight(Chemical)
			MolecularFormula = _bank.GetMolecularStructure(Chemical)

			MyBase.Display()
			Console.WriteLine(" Formula: {0}", MolecularFormula)
			Console.WriteLine(" Weight : {0}", MolecularWeight)
			Console.WriteLine(" Melting Pt: {0}", MeltingPoint)
			Console.WriteLine(" Boiling Pt: {0}", BoilingPoint)
		End Sub
	End Class

	''' <summary>
	''' The 'Adaptee' class
	''' </summary>
	Friend Class ChemicalDatabank
		' The databank 'legacy API'
		Public Function GetCriticalPoint(ByVal compound As Chemical, ByVal point As State) As Single
			' Melting Point
			If point = State.Melting Then
				Select Case compound
					Case Chemical.Water
						Return 0.0f
					Case Chemical.Benzene
						Return 5.5f
					Case Chemical.Ethanol
						Return -114.1f
					Case Else
						Return 0f
				End Select
			' Boiling Point
			Else
				Select Case compound
					Case Chemical.Water
						Return 100.0f
					Case Chemical.Benzene
						Return 80.1f
					Case Chemical.Ethanol
						Return 78.3f
					Case Else
						Return 0f
				End Select
			End If
		End Function

		Public Function GetMolecularStructure(ByVal compound As Chemical) As String
			Select Case compound
				Case Chemical.Water
					Return "H20"
				Case Chemical.Benzene
					Return "C6H6"
				Case Chemical.Ethanol
					Return "C2H5OH"
				Case Else
					Return ""
			End Select
		End Function

		Public Function GetMolecularWeight(ByVal compound As Chemical) As Double
			Select Case compound
				Case Chemical.Water
					Return 18.015
				Case Chemical.Benzene
					Return 78.1134
				Case Chemical.Ethanol
					Return 46.0688
			End Select
			Return 0R
		End Function
	End Class


	''' <summary>
	''' Chemical enumeration
	''' </summary>
	Public Enum Chemical
		Unknown
		Water
		Benzene
		Ethanol
	End Enum

	''' <summary>
	''' State enumeration
	''' </summary>
	Public Enum State
		Boiling
		Melting
	End Enum
End Namespace
