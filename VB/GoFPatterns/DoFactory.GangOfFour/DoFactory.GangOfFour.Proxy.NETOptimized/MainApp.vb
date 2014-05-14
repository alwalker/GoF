Imports Microsoft.VisualBasic
Imports System
Imports System.Runtime.Remoting

Namespace DoFactory.GangOfFour.Proxy.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Proxy Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            ' Create math proxy
            Dim proxy As New MathProxy()

            ' Do the math
            Console.WriteLine("4 + 2 = " & proxy.Add(4, 2))
            Console.WriteLine("4 - 2 = " & proxy.Sub(4, 2))
            Console.WriteLine("4 * 2 = " & proxy.Mul(4, 2))
            Console.WriteLine("4 / 2 = " & proxy.Div(4, 2))

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Subject' interface
	''' </summary>
	Public Interface IMath
		Function Add(ByVal x As Double, ByVal y As Double) As Double
		Function [Sub](ByVal x As Double, ByVal y As Double) As Double
		Function Mul(ByVal x As Double, ByVal y As Double) As Double
		Function Div(ByVal x As Double, ByVal y As Double) As Double
	End Interface

	''' <summary>
	''' The 'RealSubject' class
	''' </summary>
	Friend Class Math
		Inherits MarshalByRefObject
		Implements IMath
		Public Function Add(ByVal x As Double, ByVal y As Double) As Double Implements IMath.Add
			Return x + y
		End Function
		Public Function [Sub](ByVal x As Double, ByVal y As Double) As Double Implements IMath.Sub
			Return x - y
		End Function
		Public Function Mul(ByVal x As Double, ByVal y As Double) As Double Implements IMath.Mul
			Return x * y
		End Function
		Public Function Div(ByVal x As Double, ByVal y As Double) As Double Implements IMath.Div
			Return x / y
		End Function
	End Class

	''' <summary>
	''' The remote 'Proxy Object' class
	''' </summary>
	Friend Class MathProxy
		Implements IMath
		Private _math As Math

		' Constructor
		Public Sub New()
			' Create Math instance in a different AppDomain
			Dim ad As AppDomain = AppDomain.CreateDomain("MathDomain", Nothing, Nothing)

			Dim o As ObjectHandle = ad.CreateInstance("DoFactory.GangOfFour.Proxy.NETOptimized", "DoFactory.GangOfFour.Proxy.NETOptimized.Math")
			_math = CType(o.Unwrap(), Math)
		End Sub

		Public Function Add(ByVal x As Double, ByVal y As Double) As Double Implements IMath.Add
			Return _math.Add(x, y)
		End Function

		Public Function [Sub](ByVal x As Double, ByVal y As Double) As Double Implements IMath.Sub
			Return _math.Sub(x, y)
		End Function

		Public Function Mul(ByVal x As Double, ByVal y As Double) As Double Implements IMath.Mul
			Return _math.Mul(x, y)
		End Function

		Public Function Div(ByVal x As Double, ByVal y As Double) As Double Implements IMath.Div
			Return _math.Div(x, y)
		End Function
	End Class
End Namespace
