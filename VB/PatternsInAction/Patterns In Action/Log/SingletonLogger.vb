Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Log
	''' <summary>
	''' Singleton logger class through which all log events are processed.
	''' </summary>
	''' <remarks>
	''' GoF Design Patterns: Singleton, Observer.
	''' </remarks>
	Public NotInheritable Class SingletonLogger
		''' <summary>
		''' Delegate event handler that hooks up requests.
		''' </summary>
		''' <param name="sender">Sender of the event.</param>
		''' <param name="e">Event arguments.</param>
		''' <remarks>
		''' GoF Design Pattern: Observer, Singleton.
		''' The Observer Design Pattern allows Observer classes to attach itself to 
		''' this Logger class and be notified if certain events occur. 
		''' 
		''' The Singleton Design Pattern allows the application to have just one
		''' place that is aware of the application-wide LogSeverity setting.
		''' </remarks>
		Public Delegate Sub LogEventHandler(ByVal sender As Object, ByVal e As LogEventArgs)

		''' <summary>
		''' The Log event.
		''' </summary>
		Public Event Log As LogEventHandler

		#Region "The Singleton definition"

		''' <summary>
		''' The one and only Singleton Logger instance. 
		''' </summary>
		Private Shared ReadOnly instance_Renamed As New SingletonLogger()

		''' <summary>
		''' Private constructor. Initializes default severity to "Error".
		''' </summary>
		Private Sub New()
			' Default severity is Error level
			Severity = LogSeverity.Error
		End Sub

		''' <summary>
		''' Gets the instance of the singleton logger object.
		''' </summary>
		Public Shared ReadOnly Property Instance() As SingletonLogger
			Get
				Return instance_Renamed
			End Get
		End Property

		#End Region

		Private _severity As LogSeverity

		' These booleans are used strictly for performance improvement.
		Private _isDebug As Boolean
		Private _isInfo As Boolean
		Private _isWarning As Boolean
		Private _isError As Boolean
		Private _isFatal As Boolean

		''' <summary>
		''' Gets and sets the severity level of logging activity.
		''' </summary>
		Public Property Severity() As LogSeverity
			Get
				Return _severity
			End Get
			Set(ByVal value As LogSeverity)
				_severity = value

				' Set booleans to help improve performance
                Dim theSeverity As Integer = CInt(Fix(_severity))

                _isDebug = If((CInt(Fix(LogSeverity.Debug))) >= theSeverity, True, False)
                _isInfo = If((CInt(Fix(LogSeverity.Info))) >= theSeverity, True, False)
                _isWarning = If((CInt(Fix(LogSeverity.Warning))) >= theSeverity, True, False)
                _isError = If((CInt(Fix(LogSeverity.Error))) >= theSeverity, True, False)
                _isFatal = If((CInt(Fix(LogSeverity.Fatal))) >= theSeverity, True, False)
			End Set
		End Property

		''' <summary>
		''' Log a message when severity level is "Debug" or higher.
		''' </summary>
		''' <param name="message">Log message</param>
		Public Sub Debug(ByVal message As String)
			If _isDebug Then
				Debug(message, Nothing)
			End If
		End Sub

		''' <summary>
		''' Log a message when severity level is "Debug" or higher.
		''' </summary>
		''' <param name="message">Log message.</param>
		''' <param name="exception">Inner exception.</param>
		Public Sub Debug(ByVal message As String, ByVal exception As Exception)
			If _isDebug Then
				OnLog(New LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now))
			End If
		End Sub

		''' <summary>
		''' Log a message when severity level is "Info" or higher.
		''' </summary>
		''' <param name="message">Log message</param>
		Public Sub Info(ByVal message As String)
			If _isInfo Then
				Info(message, Nothing)
			End If
		End Sub

		''' <summary>
		''' Log a message when severity level is "Info" or higher.
		''' </summary>
		''' <param name="message">Log message.</param>
		''' <param name="exception">Inner exception.</param>
		Public Sub Info(ByVal message As String, ByVal exception As Exception)
			If _isInfo Then
				OnLog(New LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now))
			End If
		End Sub

		''' <summary>
		''' Log a message when severity level is "Warning" or higher.
		''' </summary>
		''' <param name="message">Log message.</param>
		Public Sub Warning(ByVal message As String)
			If _isWarning Then
				Warning(message, Nothing)
			End If
		End Sub

		''' <summary>
		''' Log a message when severity level is "Warning" or higher.
		''' </summary>
		''' <param name="message">Log message.</param>
		''' <param name="exception">Inner exception.</param>
		Public Sub Warning(ByVal message As String, ByVal exception As Exception)
			If _isWarning Then
				OnLog(New LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now))
			End If
		End Sub

		''' <summary>
		''' Log a message when severity level is "Error" or higher.
		''' </summary>
		''' <param name="message">Log message</param>
		Public Sub [Error](ByVal message As String)
			If _isError Then
				[Error](message, Nothing)
			End If
		End Sub

		''' <summary>
		''' Log a message when severity level is "Error" or higher.
		''' </summary>
		''' <param name="message">Log message.</param>
		''' <param name="exception">Inner exception.</param>
		Public Sub [Error](ByVal message As String, ByVal exception As Exception)
			If _isError Then
				OnLog(New LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now))
			End If
		End Sub

		''' <summary>
		''' Log a message when severity level is "Fatal"
		''' </summary>
		''' <param name="message">Log message</param>
		Public Sub Fatal(ByVal message As String)
			If _isFatal Then
				Fatal(message, Nothing)
			End If
		End Sub

		''' <summary>
		''' Log a message when severity level is "Fatal"
		''' </summary>
		''' <param name="message">Log message.</param>
		''' <param name="exception">Inner exception.</param>
		Public Sub Fatal(ByVal message As String, ByVal exception As Exception)
			If _isFatal Then
				OnLog(New LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now))
			End If
		End Sub

		''' <summary>
		''' Invoke the Log event.
		''' </summary>
		''' <param name="e">Log event parameters.</param>
		Public Sub OnLog(ByVal e As LogEventArgs)
			RaiseEvent Log(Me, e)
		End Sub

		''' <summary>
		''' Attach a listening observer logging device to logger.
		''' </summary>
		''' <param name="observer">Observer (listening device).</param>
		Public Sub Attach(ByVal observer As ILog)
			AddHandler Log, AddressOf observer.Log
		End Sub

		''' <summary>
		''' Detach a listening observer logging device from logger.
		''' </summary>
		''' <param name="observer">Observer (listening device).</param>
		Public Sub Detach(ByVal observer As ILog)
			RemoveHandler Log, AddressOf observer.Log
		End Sub
	End Class
End Namespace
