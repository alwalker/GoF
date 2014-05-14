<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="ASPNETWebApplication.WebShop.Product" Title="Product Details" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>
<%@ Import Namespace="ASPNETWebApplication.ActionServiceReference" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Content header and back button -->
    
    <ul class="zone">
      <li class="zoneleft"><h1>Product Details</h1></li>
      <li class="zoneright"><asp:HyperLink ID="HyperLinkBack" runat="server"  NavigateUrl="JavaScript:history.go(-1);" Text="&lt; back to products page"></asp:HyperLink></li>
    </ul>

    <div id="main-body">

    <!-- Product image -->
    
	<asp:Image ID="ImageProduct" runat="server" 
	     CssClass="floatright" Width="130" Height="130" BorderWidth="0" />
  	
    <!-- Details View -->
    <asp:DetailsView ID="DetailViewProduct" runat="server" 
         DataSourceID="ObjectDataSourceProduct">
         <Fields>
          <asp:TemplateField HeaderText="Category&nbsp;" ItemStyle-BackColor="Moccasin" ItemStyle-Font-Bold="True" >
            <ItemTemplate>
              <asp:Label id="LabelCategory" runat="server" Text='<%# (Eval("Category") as Category).Name %>' />
            </ItemTemplate>
          </asp:TemplateField>
         
          <asp:BoundField DataField="ProductName" HeaderText="ProductName&nbsp;" />
          <asp:BoundField DataField="UnitPrice" DataFormatString="{0:c}" HtmlEncode="False" HeaderText="Price&nbsp;" />
          <asp:BoundField DataField="Weight" HeaderText="Weight&nbsp;" />
          <asp:BoundField DataField="UnitsInStock" HeaderText="# In Stock&nbsp;" />
         </Fields>
    </asp:DetailsView>
    
    <!-- Object Data Source -->
    
    <asp:ObjectDataSource ID="ObjectDataSourceProduct" runat="server" 
        TypeName="ASPNETWebApplication.Controllers.ProductController" SelectMethod="GetProduct">
       <SelectParameters>
         <asp:QueryStringParameter Name="ProductId" QueryStringField="id" />
       </SelectParameters>
    </asp:ObjectDataSource>
    
    <br /><br />
      
    <!-- Buttons -->
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Quantity:</b>
    &nbsp;&nbsp;<asp:TextBox ID="TextBoxQuantity" runat="Server" MaxLength="2" Width="30" Text="1" />
    &nbsp;&nbsp;<asp:Button ID="ButtonAddToCart" runat="Server" Text=" Add to Cart " OnClick="ButtonAddToCart_Click" /><br />
    
    <!-- Validators -->
    
    <asp:ValidationSummary ID="ValidationSummaryQuantity" runat="server" 
      ShowSummary="True" >  
    </asp:ValidationSummary>
    
    <br />
    <asp:RequiredFieldValidator ID="RequiredFieldValidatorQuantity" runat="server" Display="None"
        ControlToValidate="TextBoxQuantity" ErrorMessage="Quantity must be entered" >
    </asp:RequiredFieldValidator><br />    

    <br />

    <asp:RangeValidator ID="RangeValidatorQuantity" runat="server" Display="None" MinimumValue="1" 
     MaximumValue="99" ControlToValidate="TextBoxQuantity" ErrorMessage="Quantity must be between 1 and 99" >
    </asp:RangeValidator>

    <br />

    <asp:CompareValidator ID="CompareValidatorQuantity" runat="server" Display="None"
        ControlToValidate="TextBoxQuantity" ErrorMessage="Quantity must be numeric"
        type="Integer" operator="DataTypeCheck">
    </asp:CompareValidator>    

    <br />
    
    </div>
    
    <br /><br />
    
</asp:Content>
