Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic

Namespace DoFactory.GangOfFour.Singleton.NETOptimized
	''' <summary>
	''' MainApp startup class for .NET optimized 
	''' Singleton Design Pattern.
	''' </summary>
	Friend Class MainApp
		''' <summary>
		''' Entry point into console application.
		''' </summary>
        Shared Sub Main()

            Dim b1 As LoadBalancer = LoadBalancer.GetLoadBalancer()
            Dim b2 As LoadBalancer = LoadBalancer.GetLoadBalancer()
            Dim b3 As LoadBalancer = LoadBalancer.GetLoadBalancer()
            Dim b4 As LoadBalancer = LoadBalancer.GetLoadBalancer()

            ' Confirm these are the same instance
            If b1 Is b2 AndAlso b2 Is b3 AndAlso b3 Is b4 Then
                Console.WriteLine("Same instance" & Constants.vbLf)
            End If

            ' Load balance 15 requests for a server
            Dim balancer As LoadBalancer = LoadBalancer.GetLoadBalancer()
            For i As Integer = 0 To 14
                Dim serverName As String = balancer.NextServer.Name
                Console.WriteLine("Dispatch request to: " & serverName)
            Next i

            ' Wait for user
            Console.ReadKey()
        End Sub
	End Class

	''' <summary>
	''' The 'Singleton' class
	''' </summary>
	Friend NotInheritable Class LoadBalancer
		' Static members are 'eagerly initialized', that is, 
		' immediately when class is loaded for the first time.
		' .NET guarantees thread safety for static initialization
		Private Shared ReadOnly _instance As New LoadBalancer()

		' Type-safe generic list of servers
		Private _servers As List(Of Server)
		Private _random As New Random()

		' Note: constructor is 'private'
		Private Sub New()
			' Load list of available servers
			_servers = New List(Of Server) (New Server() {New Server With {.Name = "ServerI", .IP = "120.14.220.18"}, New Server With {.Name = "ServerII", .IP = "120.14.220.19"}, New Server With {.Name = "ServerIII", .IP = "120.14.220.20"}, New Server With {.Name = "ServerIV", .IP = "120.14.220.21"}, New Server With {.Name = "ServerV", .IP = "120.14.220.22"}})
		End Sub

		Public Shared Function GetLoadBalancer() As LoadBalancer
			Return _instance
		End Function

		' Simple, but effective load balancer
		Public ReadOnly Property NextServer() As Server
			Get
				Dim r As Integer = _random.Next(_servers.Count)
				Return _servers(r)
			End Get
		End Property
	End Class

	''' <summary>
	''' Represents a server machine
	''' </summary>
	Friend Class Server
		' Gets or sets server name
        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

		' Gets or sets server IP address
        Private _IP As String
        Public Property IP() As String
            Get
                Return _IP
            End Get
            Set(ByVal value As String)
                _IP = value
            End Set
        End Property
	End Class
End Namespace
