Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq

Imports ViewState


''' <summary>
''' Base class to all pages in the web site. Provides functionality that is 
''' shared among all pages. Functionality offered includes: page render timing, 
''' gridview sorting, shopping cart access, viewstate provider access, and 
''' Javascript to be sent to the browser.
''' </summary>
''' <remarks>
''' GoF Design Patterns: Template Method.
''' Enterprise Design Patterns: Page Controller. 
''' 
''' The Template Methods Design Pattern is implemented by the StartTime property and 
''' virtual (Overridable in VB) PageRenderTime property.  Each page that derives from this 
''' base class has the option to use the properties as is, or override it with its own 
''' implementation. This base class provides the template for the page render timing facility. 
''' 
''' The Page Controller Pattern is implemented by default in ASP.NET: each page has its own 
''' Page Controller in the form of the code behind page. The implementation of a BasePage allows 
''' the common code that is shared among Page Controllers (pages) to reside in a single 
''' location. That location is this PageBase class. 
''' </remarks>
Public Class PageBase
	Inherits Page
	' Gets image service url from web.config
	Protected Shared imageService As String = ConfigurationManager.AppSettings.Get("ImageService")

	#Region "Page Render Timing"

	' Page render performance fields.
	Private startTime_Renamed As DateTime = DateTime.Now
	Private renderTime As TimeSpan

	''' <summary>
	''' Sets and gets the page render starting time. This property 
	''' represents the Template Design Pattern.
	''' </summary>
	Public Property StartTime() As DateTime
		Set(ByVal value As DateTime)
			startTime_Renamed = value
		End Set
		Get
			Return startTime_Renamed
		End Get
	End Property

	''' <summary>
	''' Gets page render time. This property is virtual therefore getting the 
	''' page performance is overridable by derived pages. This property 
	''' represents the Template Design Pattern.
	''' </summary>
	Public Overridable ReadOnly Property PageRenderTime() As String
		Get
			renderTime = DateTime.Now.Subtract(startTime_Renamed)
			Return renderTime.Seconds & "." & renderTime.Milliseconds & " seconds"
		End Get
	End Property

	#End Region

	#Region "Sorting support"

	''' <summary>
	''' Adds an up- or down-arrow image to GridView header.
	''' </summary>
	''' <param name="grid">Gridview.</param>
	''' <param name="row">Header row of gridview.</param>
	Protected Sub AddGlyph(ByVal grid As GridView, ByVal row As GridViewRow)
		' Find the column sorted on
		For i As Integer = 0 To grid.Columns.Count - 1
			If grid.Columns(i).SortExpression = SortColumn Then
				' Add a space between header and symbol
				Dim literal As New Literal()
				literal.Text = "&nbsp;"
				row.Cells(i).Controls.Add(literal)

				Dim image As New Image()
				image.ImageUrl = (If(SortDirection = "ASC", "~/Images/Site/sortasc.gif", "~/Images/Site/sortdesc.gif"))
				image.Width = 9
				image.Height = 5

				row.Cells(i).Controls.Add(image)

				Return
			End If
		Next i
	End Sub

	''' <summary>
	''' Gets or sets the current column being sorted on.
	''' </summary>
	Protected Property SortColumn() As String
		Get
			Return ViewState("SortColumn").ToString()
		End Get
		Set(ByVal value As String)
			ViewState("SortColumn") = value
		End Set
	End Property

	''' <summary>
	''' Gets or sets the current sort direction (ascending or descending).
	''' </summary>
	Protected Property SortDirection() As String
		Get
			Return ViewState("SortDirection").ToString()
		End Get
		Set(ByVal value As String)
			ViewState("SortDirection") = value
		End Set
	End Property

	''' <summary>
	''' Gets the Sql sort expression for the current sort settings.
	''' </summary>
	Protected ReadOnly Property SortExpression() As String
		Get
			Return SortColumn & " " & SortDirection
		End Get
	End Property
	#End Region

	#Region "ViewState Provider Service Access"

	' Random number generator 
	Private Shared _random As New Random(Environment.TickCount)

	''' <summary>
	''' Saves any view and control state to appropriate viewstate provider.
	''' This method shields the client from viewstate key generation issues.
	''' </summary>
	''' <param name="viewState"></param>
	Protected Overrides Sub SavePageStateToPersistenceMedium(ByVal viewState As Object)
		' Make up a unique name
		Dim random As String = _random.Next(0, Integer.MaxValue).ToString()
		Dim name As String = "ACTION_" & random & "_" & Request.UserHostAddress & "_" & DateTime.Now.Ticks.ToString()

		ViewStateProviderService.SavePageState(name, viewState)
		ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", name)
	End Sub

	'/// <summary>
	'/// Retrieves viewstate from appropriate viewstate provider.
	'/// This method shields the client from viewstate key retrieval issues.
	'/// </summary>
	'/// <returns></returns>
	Protected Overrides Function LoadPageStateFromPersistenceMedium() As Object
		Dim name As String = Request.Form("__VIEWSTATE_KEY")
		Return ViewStateProviderService.LoadPageState(name)
	End Function

	#End Region

	#Region "Javascript support"

	''' <summary>
	''' Adds an 'Open Window' Javascript function to page.
	''' </summary>
	Protected Sub RegisterOpenWindowJavaScript()
		Dim script As String = "<script language='JavaScript' type='text/javascript'>" & Constants.vbCrLf & " <!--" & Constants.vbCrLf & " function openwindow(url,name,width,height) " & Constants.vbCrLf & " { " & Constants.vbCrLf & "   window.open(url,name,'toolbar=yes,location=yes,scrollbars=yes,status=yes,menubar=yes,resizable=yes,top=50,left=50,width='+width+',height=' + height) " & Constants.vbCrLf & " } " & Constants.vbCrLf & " //--> " & Constants.vbCrLf & "</script>" & Constants.vbCrLf

		ClientScript.RegisterClientScriptBlock(GetType(String), "OpenWindowScript", script)
	End Sub
	#End Region
End Class
