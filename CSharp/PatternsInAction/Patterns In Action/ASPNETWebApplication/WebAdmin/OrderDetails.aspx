<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="OrderDetails.aspx.cs" Inherits="ASPNETWebApplication.WebAdmin.OrderDetails" Title="Customer Order Details" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content header and back button -->

    <ul class="zone">
      <li class="zoneleft"><h1><asp:Label ID="LabelHeader" Runat="server" /></h1></li>
      <li class="zoneright"><asp:HyperLink ID="HyperLinkBack" runat="server"  NavigateUrl="JavaScript:history.go(-1);" Text="&lt; back"></asp:HyperLink></li>
    </ul>
    <div style="clear:both"></div>
      
    <!-- Customer name and order date -->
    <h3><asp:Label ID="LabelOrderDate" Runat="server" /></h3><br />  
      
    <!-- Order details GridView -->
            
	<asp:GridView id="GridViewOrderDetails" runat="server" 
          AutoGenerateColumns="False" Width="600">
      <Columns>
       <asp:BoundField HeaderText="Product" DataField="ProductName" />
       <asp:BoundField HeaderText="Quantity" DataField="Quantity"  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
       <asp:BoundField HeaderText="Unit Price" DataField="UnitPrice" DataFormatString="{0:c}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right"  />
       <asp:BoundField HeaderText="Discount" DataField="Discount" DataFormatString="{0:c}" HtmlEncode="false" HeaderStyle-HorizontalAlign="Right"  ItemStyle-HorizontalAlign="Right" />
      </Columns>
    </asp:GridView>
    
    <br /><br />
    
</asp:Content>
