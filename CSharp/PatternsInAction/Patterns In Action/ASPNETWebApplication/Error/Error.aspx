<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="ASPNETWebApplication.Error.Error" Title="Untitled Page" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Error</h1>

    <p>
    Sorry an error has occurred. <br /> 
    </p>
	
    <ul>
     <li>
	   <asp:HyperLink ID="HyperLinkHome" runat="server" NavigateUrl="~/Default.aspx" Text="Click here"></asp:HyperLink> to return to home page
     </li>
    </ul>

</asp:Content>
