Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Configuration
Imports System.Data
Imports System.Data.Common

Namespace DataObjects.AdoNet
	''' <summary>
	''' Class that manages all lower level ADO.NET data base access.
	''' </summary>
	''' <remarks>
	''' GoF Design Patterns: Singleton, Factory, Proxy.
	''' 
	''' This class is the 'swiss army knife' of data access. It handles all  
	''' database access details and shields its complexity from its clients.
	''' 
	''' The Factory Design pattern is used to create database specific instances
	''' of Connection objects, Command objects, etc.
	''' 
	''' This class is like a Singleton -- it is a static class (Shared in VB) and 
	''' therefore only one 'instance' ever will exist.
	''' 
	''' This class is a Proxy in that it 'stands in' for the actual DbProviderFactory.
	''' </remarks>
	Public NotInheritable Class Db
		' Note: Static initializers are thread safe.
		' If this class had a static constructor then these static variables 
		' would need to be initialized there.
		Private Shared ReadOnly dataProvider As String = ConfigurationManager.AppSettings.Get("DataProvider")
		Private Shared ReadOnly factory As DbProviderFactory = DbProviderFactories.GetFactory(dataProvider)

		Private Shared ReadOnly connectionStringName As String = ConfigurationManager.AppSettings.Get("ConnectionStringName")
		Private Shared ReadOnly connectionString As String = ConfigurationManager.ConnectionStrings(connectionStringName).ConnectionString

		#Region "Data Update handlers"

		''' <summary>
		''' Executes Update statements in the database.
		''' </summary>
		''' <param name="sql">Sql statement.</param>
		''' <returns>Number of rows affected.</returns>
		Private Sub New()
		End Sub
		Public Shared Function Update(ByVal sql As String) As Integer
			Using connection As DbConnection = factory.CreateConnection()
				connection.ConnectionString = connectionString

				Using command As DbCommand = factory.CreateCommand()
					command.Connection = connection
					command.CommandText = sql

					connection.Open()
					Return command.ExecuteNonQuery()
				End Using
			End Using
		End Function


		''' <summary>
		''' Executes Insert statements in the database. Optionally returns new identifier.
		''' </summary>
		''' <param name="sql">Sql statement.</param>
		''' <param name="getId">Value indicating whether newly generated identity is returned.</param>
		''' <returns>Newly generated identity value (autonumber value).</returns>
		Public Shared Function Insert(ByVal sql As String, ByVal getId As Boolean) As Integer
			Using connection As DbConnection = factory.CreateConnection()
				connection.ConnectionString = connectionString

				Using command As DbCommand = factory.CreateCommand()
					command.Connection = connection
					command.CommandText = sql

					connection.Open()
					command.ExecuteNonQuery()

					Dim id As Integer = -1

					' Check if new identity is needed.
					If getId Then
						' Execute db specific autonumber or identity retrieval code
						' SELECT SCOPE_IDENTITY() -- for SQL Server
						' SELECT @@IDENTITY -- for MS Access
						' SELECT MySequence.NEXTVAL FROM DUAL -- for Oracle
						Dim identitySelect As String
						Select Case dataProvider
							' Access
							Case "System.Data.OleDb"
								identitySelect = "SELECT @@IDENTITY"
							' Sql Server
							Case "System.Data.SqlClient"
								identitySelect = "SELECT SCOPE_IDENTITY()"
							' Oracle
							Case "System.Data.OracleClient"
								identitySelect = "SELECT MySequence.NEXTVAL FROM DUAL"
							Case Else
								identitySelect = "SELECT @@IDENTITY"
						End Select
						command.CommandText = identitySelect
						id = Integer.Parse(command.ExecuteScalar().ToString())
					End If
					Return id
				End Using
			End Using
		End Function

		''' <summary>
		''' Executes Insert statements in the database.
		''' </summary>
		''' <param name="sql">Sql statement.</param>
		Public Shared Sub Insert(ByVal sql As String)
			Insert(sql, False)
		End Sub

		#End Region

		#Region "Data Retrieve Handlers"

		''' <summary>
		''' Populates a DataSet according to a Sql statement.
		''' </summary>
		''' <param name="sql">Sql statement.</param>
		''' <returns>Populated DataSet.</returns>
		Public Shared Function GetDataSet(ByVal sql As String) As DataSet
			Using connection As DbConnection = factory.CreateConnection()
				connection.ConnectionString = connectionString

				Using command As DbCommand = factory.CreateCommand()
					command.Connection = connection
					command.CommandType = CommandType.Text
					command.CommandText = sql

					Using adapter As DbDataAdapter = factory.CreateDataAdapter()
						adapter.SelectCommand = command

						Dim ds As New DataSet()
						adapter.Fill(ds)

						Return ds
					End Using
				End Using
			End Using
		End Function

		''' <summary>
		''' Populates a DataTable according to a Sql statement.
		''' </summary>
		''' <param name="sql">Sql statement.</param>
		''' <returns>Populated DataTable.</returns>
		Public Shared Function GetDataTable(ByVal sql As String) As DataTable
			Using connection As DbConnection = factory.CreateConnection()
				connection.ConnectionString = connectionString

				Using command As DbCommand = factory.CreateCommand()
					command.Connection = connection
					command.CommandType = CommandType.Text
					command.CommandText = sql

					Using adapter As DbDataAdapter = factory.CreateDataAdapter()
						adapter.SelectCommand = command

						Dim dt As New DataTable()
						adapter.Fill(dt)

						Return dt
					End Using
				End Using
			End Using
			'using (SqlConnection connection = new SqlConnection())
			'{
			'    //return GetDataSet(sql).Tables[0];
			'    connection.ConnectionString = connectionString;

			'    using (SqlCommand command = new SqlCommand())
			'    {
			'        command.Connection = connection;
			'        command.CommandType = CommandType.Text;
			'        command.CommandText = sql;

			'        using (DbDataAdapter adapter = new SqlDataAdapter())
			'        {
			'            adapter.SelectCommand = command;

			'            DataTable dt = new DataTable();
			'            adapter.Fill(dt);

			'            return dt;
			'        }
			'    }
			'}
		End Function

		''' <summary>
		''' Populates a DataRow according to a Sql statement.
		''' </summary>
		''' <param name="sql">Sql statement.</param>
		''' <returns>Populated DataRow.</returns>
		Public Shared Function GetDataRow(ByVal sql As String) As DataRow
			Dim row As DataRow = Nothing

			Dim dt As DataTable = GetDataTable(sql)
			If dt.Rows.Count > 0 Then
				row = dt.Rows(0)
			End If

			Return row
		End Function

		''' <summary>
		''' Executes a Sql statement and returns a scalar value.
		''' </summary>
		''' <param name="sql">Sql statement.</param>
		''' <returns>Scalar value.</returns>
		Public Shared Function GetScalar(ByVal sql As String) As Object
			Using connection As DbConnection = factory.CreateConnection()
				connection.ConnectionString = connectionString

				Using command As DbCommand = factory.CreateCommand()
					command.Connection = connection
					command.CommandText = sql

					connection.Open()
					Return command.ExecuteScalar()
				End Using
			End Using
		End Function

		#End Region

		#Region "Utility methods"

		''' <summary>
		''' Escapes an input string for database processing, that is, 
		''' surround it with quotes and change any quote in the string to 
		''' two adjacent quotes (i.e. escape it). 
		''' If input string is null or empty a NULL string is returned.
		''' </summary>
		''' <param name="s">Input string.</param>
		''' <returns>Escaped output string.</returns>
		Public Shared Function Escape(ByVal s As String) As String
			If String.IsNullOrEmpty(s) Then
				Return "NULL"
			Else
				Return "'" & s.Trim().Replace("'", "''") & "'"
			End If
		End Function

		''' <summary>
		''' Escapes an input string for database processing, that is, 
		''' surround it with quotes and change any quote in the string to 
		''' two adjacent quotes (i.e. escape it). 
		''' Also trims string at a given maximum length.
		''' If input string is null or empty a NULL string is returned.
		''' </summary>
		''' <param name="s">Input string.</param>
		''' <param name="maxLength">Maximum length of output string.</param>
		''' <returns>Escaped output string.</returns>
		Public Shared Function Escape(ByVal s As String, ByVal maxLength As Integer) As String
			If String.IsNullOrEmpty(s) Then
				Return "NULL"
			Else
				s = s.Trim()
				If s.Length > maxLength Then
					s = s.Substring(0, maxLength - 1)
				End If
				Return "'" & s.Trim().Replace("'", "''") & "'"
			End If
		End Function

		#End Region
	End Class
End Namespace
