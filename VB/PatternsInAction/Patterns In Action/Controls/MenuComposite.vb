Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Linq

Namespace AspControls
    ''' <summary>
    ''' Menu control. Renders HTML menu controls including selected menu items,
    ''' menu item indentation, as well as the proper HTML and CSS (cascading 
    ''' style sheet) tags.
    ''' </summary>
    ''' <remarks>
    ''' GoF Design patterns: Composite.
    ''' EnterPrise Design Pattern: Transform View.
    ''' The Composite Design Pattern is a self-referencing data structure which 
    ''' in this case is a menu tree hierarchy of self-referencing menu items. 
    ''' 
    ''' The Transform View processes data elements and transforms these into HTML.  
    ''' Usually this applies to domain specific data (business objects), but it is 
    ''' valid also for menu items. In fact, all databound Web Controls in ASP.NET 
    ''' are pure TransForm View Design Pattern implementations.
    ''' </remarks>
    <DefaultProperty("Selected"), ToolboxData("<{0}:MenuComposite runat=server></{0}:MenuComposite>")> _
    Public Class MenuComposite
        Inherits WebControl
        ''' <summary>
        ''' Gets and sets the selected menu item.
        ''' </summary>
        <Bindable(True), Category("Appearance"), DefaultValue(""), Localizable(True)> _
        Public Property SelectedItem() As String
            Get
                Dim s As String = CType(ViewState("SelectedItem"), String)
                Return (If((s Is Nothing), String.Empty, s))
            End Get

            Set(ByVal value As String)
                ViewState("SelectedItem") = value
            End Set
        End Property

        ''' <summary>
        ''' Gets and sets the entire menu tree using the ASP.NET Viewstate.
        ''' </summary>
        <Bindable(True), Category("Appearance"), DefaultValue(""), Localizable(True)> _
        Public Property MenuItems() As MenuCompositeItem
            Get
                Return TryCast(ViewState("MenuItems"), MenuCompositeItem)
            End Get

            Set(ByVal value As MenuCompositeItem)
                ViewState("MenuItems") = value
            End Set
        End Property

        ''' <summary>
        ''' Renders the entire menu structure.
        ''' </summary>
        ''' <param name="output">The HTML output stream</param>
        Protected Overrides Sub RenderContents(ByVal output As HtmlTextWriter)
            Dim root As MenuCompositeItem = Me.MenuItems

            output.Write("<div id=""navcontainer"">")
            output.Write("	<ul id=""navlist"">")

            RecursiveRender(output, root, 0)

            output.Write("	</ul>")
            output.Write("</div>")
        End Sub

        ''' <summary>
        ''' Recursive function that renders a menu item at the correct 
        ''' indentation level.  This is a private helper method.
        ''' </summary>
        ''' <param name="output">The HTML output stream.</param>
        ''' <param name="item">Menu item.</param>
        ''' <param name="depth">Indentation depth.</param>
        Private Sub RecursiveRender(ByVal output As HtmlTextWriter, ByVal item As MenuCompositeItem, ByVal depth As Integer)
            If depth > 0 Then ' Skip root node
                If depth = 1 Then
                    output.Write("<li>") ' main menu
                Else
                    output.Write("<li class=""indented"">") ' sub menu
                End If

                output.Write("<a href=""" & item.Link & """>")

                If item.Item = SelectedItem Then ' selected item
                    output.Write("<span class=""selected"">" & item.Item & "</span>")
                Else
                    output.Write(item.Item) ' unselected item.
                End If

                output.Write("</a>")
            End If

            ' Recursively iterate over its children.
            For i As Integer = 0 To item.Children.Count - 1
                RecursiveRender(output, item.Children(i), depth + 1)
            Next i
        End Sub
    End Class
End Namespace
