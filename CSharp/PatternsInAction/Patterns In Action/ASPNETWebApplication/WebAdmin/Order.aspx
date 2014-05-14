<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Order.aspx.cs" Inherits="ASPNETWebApplication.WebAdmin.Order" Title="Customer Order" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content header and back button -->
      
    <ul class="zone">
      <li class="zoneleft"><h1><asp:Label ID="LabelHeader" Runat="server" /></h1></li>
      <li class="zoneright"><asp:HyperLink ID="HyperLinkBack" runat="server"  NavigateUrl="Orders.aspx" Text="&lt; back to orders"></asp:HyperLink></li>
    </ul>
    <div style="clear:both"></div>
      
    <!-- Orders GridView -->
      
    <asp:GridView id="GridViewOrders" runat="server" 
       AutoGenerateColumns="False" Width="600"
       OnRowDataBound="GridView_RowDataBound"
       >
      <Columns>
       <asp:BoundField HeaderText="Order Id" DataField="OrderId"/>
       <asp:BoundField HeaderText="Order Date" DataField="OrderDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
       <asp:BoundField HeaderText="Required Date" DataField="RequiredDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
       <asp:BoundField HeaderText="Shipping" DataField="Freight" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:c}" HtmlEncode="False" />
       <asp:HyperLinkField HeaderText="Items" HeaderStyle-HorizontalAlign="Center" DataNavigateUrlFields="OrderId" DataNavigateUrlFormatString="OrderDetails.aspx?id={0}" Text="View" ItemStyle-HorizontalAlign="Center" />
      </Columns>
    </asp:GridView>
      
    <!-- ObjectDataSource for orders -->
      
    <%--<asp:ObjectDataSource ID="ObjectDataSourceOrders" runat="Server" 
       SelectMethod="GetOrders" TypeName="ASPNETWebApplication.Controllers.OrderController">
       <SelectParameters>
          <asp:QueryStringParameter Name="customerId" QueryStringField="id" Type="Int32" />
       </SelectParameters>  
    </asp:ObjectDataSource>--%>

    <br /><br />
    
</asp:Content>
