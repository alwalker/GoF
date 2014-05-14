Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Formatters.Binary

Namespace DoFactory.GangOfFour.Prototype.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Prototype Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            Dim colormanager As New ColorManager()

            ' Initialize with standard colors
            colormanager(ColorType.Red) = New Color With {.Red = 255, .Blue = 0, .Green = 0}
            colormanager(ColorType.Green) = New Color With {.Red = 0, .Blue = 255, .Green = 0}
            colormanager(ColorType.Blue) = New Color With {.Red = 0, .Blue = 0, .Green = 255}

            ' User adds personalized colors
            colormanager(ColorType.Angry) = New Color With {.Red = 255, .Blue = 54, .Green = 0}
            colormanager(ColorType.Peace) = New Color With {.Red = 128, .Blue = 211, .Green = 128}
            colormanager(ColorType.Flame) = New Color With {.Red = 211, .Blue = 34, .Green = 20}

            ' User uses selected colors
            Dim color1 As Color = TryCast(colormanager(ColorType.Red).Clone(), Color)
            Dim color2 As Color = TryCast(colormanager(ColorType.Peace).Clone(), Color)

            ' Creates a "deep copy"
            Dim color3 As Color = TryCast(colormanager(ColorType.Flame).Clone(False), Color)

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'ConcretePrototype' class
	''' </summary>
	<Serializable> _
	Friend Class Color
		Implements ICloneable
		' Gets or sets red value
        Private _red As Byte
        Public Property Red() As Byte
            Get
                Return _red
            End Get
            Set(ByVal value As Byte)
                _red = value
            End Set
        End Property

		' Gets or sets green value
        Private _green As Byte
        Public Property Green() As Byte
            Get
                Return _green
            End Get
            Set(ByVal value As Byte)
                _green = value
            End Set
        End Property

		' Gets or sets blue channel
        Private _blue As Byte
        Public Property Blue() As Byte
            Get
                Return _blue
            End Get
            Set(ByVal value As Byte)
                _blue = value
            End Set
        End Property

		' Returns shallow or deep copy
		Public Function Clone(ByVal shallow As Boolean) As Object
			Return If(shallow, Clone(), DeepCopy())
		End Function

		' Creates a shallow copy
		Public Function Clone() As Object Implements ICloneable.Clone
			Console.WriteLine("Shallow copy of color RGB: {0,3},{1,3},{2,3}", Red, Green, Blue)

			Return Me.MemberwiseClone()
		End Function

		' Creates a deep copy
		Public Function DeepCopy() As Object
			Dim stream As New MemoryStream()
			Dim formatter As New BinaryFormatter()

			formatter.Serialize(stream, Me)
			stream.Seek(0, SeekOrigin.Begin)

			Dim copy As Object = formatter.Deserialize(stream)
			stream.Close()

			Console.WriteLine("*Deep*  copy of color RGB: {0,3},{1,3},{2,3}", (TryCast(copy, Color)).Red, (TryCast(copy, Color)).Green, (TryCast(copy, Color)).Blue)

			Return copy
		End Function
	End Class

	''' <summary>
	''' Type-safe prototype manager
	''' </summary>
	Friend Class ColorManager
		Private _colors As Dictionary(Of ColorType, Color) = New Dictionary(Of ColorType, Color)()

		' Gets or sets color
		Default Public Property Item(ByVal type As ColorType) As Color
			Get
				Return _colors(type)
			End Get
			Set(ByVal value As Color)
				_colors.Add(type, value)
			End Set
		End Property
	End Class

	''' <summary>
	''' Color type enumerations
	''' </summary>
	Friend Enum ColorType
		Red
		Green
		Blue

		Angry
		Peace
		Flame
	End Enum
End Namespace
