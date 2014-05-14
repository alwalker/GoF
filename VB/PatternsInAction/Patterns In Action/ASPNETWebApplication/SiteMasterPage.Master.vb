Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq

Imports AspControls

Namespace ASPNETWebApplication
	Partial Public Class SiteMasterPage
		Inherits System.Web.UI.MasterPage
		''' <summary>
		''' Establishes the composite menu hierarchy which is present on all pages.
		''' </summary>
		Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
			If (Not IsPostBack) Then
				' Build the composite menu tree
				' This tree implements the Composite Design Pattern.
				Dim root As New MenuCompositeItem("root", Nothing)
				Dim home As New MenuCompositeItem("home", ResolveUrl("~/Default.aspx"))
				Dim shop As New MenuCompositeItem("shopping", ResolveUrl("~/WebShop/Shopping.aspx"))
				Dim prod As New MenuCompositeItem("products", ResolveUrl("~/WebShop/Products.aspx"))
				Dim srch As New MenuCompositeItem("search", ResolveUrl("~/WebShop/Search.aspx"))
				Dim cart As New MenuCompositeItem("cart", ResolveUrl("~/WebShop/Cart.aspx"))
				Dim admn As New MenuCompositeItem("administration", ResolveUrl("~/WebAdmin/Admin.aspx"))
				Dim cust As New MenuCompositeItem("customers", ResolveUrl("~/WebAdmin/Customers.aspx"))
				Dim ordr As New MenuCompositeItem("orders", ResolveUrl("~/WebAdmin/Orders.aspx"))

				Dim auth As MenuCompositeItem
				If Request.IsAuthenticated Then
					auth = New MenuCompositeItem("logout", ResolveUrl("~/Logout.aspx"))
				Else
					auth = New MenuCompositeItem("login", ResolveUrl("~/Login.aspx"))
				End If

				shop.Children.Add(prod)
				shop.Children.Add(srch)
				shop.Children.Add(cart)
				admn.Children.Add(cust)
				admn.Children.Add(ordr)
				root.Children.Add(home)
				root.Children.Add(shop)
				root.Children.Add(admn)
				root.Children.Add(auth)

				TheMenuComposite.MenuItems = root
			End If
		End Sub

		''' <summary>
		''' Gets the menu from the master page. This property makes the menu 
		''' accessible from contentplaceholders. This allows the individual pages 
		''' to set the selected menu item.
		''' </summary>
		Public ReadOnly Property TheMenuInMasterPage() As MenuComposite
			Get
				Return Me.TheMenuComposite
			End Get
		End Property

		''' <summary>
		''' Gets the page render time.
		''' </summary>
		Protected ReadOnly Property PageRenderTime() As String
			Get
				' Be sure that all ContentPlaceHolder pages are derived from PageBase.
				' BTW: this is how you access content pages from a master page --
				' most developers ask about access the other way around, that is, access 
				' the master page from the content pages which is also demonstrated here 
				' with the above TheMenuInMasterPage property.
				Try
					Dim pageBase As PageBase = TryCast(Me.ContentPlaceHolder1.Page, PageBase) '(this.FindControl("ContentPlaceHolder1") as ContentPlaceHolder).Page as PageBase;//
					Return pageBase.PageRenderTime
                Catch
                End Try

				Return ""
			End Get
		End Property
	End Class
End Namespace
