<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ASPNETWebApplication.Login" Title="Login" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <h1>
    Login</h1>
  <br />
  <p>
    Please login to access the Administration area. For demonstration purposes you can
    use these credentials: &nbsp;<i>username:</i> '<font color='red'>debbie</font>',
    <i>password:</i> '<font color='red'>secret123</font>'.</p>
  
  <%-- panel allows default button to be set --%>
  
  <asp:Panel ID="Panel1" DefaultButton="ButtonSubmit" runat="server">
  
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
      <tr>
        <td width="10">&nbsp;</td>
        <td>
          <font color="red" face="Arial">
            <asp:Literal runat="server" ID="LiteralError"></asp:Literal></font>
          <br />
          <table bgcolor="#ffffcc" width="340" border="0" cellpadding="0" cellspacing="0">
            <tr>
              <td height="22" bgcolor="#ffcc33" align="left" colspan="2">
                &nbsp;&nbsp;&nbsp;please login
              </td>
            </tr>
            <tr>
              <td align="right" height="32">
                username:&nbsp;
              </td>
              <td>
                <asp:TextBox ID="TextboxUserName" runat="server" TextMode="SingleLine" TabIndex="1"
                  Width="200"></asp:TextBox>
              </td>
            </tr>
            <tr>
              <td align="right">
                password:&nbsp;
              </td>
              <td>
                <asp:TextBox ID="TextboxPassword" runat="server" TextMode="Password" TabIndex="2"
                  Width="200"></asp:TextBox>
              </td>
            </tr>
            <tr>
              <td height="40" bgcolor="#ffffcc">
                &nbsp;
              </td>
              <td height="40" bgcolor="#ffffcc" valign="middle">
                <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" OnClick="ButtonSubmit_Click">
                </asp:Button>
              </td>
            </tr>
          </table>
        </td>
      </tr>
    </table>
  </asp:Panel>
  <br />
  <br />
  <br />
  <br />
</asp:Content>
