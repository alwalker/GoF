Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms

Imports WindowsFormsPresenter
Imports WindowsFormsView

Namespace WindowsFormsApplication
	''' <summary>
	''' This form is used to add new customer data or edit 
	''' existing customer data. 
	''' </summary>
	Partial Public Class FormCustomer
		Inherits Form
		Implements ICustomerView

        ' The customer Presenter.
		Private _customerPresenter As CustomerPresenter
		Private _cancelClose As Boolean

		''' <summary>
		''' Default constructor of FormCustomer.
		''' </summary>
		Public Sub New()
			InitializeComponent()
			AddHandler Me.Closing, AddressOf FormCustomer_Closing

			' Initialize Presenter.
			_customerPresenter = New CustomerPresenter(Me)
		End Sub

		''' <summary>
		''' Gets or sets customer id
		''' </summary>
        Private _customerId As Integer
        Public Property CustomerId() As Integer Implements ICustomerView.CustomerId
            Get
                Return _customerId
            End Get
            Set(ByVal value As Integer)
                _customerId = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets customer version.
		''' </summary>
        Private _version As String
        Public Property Version() As String Implements ICustomerView.Version
            Get
                Return _version
            End Get
            Set(ByVal value As String)
                _version = value
            End Set
        End Property

		''' <summary>
		''' Gets or sets customer (company) name.
		''' </summary>
		Public Property Company() As String Implements ICustomerView.Company
			Get
				Return textBoxCompany.Text.Trim()
			End Get
			Set(ByVal value As String)
				textBoxCompany.Text = value
			End Set
		End Property

		''' <summary>
		''' Gets or sets customer city.
		''' </summary>
		Public Property City() As String Implements ICustomerView.City
			Get
				Return textBoxCity.Text.Trim()
			End Get
			Set(ByVal value As String)
				textBoxCity.Text = value
			End Set
		End Property

		''' <summary>
		''' Gets or set customer country.
		''' </summary>
		Public Property Country() As String Implements ICustomerView.Country
			Get
				Return textBoxCountry.Text.Trim()
			End Get
			Set(ByVal value As String)
				textBoxCountry.Text = value
			End Set
		End Property

		''' <summary>
		''' Validates user input and, if valid, closes window. 
		''' </summary>
		Private Sub buttonSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles buttonSave.Click
			If String.IsNullOrEmpty(CompanyName) OrElse String.IsNullOrEmpty(City) OrElse String.IsNullOrEmpty(Country) Then
				' Do not close the dialog 
				MessageBox.Show("All fields are required")
				Return
			End If

			Try
				_customerPresenter.Save()
				Me.Close()
			Catch ex As ApplicationException
				MessageBox.Show(ex.Message, "Save failed")
				_cancelClose = True
			End Try
		End Sub

		''' <summary>
		''' Provides opportunity to cancel window close.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub FormCustomer_Closing(ByVal sender As Object, ByVal e As CancelEventArgs)
			e.Cancel = _cancelClose
			_cancelClose = False
		End Sub

		''' <summary>
		''' Checks for new customer or edit existing customer.
		''' After that it displays customer data.
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		Private Sub FormCustomer_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			' Check if this a new customer or existing one
			If CustomerId = 0 Then
				Me.Text = "New Customer"
			Else
				Me.Text = "Edit Customer"
			End If

			_customerPresenter.Display(CustomerId)
		End Sub
	End Class
End Namespace
