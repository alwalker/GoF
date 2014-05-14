Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Builder.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Builder Design Pattern.
	''' </summary>
	Public Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Public Shared Sub Main()

            Dim builder As VehicleBuilder

            ' Create shop with vehicle builders
            Dim shop As New Shop()

            ' Create builders and display vehicle
            builder = New ScooterBuilder()
            shop.Construct(builder)
            builder.Vehicle.Show()

            builder = New CarBuilder()
            shop.Construct(builder)
            builder.Vehicle.Show()

            builder = New MotorCycleBuilder()
            shop.Construct(builder)
            builder.Vehicle.Show()

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Director' class
	''' </summary>
	Friend Class Shop
		' Builder uses a complex series of steps
		Public Sub Construct(ByVal vehicleBuilder As VehicleBuilder)
			vehicleBuilder.BuildFrame()
			vehicleBuilder.BuildEngine()
			vehicleBuilder.BuildWheels()
			vehicleBuilder.BuildDoors()
		End Sub
	End Class

	''' <summary>
	''' The 'Builder' abstract class
	''' </summary>
	Friend MustInherit Class VehicleBuilder
        Protected theVehicle As Vehicle

        ' Constructor
        Public Sub New(ByVal vehicleType As VehicleType)
            theVehicle = New Vehicle(vehicleType)
        End Sub

        ' Property
        Public ReadOnly Property Vehicle() As Vehicle
            Get
                Return TheVehicle
            End Get
        End Property

        Public MustOverride Sub BuildFrame()
        Public MustOverride Sub BuildEngine()
        Public MustOverride Sub BuildWheels()
        Public MustOverride Sub BuildDoors()
    End Class

    ''' <summary>
    ''' The 'ConcreteBuilder1' class
    ''' </summary>
    Friend Class MotorCycleBuilder
        Inherits VehicleBuilder
        ' Invoke base class constructor
        Public Sub New()
            MyBase.New(VehicleType.MotorCycle)
        End Sub

        Public Overrides Sub BuildFrame()
            theVehicle(PartType.Frame) = "MotorCycle Frame"
        End Sub

        Public Overrides Sub BuildEngine()
            theVehicle(PartType.Engine) = "500 cc"
        End Sub

        Public Overrides Sub BuildWheels()
            theVehicle(PartType.Wheel) = "2"
        End Sub

        Public Overrides Sub BuildDoors()
            theVehicle(PartType.Door) = "0"
        End Sub
    End Class

    ''' <summary>
    ''' The 'ConcreteBuilder2' class
    ''' </summary>
    Friend Class CarBuilder
        Inherits VehicleBuilder
        ' Invoke base class constructor
        Public Sub New()
            MyBase.New(VehicleType.Car)
        End Sub

        Public Overrides Sub BuildFrame()
            theVehicle(PartType.Frame) = "Car Frame"
        End Sub

        Public Overrides Sub BuildEngine()
            theVehicle(PartType.Engine) = "2500 cc"
        End Sub

        Public Overrides Sub BuildWheels()
            theVehicle(PartType.Wheel) = "4"
        End Sub

        Public Overrides Sub BuildDoors()
            theVehicle(PartType.Door) = "4"
        End Sub
    End Class

    ''' <summary>
    ''' The 'ConcreteBuilder3' class
    ''' </summary>
    Friend Class ScooterBuilder
        Inherits VehicleBuilder
        ' Invoke base class constructor
        Public Sub New()
            MyBase.New(VehicleType.Scooter)
        End Sub

        Public Overrides Sub BuildFrame()
            theVehicle(PartType.Frame) = "Scooter Frame"
        End Sub

        Public Overrides Sub BuildEngine()
            theVehicle(PartType.Engine) = "50 cc"
        End Sub

        Public Overrides Sub BuildWheels()
            theVehicle(PartType.Wheel) = "2"
        End Sub

        Public Overrides Sub BuildDoors()
            theVehicle(PartType.Door) = "0"
		End Sub
	End Class

	''' <summary>
	''' The 'Product' class
	''' </summary>
	Friend Class Vehicle
		Private _vehicleType As VehicleType

		Private _parts As Dictionary(Of PartType, String) = New Dictionary(Of PartType, String)()

		' Constructor
		Public Sub New(ByVal vehicleType As VehicleType)
			Me._vehicleType = vehicleType
		End Sub

		' Indexer 
		Default Public Property Item(ByVal key As PartType) As String
			Get
				Return _parts(key)
			End Get
			Set(ByVal value As String)
				_parts(key) = value
			End Set
		End Property

		Public Sub Show()
			Console.WriteLine(Constants.vbLf & "---------------------------")
			Console.WriteLine("Vehicle Type: {0}", _vehicleType)
			Console.WriteLine(" Frame  : {0}", Me(PartType.Frame))
			Console.WriteLine(" Engine : {0}", Me(PartType.Engine))
			Console.WriteLine(" #Wheels: {0}", Me(PartType.Wheel))
			Console.WriteLine(" #Doors : {0}", Me(PartType.Door))
		End Sub
	End Class

	''' <summary>
	''' Part type enumeration
	''' </summary>
	Public Enum PartType
		Frame
		Engine
		Wheel
		Door
	End Enum

	''' <summary>
	''' Vehicle type enumeration
	''' </summary>
	Public Enum VehicleType
		Car
		Scooter
		MotorCycle
	End Enum
End Namespace

