<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="ASPNETWebApplication.WebAdmin.Customer" Title="Customer Details" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <!-- Content header and back button -->
      
      <ul class="zone">
        <li class="zoneleft"><h1>Customer Details</h1></li>
        <li class="zoneright"><asp:HyperLink ID="HyperLinkBack" runat="server"  NavigateUrl="JavaScript:history.go(-1);" Text="&lt; back to customer list"></asp:HyperLink></li>
      </ul>
      
      <!-- Error message label -->
      
      <asp:Label ID="LabelError" runat="server" Visible="False" ForeColor="Red" BackColor="Yellow"></asp:Label>
      
      <div id="main-body">
      
      <!-- Customer image -->

      <asp:Image ID="ImageCustomer" runat="server" CssClass="floatright" Width="100" Height="100" BorderStyle="None" />
	  
      <!-- Form View -->

      <asp:DetailsView ID="DetailsViewCustomer" runat="server" 
         DataKeyNames="CustomerId"
         DataSourceID="ObjectDataSourceCustomer"
          OnDataBound="DetailsView_OnDataBound"
         >
         <Fields>
           <asp:BoundField DataField="CustomerId" HeaderText="Id&nbsp;" InsertVisible="False" ReadOnly="True" ItemStyle-BackColor="Moccasin" ItemStyle-Font-Bold="True" />
           <asp:BoundField DataField="Company" HeaderText="Company&nbsp;" />
           <asp:BoundField DataField="City" HeaderText="City&nbsp;" />
           <asp:BoundField DataField="Country" HeaderText="Country&nbsp;" />
         </Fields>
         <FooterTemplate>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              <asp:Button ID="ButtonSave" Runat="server" Text=" Save " onclick="ButtonSave_Click"></asp:Button>
              &nbsp;&nbsp;<asp:Button ID="ButtonCancel" Runat="server" Text="Cancel" onclick="ButtonCancel_Click"></asp:Button>            
         </FooterTemplate>
         <FooterStyle Height="40" BackColor="White" HorizontalAlign="Center" />
      </asp:DetailsView>

      <!-- Object Data Source -->

      <asp:ObjectDataSource ID="ObjectDataSourceCustomer" runat="server" 
        TypeName="ASPNetWebApplication.Controllers.CustomerController" 
         SelectMethod="GetCustomer">
        <SelectParameters>
          <asp:QueryStringParameter Name="CustomerId" QueryStringField="id" />
        </SelectParameters>
      </asp:ObjectDataSource>
     
      </div>     
     
      <br /><br />
</asp:Content>
