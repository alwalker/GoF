Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Transactions
Imports System.Configuration

Namespace Transactions
	''' <summary>
	''' TransactionDecorator wraps (decorates) the built-in TransactionScope class 
	''' and simply filters out the Microsoft Access transactions (the Jet engine 
	''' provider does not support 'local transactions'). Microsoft Access transactions 
	''' are therefore handled in simple autocommit mode. Additional database vendors
	''' can be handled accordingly.
	''' </summary>
	''' <remarks>
	''' GoF Design Pattern: Decorator.
	''' The Decorator Design Pattern is usually associated with additional functionality. 
	''' In this simple decorator the additional functionality is that is simply filters 
	''' for a particular database.
	''' </remarks>
	Public NotInheritable Class TransactionDecorator
		Implements IDisposable
		Private _scope As TransactionScope
		Private _transactionToUse As Transaction
		Private _scopeOption As TransactionScopeOption
		Private _scopeTimeout As TimeSpan
		Private _transactionOptions As TransactionOptions
		Private _interopOption As EnterpriseServicesInteropOption

		' Get database configuration from config.web file.
		Private Shared ReadOnly _dataProvider As String = ConfigurationManager.AppSettings.Get("DataProvider")

		' All constructors wrap (decorate) constructors from the 
		' built-in TransactionScope class.
		#Region "Overloaded Constructors"

		''' <summary>
		''' Initializes a new instance of TransactionDecorator. 
		''' Sets up the internal transaction scope variable.
		''' </summary>
		Public Sub New()
			If _dataProvider <> "System.Data.OleDb" Then
				_scope = New TransactionScope()
			End If
		End Sub

		''' <summary>
		''' Initializes a new instance of the TransactionScope class and sets the 
		''' specified transaction as the ambient transaction, so that transactional 
		''' work done inside the scope uses this transDoFactory.
		''' </summary>
		''' <param name="transactionToUse">Represents a transaction.</param>
		Public Sub New(ByVal transactionToUse As Transaction)
			_transactionToUse = transactionToUse

			If _dataProvider <> "System.Data.OleDb" Then
				_scope = New TransactionScope(_transactionToUse)
			End If
		End Sub

		''' <summary>
		''' Initializes a new instance of the TransactionScope class with the specified requirements. 
		''' </summary>
		''' <param name="scopeOption">Provides additional options for creating a TransactionDecorator.</param>
		Public Sub New(ByVal scopeOption As TransactionScopeOption)
			_scopeOption = scopeOption

			If _dataProvider <> "System.Data.OleDb" Then
				_scope = New TransactionScope(_scopeOption)
			End If
		End Sub

		''' <summary>
		''' Initializes a new instance of the TransactionScope class 
		''' with the specified timeout value, and sets the specified 
		''' transaction as the ambient transaction, so that transactional 
		''' work done inside the scope uses this transDoFactory. 
		''' </summary>
		''' <param name="transactionToUse">Represents a transaction.</param>
		''' <param name="scopeTimeout">The TimeSpan after which the transaction scope times out and aborts the transaction.</param>
		Public Sub New(ByVal transactionToUse As Transaction, ByVal scopeTimeout As TimeSpan)
			_transactionToUse = transactionToUse
			_scopeTimeout = scopeTimeout

			If _dataProvider <> "System.Data.OleDb" Then
				_scope = New TransactionScope(_transactionToUse, _scopeTimeout)
			End If
		End Sub

		''' <summary>
		''' Initializes a new instance of the TransactionScope class 
		''' with the specified timeout value and requirements.
		''' </summary>
		''' <param name="scopeOption">TransactionScopeOption enumeration that describes the transaction requirements associated with this transaction scope.</param>
		''' <param name="scopeTimeout">The TimeSpan after which the transaction scope times out and aborts the transaction.</param>
		Public Sub New(ByVal scopeOption As TransactionScopeOption, ByVal scopeTimeout As TimeSpan)
			_scopeOption = scopeOption
			_scopeTimeout = scopeTimeout

			If _dataProvider <> "System.Data.OleDb" Then
				_scope = New TransactionScope(_scopeOption, _scopeTimeout)
			End If
		End Sub

		''' <summary>
		''' Initializes a new instance of the TransactionScope class with the specified requirements.
		''' </summary>
		''' <param name="scopeOption">TransactionScopeOption enumeration that describes the transaction requirements associated with this transaction scope.</param>
		''' <param name="transactionOptions">A TransactionOptions structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		Public Sub New(ByVal scopeOption As TransactionScopeOption, ByVal transactionOptions As TransactionOptions)
			_scopeOption = scopeOption
			_transactionOptions = transactionOptions

			If _dataProvider <> "System.Data.OleDb" Then
				_scope = New TransactionScope(_scopeOption, _transactionOptions)
			End If
		End Sub

		''' <summary>
		''' Initializes a new instance of the TransactionScope class with the specified 
		''' timeout value and COM+ interoperability requirements, and sets the specified 
		''' transaction as the ambient transaction, so that transactional work done inside 
		''' the scope uses this transDoFactory. 
		''' </summary>
		''' <param name="transactionToUse">Represents a transaction.</param>
		''' <param name="scopeTimeout">The TimeSpan after which the transaction scope times out and aborts the transaction.</param>
		''' <param name="interopOption">An instance of the EnterpriseServicesInteropOption enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
		Public Sub New(ByVal transactionToUse As Transaction, ByVal scopeTimeout As TimeSpan, ByVal interopOption As EnterpriseServicesInteropOption)
			_transactionToUse = transactionToUse
			_scopeTimeout = scopeTimeout
			_interopOption = interopOption

			If _dataProvider <> "System.Data.OleDb" Then
				_scope = New TransactionScope(_transactionToUse, _scopeTimeout, _interopOption)
			End If
		End Sub

		''' <summary>
		''' Initializes a new instance of the TransactionScope class with the 
		''' specified scope and COM+ interoperability requirements, and 
		''' transaction options.
		''' </summary>
		''' <param name="scopeOption">TransactionScopeOption enumeration that describes the transaction requirements associated with this transaction scope.</param>
		''' <param name="transactionOptions">A TransactionOptions structure that describes the transaction options to use if a new transaction is created. If an existing transaction is used, the timeout value in this parameter applies to the transaction scope. If that time expires before the scope is disposed, the transaction is aborted.</param>
		''' <param name="interopOption">An instance of the EnterpriseServicesInteropOption enumeration that describes how the associated transaction interacts with COM+ transactions.</param>
		Public Sub New(ByVal scopeOption As TransactionScopeOption, ByVal transactionOptions As TransactionOptions, ByVal interopOption As EnterpriseServicesInteropOption)
			_scopeOption = scopeOption
			_transactionOptions = transactionOptions
			_interopOption = interopOption

			If _dataProvider <> "System.Data.OleDb" Then
				_scope = New TransactionScope(_scopeOption, _transactionOptions, _interopOption)
			End If
		End Sub

		#End Region

		''' <summary>
		''' Indicates that all operations within the scope are completed succesfully.
		''' </summary>
		Public Sub Complete()
			If _scope IsNot Nothing Then
				_scope.Complete()
			End If
		End Sub

		#Region "IDisposable members"

		''' <summary>
		''' Ends and disposes the transaction scope.
		''' </summary>
		Public Sub Dispose() Implements IDisposable.Dispose
			If _dataProvider <> "System.Data.OleDb" Then
				If _scope IsNot Nothing Then
					_scope.Dispose()
				End If
			End If
		End Sub

		#End Region
	End Class
End Namespace
