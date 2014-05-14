Imports Microsoft.VisualBasic
Imports System

Namespace DoFactory.HeadFirst.Decorator.Starbuzz
    Friend Class StarbuzzCoffee
        Shared Sub Main(ByVal args() As String)

            Dim beverage As Beverage = New Espresso()

            Console.WriteLine(beverage.Description & " $" & beverage.Cost)

            Dim beverage2 As Beverage = New DarkRoast()
            beverage2 = New Mocha(beverage2)
            beverage2 = New Mocha(beverage2)
            beverage2 = New Whip(beverage2)
            Console.WriteLine(beverage2.Description & " $" & beverage2.Cost)

            Dim beverage3 As Beverage = New HouseBlend()
            beverage3 = New Soy(beverage3)
            beverage3 = New Mocha(beverage3)
            beverage3 = New Whip(beverage3)
            Console.WriteLine(beverage3.Description & " $" & beverage3.Cost)

            ' Wait for user
            Console.ReadKey()
        End Sub
    End Class

#Region "Beverage"

    Public Class Beverage
        Private _description As String
        Public Overridable Property Description() As String
            Get
                Return _description
            End Get
            Set(ByVal value As String)
                _description = value
            End Set
        End Property
        Private _cost As Double
        Public Overridable Property Cost() As Double
            Get
                Return _cost
            End Get
            Set(ByVal value As Double)
                _cost = value
            End Set
        End Property
    End Class

    Public Class DarkRoast
        Inherits Beverage
        Public Sub New()
            Description = "Dark Roast Coffee"
            Cost = 0.99
        End Sub
    End Class

    Public Class Decaf
        Inherits Beverage
        Public Sub New()
            Description = "Decaf Coffee"
            Cost = 1.05
        End Sub
    End Class

    Public Class Espresso
        Inherits Beverage
        Public Sub New()
            Description = "Espresso"
            Cost = 1.99
        End Sub
    End Class

    Public Class HouseBlend
        Inherits Beverage
        Public Sub New()
            Description = "House Blend Coffee"
            Cost = 0.89
        End Sub
    End Class

#End Region

#Region "CondimentDecorator"

    Public MustInherit Class CondimentDecorator
        Inherits Beverage
    End Class

    Public Class Whip
        Inherits CondimentDecorator
        Private _beverage As Beverage

        Public Sub New(ByVal beverage As Beverage)
            Me._beverage = beverage
        End Sub

        Public Overrides Property Description() As String
            Get
                Return _beverage.Description & ", Whip"
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        Public Overrides Property Cost() As Double
            Get
                Return 0.1 + _beverage.Cost
            End Get
            Set(ByVal value As Double)
            End Set
        End Property
    End Class

    Public Class Milk
        Inherits CondimentDecorator
        Private _beverage As Beverage

        Public Sub New(ByVal beverage As Beverage)
            Me._beverage = beverage
        End Sub

        Public Overrides Property Description() As String
            Get
                Return _beverage.Description & ", Milk"
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        Public Overrides Property Cost() As Double
            Get
                Return 0.1 + _beverage.Cost
            End Get
            Set(ByVal value As Double)
            End Set
        End Property
    End Class

    Public Class Mocha
        Inherits CondimentDecorator
        Private _beverage As Beverage

        Public Sub New(ByVal beverage As Beverage)
            Me._beverage = beverage
        End Sub

        Public Overrides Property Description() As String
            Get
                Return _beverage.Description & ", Mocha"
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        Public Overrides Property Cost() As Double
            Get
                Return 0.2 + _beverage.Cost
            End Get
            Set(ByVal value As Double)
            End Set
        End Property
    End Class

    Public Class Soy
        Inherits CondimentDecorator
        Private _beverage As Beverage

        Public Sub New(ByVal beverage As Beverage)
            Me._beverage = beverage
        End Sub

        Public Overrides Property Description() As String
            Get
                Return _beverage.Description & ", Soy"
            End Get
            Set(ByVal value As String)
            End Set
        End Property

        Public Overrides Property Cost() As Double
            Get
                Return 0.15 + _beverage.Cost
            End Get
            Set(ByVal value As Double)
            End Set
        End Property
    End Class

#End Region
End Namespace
