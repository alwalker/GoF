<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="ASPNETWebApplication.WebShop.Search" Title="Search for Products" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <h1>Search Products</h1>
    
  
  
    
    <%-- Search criteria --%>

    Product Name: 
     <asp:TextBox ID="TextBoxProductName" runat="server" Width="100"></asp:TextBox>&nbsp;&nbsp;&nbsp;
    Price range: 
    <asp:DropDownList ID="DropDownListRange" runat="server" Width="130" DataSourceID="ObjectDataSourcePriceRange" DataTextField="RangeText" DataValueField="RangeId">
      <asp:ListItem Selected="True" Value="0"></asp:ListItem>
    </asp:DropDownList>
    
    &nbsp;&nbsp;<asp:Button ID="ButtonSearch" runat="server" Text=" Find " UseSubmitBehavior="true" OnClick="ButtonSearch_Click" /><br />

    
    <hr />
    <br />

    <%--  Price range ObjectDataSource --%>

    <asp:ObjectDataSource runat="Server" ID="ObjectDataSourcePriceRange"
        SelectMethod="GetProductPriceRange" TypeName="ASPNETWebApplication.Controllers.ProductController" >
    </asp:ObjectDataSource>
 
 
    <%-- Ajax Update Panel --%>
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>    
    
 
    <%--  Search results --%>
    
    <asp:GridView ID="GridViewProducts" runat="server"
        AutoGenerateColumns="False" Width="600"
        AllowSorting="True"
        OnSorting="GridViewProducts_Sorting"
        OnRowCreated="GridViewProducts_RowCreated" 
        EmptyDataText="No products found. Please try again">
    
        <Columns>
    		<asp:BoundField HeaderText="Id" DataField="ProductId"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" SortExpression="ProductId" HeaderStyle-Width="50"/>
			<asp:BoundField HeaderText="Product Name" DataField="ProductName" SortExpression="ProductName" HeaderStyle-Width="350"/>
			<asp:BoundField HeaderText="Price" DataField="UnitPrice" DataFormatString="{0:c}" SortExpression="UnitPrice" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
			<asp:HyperLinkField HeaderText="Details" DataNavigateUrlFields="ProductId" DataNavigateUrlFormatString="Product.aspx?id={0}" 
                Text="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90" />
        </Columns>
        <EmptyDataRowStyle Font-Bold="True" BackColor="FloralWhite" />
    </asp:GridView>
    
   </ContentTemplate>
  </asp:UpdatePanel>
 
  
  <br /><br />
  
</asp:Content>
