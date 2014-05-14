<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Customers.aspx.cs" Inherits="ASPNETWebApplication.WebAdmin.Customers" Title="Customers" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Customers</h1>
    
    <%-- Ajax Update Panel --%> 
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>        
    
    
    <%-- Error message and Add new Customer button --%>
    
    <ul id="customers">
      <li id="customerserror"><asp:Label ID="LabelError" runat="server" Text="" EnableViewState="false"></asp:Label></li>
      <li id="addnewcustomer"><asp:HyperLink ID="HyperLinkNewCustomer" runat="Server"
          text="Add new Customer" NavigateUrl="~/WebAdmin/Customer.aspx?id=0" /></li>
    </ul>
    
    
      
    <%-- Customer GridView --%>
    
    <asp:GridView ID="GridViewCustomers" runat="server"
        DataKeyNames="CustomerId" 
        AutoGenerateColumns="False" Width="600"
        AllowSorting="True"
        OnRowDataBound="GridView_RowDataBound"
        OnSorting="GridViewCustomers_Sorting"
        OnRowCreated="GridViewCustomers_RowCreated" 
        OnRowDeleting="GridViewCustomers_RowDeleting" >
        <Columns>
    	  	<asp:BoundField HeaderText="Id" DataField="CustomerId" SortExpression="CustomerId" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50" />
          <asp:BoundField HeaderText="Customer Name" DataField="Company" SortExpression="CompanyName" HeaderStyle-Width="220" />
			    <asp:BoundField HeaderText="City" DataField="City" SortExpression="City" HeaderStyle-Width="120" />
			    <asp:BoundField HeaderText="Country" DataField="Country" SortExpression="Country" HeaderStyle-Width="80" />
			    <asp:HyperLinkField HeaderText="Edit" DataNavigateUrlFields="CustomerId" DataNavigateUrlFormatString="Customer.aspx?id={0}" 
                Text="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60" />
			    <asp:CommandField HeaderText="Delete" ButtonType="Link"  
			          ShowDeleteButton="True" ControlStyle-ForeColor="#990000" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70"  />
        </Columns>
    </asp:GridView>
   
   </ContentTemplate>
  </asp:UpdatePanel>
  
  <br /><br />
  
</asp:Content>
