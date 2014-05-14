<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="ASPNETWebApplication.WebAdmin.Orders" Title="Customer Orders" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Orders by Customer</h1>
    
    <%-- Ajax Update Panel --%>
     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
  
    <!-- Customers wit Order Summary GridView -->
    
    <asp:GridView ID="GridViewOrders" runat="server" 
        AutoGenerateColumns="False" Width="640"
        AllowSorting="True"
        OnSorting="GridViewOrders_Sorting"
        OnRowCreated="GridViewOrders_RowCreated" >

        <Columns>
    	<asp:BoundField HeaderText="Id" DataField="CustomerId" SortExpression="CustomerId" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"/>
			<asp:BoundField HeaderText="Customer Name" DataField="Company" SortExpression="CompanyName" />
			<asp:BoundField HeaderText="City" DataField="City" SortExpression="City"  />
			<asp:BoundField HeaderText="Country" DataField="Country" SortExpression="Country"  />
			<asp:BoundField HeaderText="# Orders" DataField="NumOrders" SortExpression="NumOrders" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  />
			<asp:BoundField HeaderText="Last Order" DataField="LastOrderDate" SortExpression="LastOrderDate" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False"  />
			<asp:HyperLinkField HeaderText="Orders" DataNavigateUrlFields="CustomerId" DataNavigateUrlFormatString="Order.aspx?id={0}" 
                Text="View" ItemStyle-HorizontalAlign="Center"  />
        </Columns>
    </asp:GridView>
   
   </ContentTemplate>
  </asp:UpdatePanel>
  
    <br /><br />
    
</asp:Content>
