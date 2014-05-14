<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="ASPNETWebApplication.WebShop.Products" Title="Products" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<h1>Products</h1>
  
  <%-- Ajax Update Panel --%>
  
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
   
    <%-- Product Category --%>
    
    Select a Category:&nbsp; <asp:DropDownList ID="DropDownListCategories" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSourceCategories"
        DataTextField="Name" DataValueField="CategoryId" Width="130" OnSelectedIndexChanged="DropDownListCategories_SelectedIndexChanged"></asp:DropDownList>

    <asp:ObjectDataSource ID="ObjectDataSourceCategories" runat="server"
        SelectMethod="GetCategories" TypeName="ASPNETWebApplication.Controllers.ProductController" >
    </asp:ObjectDataSource><br /><br />

     
    <%-- Product List --%>
    
    <asp:GridView ID="GridViewProducts" runat="server" 
       AutoGenerateColumns="False" Width="600" 
       AllowSorting="True"   
       OnSorting="GridViewProducts_Sorting"
       OnRowCreated="GridViewProducts_RowCreated" >
       <Columns>
    	  <asp:BoundField HeaderText="Id" DataField="ProductId"  SortExpression="ProductId" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" />
		    <asp:BoundField HeaderText="Product Name" DataField="ProductName" SortExpression="ProductName" HeaderStyle-Width="300" />
		    <asp:BoundField HeaderText="Weight" DataField="Weight" SortExpression="Weight" HeaderStyle-Width="80" />
		    <asp:BoundField HeaderText="Price" DataField="UnitPrice" SortExpression="UnitPrice" DataFormatString="{0:c}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80"  />
		    <asp:HyperLinkField HeaderText="Details" DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="Product.aspx?id={0}" 
               Text="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90" />
       </Columns>
    </asp:GridView>
    
   </ContentTemplate>
  </asp:UpdatePanel>
  
  <br /><br />
  
</asp:Content>
