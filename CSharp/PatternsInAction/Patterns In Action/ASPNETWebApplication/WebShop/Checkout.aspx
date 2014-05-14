<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="ASPNETWebApplication.WebShop.Checkout" Title="Checkout" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Checkout</h1>

    <p>
    This is where you would proceed to a checkout process. <br /> 
    Checkout includes collecting information on address, shipping, payment, etc.
    </p>
	
    <ul>
     <li>
	   <asp:HyperLink ID="HyperLinkHome" runat="server" NavigateUrl="~/Default.aspx" Text="Click here"></asp:HyperLink> to return to home page
     </li>
    </ul>
    
</asp:Content>
