using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using System.Net.Mail;

using Log;

namespace ASPNETWebApplication
{
    public class Global : System.Web.HttpApplication
    {
        /// <summary>
        /// Event handler for application start event. Initializes logging.
        /// </summary>
        protected void Application_Start(Object sender, EventArgs e)
        {
            // Initialize logging facility
            InitializeLogger();
        }

        /// <summary>
        /// Initializes logging facility with severity level and observer(s).
        /// Private helper method.
        /// </summary>
        private void InitializeLogger()
        {
            // Read and assign application wide logging severity
            string severity = ConfigurationManager.AppSettings.Get("LogSeverity");
            SingletonLogger.Instance.Severity = (LogSeverity)Enum.Parse(typeof(LogSeverity), severity, true);

            // Send log messages to database (observer pattern)
            ILog log = new ObserverLogToDatabase();
            SingletonLogger.Instance.Attach(log);

            // Send log messages to email (observer pattern)
            string from = "notification@yourcompany.com";
            string to = "webmaster@yourcompany.com";
            string subject = "Webmaster: please review";
            string body = "email text";
            SmtpClient smtpClient = new SmtpClient("mail.yourcompany.com");

            log = new ObserverLogToEmail(from, to, subject, body, smtpClient);
            SingletonLogger.Instance.Attach(log);

            // Send log messages to a file
            log = new ObserverLogToFile(@"C:\Temp\DoFactory.log");
            SingletonLogger.Instance.Attach(log);

            // Send log message to event log
            log = new ObserverLogToEventlog();
            SingletonLogger.Instance.Attach(log);
        }

        /// <summary>
        /// This is the last-resort exception handler.
        /// It uses te logging infrastructure to log the error details.
        /// The application will then be redirected according to the 
        /// customErrors configuration in web.config.
        /// </summary>
        /// <remarks>
        /// Logging is commented out. Be sure the application has privilege to 
        /// to write to a file, send email, or add to the event log. Only then 
        /// turn logging on.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError().GetBaseException();

            // NOTE: commented out because the site needs privileges to logging resources.
            // SingletonLogger.Instance.Error(ex.Message);

            // <customErrors ..> in web config will now redirect.
        }
    }
}