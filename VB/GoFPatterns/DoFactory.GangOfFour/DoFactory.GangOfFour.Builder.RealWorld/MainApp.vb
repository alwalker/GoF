Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Builder.RealWorld
	''' <summary>
	''' MainApp startup class for Real-World 
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

            ' Construct and display vehicles
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

        ' Gets vehicle instance
        Public ReadOnly Property Vehicle() As Vehicle
            Get
                Return theVehicle
            End Get
        End Property

        ' Abstract build methods
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
        Public Sub New()
            theVehicle = New Vehicle("MotorCycle")
        End Sub

        Public Overrides Sub BuildFrame()
            theVehicle("frame") = "MotorCycle Frame"
        End Sub

        Public Overrides Sub BuildEngine()
            theVehicle("engine") = "500 cc"
        End Sub

        Public Overrides Sub BuildWheels()
            theVehicle("wheels") = "2"
        End Sub

        Public Overrides Sub BuildDoors()
            theVehicle("doors") = "0"
        End Sub
    End Class


    ''' <summary>
    ''' The 'ConcreteBuilder2' class
    ''' </summary>
    Friend Class CarBuilder
        Inherits VehicleBuilder
        Public Sub New()
            theVehicle = New Vehicle("Car")
        End Sub

        Public Overrides Sub BuildFrame()
            theVehicle("frame") = "Car Frame"
        End Sub

        Public Overrides Sub BuildEngine()
            theVehicle("engine") = "2500 cc"
        End Sub

        Public Overrides Sub BuildWheels()
            theVehicle("wheels") = "4"
        End Sub

        Public Overrides Sub BuildDoors()
            theVehicle("doors") = "4"
        End Sub
    End Class

    ''' <summary>
    ''' The 'ConcreteBuilder3' class
    ''' </summary>
    Friend Class ScooterBuilder
        Inherits VehicleBuilder
        Public Sub New()
            theVehicle = New Vehicle("Scooter")
        End Sub

        Public Overrides Sub BuildFrame()
            theVehicle("frame") = "Scooter Frame"
        End Sub

        Public Overrides Sub BuildEngine()
            theVehicle("engine") = "50 cc"
        End Sub

        Public Overrides Sub BuildWheels()
            theVehicle("wheels") = "2"
        End Sub

        Public Overrides Sub BuildDoors()
            theVehicle("doors") = "0"
		End Sub
	End Class

	''' <summary>
	''' The 'Product' class
	''' </summary>
	Friend Class Vehicle
		Private _vehicleType As String
		Private _parts As Dictionary(Of String,String) = New Dictionary(Of String,String)()

		' Constructor
		Public Sub New(ByVal vehicleType As String)
			Me._vehicleType = vehicleType
		End Sub

		' Indexer
		Default Public Property Item(ByVal key As String) As String
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
			Console.WriteLine(" Frame  : {0}", _parts("frame"))
			Console.WriteLine(" Engine : {0}", _parts("engine"))
			Console.WriteLine(" #Wheels: {0}", _parts("wheels"))
			Console.WriteLine(" #Doors : {0}", _parts("doors"))
		End Sub
	End Class
End Namespace

