Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.SessionState
Imports System.Xml.Linq
Imports System.Net.Mail

Imports Log

Namespace ASPNETWebApplication
	Public Class [Global]
		Inherits System.Web.HttpApplication
		''' <summary>
		''' Event handler for application start event. Initializes logging.
		''' </summary>
		Protected Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
			' Initialize logging facility
			InitializeLogger()
		End Sub

		''' <summary>
		''' Initializes logging facility with severity level and observer(s).
		''' Private helper method.
		''' </summary>
		Private Sub InitializeLogger()
			' Read and assign application wide logging severity
			Dim severity As String = ConfigurationManager.AppSettings.Get("LogSeverity")
			SingletonLogger.Instance.Severity = CType(System.Enum.Parse(GetType(LogSeverity), severity, True), LogSeverity)

			' Send log messages to database (observer pattern)
			Dim log As ILog = New ObserverLogToDatabase()
			SingletonLogger.Instance.Attach(log)

			' Send log messages to email (observer pattern)
			Dim From As String = "notification@yourcompany.com"
			Dim [to] As String = "webmaster@yourcompany.com"
			Dim subject As String = "Webmaster: please review"
			Dim body As String = "email text"
			Dim smtpClient As New SmtpClient("mail.yourcompany.com")

			log = New ObserverLogToEmail(From, [to], subject, body, smtpClient)
			SingletonLogger.Instance.Attach(log)

			' Send log messages to a file
			log = New ObserverLogToFile("C:\Temp\DoFactory.log")
			SingletonLogger.Instance.Attach(log)

			' Send log message to event log
			log = New ObserverLogToEventlog()
			SingletonLogger.Instance.Attach(log)
		End Sub

		''' <summary>
		''' This is the last-resort exception handler.
		''' It uses te logging infrastructure to log the error details.
		''' The application will then be redirected according to the 
		''' customErrors configuration in web.config.
		''' </summary>
		''' <remarks>
		''' Logging is commented out. Be sure the application has privilege to 
		''' to write to a file, send email, or add to the event log. Only then 
		''' turn logging on.
		''' </remarks>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Protected Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
			Dim ex As Exception = Server.GetLastError().GetBaseException()

			' NOTE: commented out because the site needs privileges to logging resources.
			' SingletonLogger.Instance.Error(ex.Message);

			' <customErrors ..> in web config will now redirect.
		End Sub
	End Class
End Namespace