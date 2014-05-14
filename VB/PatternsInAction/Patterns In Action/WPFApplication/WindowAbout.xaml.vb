Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Shapes

Imports System.ComponentModel
Imports System.Reflection

Namespace WPFApplication
	''' <summary>
	''' The About dialog window
	''' </summary>
	Partial Public Class WindowAbout
		Inherits Window
		''' <summary>
		''' Constructor. Populates dialog controls.
		''' </summary>
		Public Sub New()
			InitializeComponent()

			Me.Title = String.Format("About {0}", AssemblyTitle)
			Me.labelProductName.Content = AssemblyProduct
			Me.labelVersion.Content = String.Format("Version {0}", AssemblyVersion)
			Me.labelCopyright.Content = AssemblyCopyright + DateTime.Now.Year.ToString()
			Me.labelCompanyName.Content = AssemblyCompany
			Me.textBoxDescription.Text = AssemblyDescription
		End Sub

		#Region "Assembly Attribute Accessors"

		''' <summary>
		''' Gets assembly title.
		''' </summary>
		Private ReadOnly Property AssemblyTitle() As String
			Get
				Dim attributes() As Object = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyTitleAttribute), False)
				If attributes.Length > 0 Then
					Dim titleAttribute As AssemblyTitleAttribute = CType(attributes(0), AssemblyTitleAttribute)
					If titleAttribute.Title <> "" Then
						Return titleAttribute.Title
					End If
				End If
				Return System.IO.Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)
			End Get
		End Property

		''' <summary>
		''' Gets assembly version.
		''' </summary>
		Private ReadOnly Property AssemblyVersion() As String
			Get
				Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
			End Get
		End Property

		''' <summary>
		''' Gets assembly description.
		''' </summary>
		Private ReadOnly Property AssemblyDescription() As String
			Get
				Dim attributes() As Object = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyDescriptionAttribute), False)
				If attributes.Length = 0 Then
					Return ""
				End If
				Return (CType(attributes(0), AssemblyDescriptionAttribute)).Description
			End Get
		End Property

		''' <summary>
		''' Gets assembly product.
		''' </summary>
		Private ReadOnly Property AssemblyProduct() As String
			Get
				Dim attributes() As Object = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyProductAttribute), False)
				If attributes.Length = 0 Then
					Return ""
				End If
				Return (CType(attributes(0), AssemblyProductAttribute)).Product
			End Get
		End Property

		''' <summary>
		''' Gets assembly copyright.
		''' </summary>
		Private ReadOnly Property AssemblyCopyright() As String
			Get
				Dim attributes() As Object = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCopyrightAttribute), False)
				If attributes.Length = 0 Then
					Return ""
				End If
				Return (CType(attributes(0), AssemblyCopyrightAttribute)).Copyright
			End Get
		End Property

		''' <summary>
		''' Gets assembly company.
		''' </summary>
		Private ReadOnly Property AssemblyCompany() As String
			Get
				Dim attributes() As Object = System.Reflection.Assembly.GetExecutingAssembly().GetCustomAttributes(GetType(AssemblyCompanyAttribute), False)
				If attributes.Length = 0 Then
					Return ""
				End If
				Return (CType(attributes(0), AssemblyCompanyAttribute)).Company
			End Get
		End Property

		#End Region
	End Class
End Namespace
