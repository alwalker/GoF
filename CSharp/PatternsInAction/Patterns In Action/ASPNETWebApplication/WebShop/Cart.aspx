<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="ASPNETWebApplication.WebShop.Cart" Title="Shopping Cart" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Your Shopping Cart</h1>

    <%-- Ajax Update Panel --%>
  
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>

    <%-- Links along the top of page --%>
    
    <div class="cartline">
        <asp:HyperLink ID="HyperLinkContinueShopping" runat="Server" 
           CssClass="cartlink" Text="Continue Shopping" 
           NavigateUrl="~/WebShop/Products.aspx" />
    </div>

    <div class="cartline">
        <asp:HyperLink ID="HyperLinkCheckout" runat="Server" 
           CssClass="cartlink" Text="Checkout" 
           NavigateUrl="~/WebShop/Checkout.aspx" />
    </div>

    <br />
    
   
     <%-- Shopping cart contents --%>
    
     <asp:GridView ID="GridViewCart" SkinID="CartSkin"
        runat="server" AutoGenerateColumns="False" 
        EmptyDataText="&nbsp;&nbsp;Your cart is empty" OnRowDeleting="GridViewCart_RowDeleting">
        <Columns>
          <asp:TemplateField >
            <HeaderStyle Width="55"></HeaderStyle>
            <HeaderTemplate>&nbsp;Qty</HeaderTemplate>
            <ItemStyle Height="30"></ItemStyle>
            <ItemTemplate>&nbsp;
              <asp:TextBox Runat="server" ID="TextBoxQuantity" Width="20" MaxLength="2" Text='<%# Eval ("Quantity") %>'></asp:TextBox>
              <asp:TextBox Runat="server" ID="TextBoxId" Visible="False" Text='<%# Eval("Id") %>'></asp:TextBox>
            </ItemTemplate>        
          </asp:TemplateField>

		  <asp:HyperLinkField HeaderText="Description" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Product.aspx?id={0}" 
               DataTextField="Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"
                HeaderStyle-Width="245">
          </asp:HyperLinkField>

          <asp:TemplateField>
            <HeaderStyle Width="100" HorizontalAlign="Right"></HeaderStyle>
            <ItemStyle HorizontalAlign="Right"></ItemStyle>
            <HeaderTemplate>Unit Price</HeaderTemplate>
            <ItemTemplate>
              <asp:Label ID="LabelCost" Runat="Server" Font-Names="Arial" Font-Size="10pt" Text='<%# Eval("UnitPrice","{0:c}") %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          
          <asp:TemplateField>
            <HeaderStyle Width="100" HorizontalAlign="Right"></HeaderStyle>
            <ItemStyle HorizontalAlign="Right"></ItemStyle>
            <HeaderTemplate>Price</HeaderTemplate>
            <ItemTemplate>
              <asp:Label ID="LabelSubtotal" Runat="Server" Font-Names="Arial" Font-Size="10pt" Text='<%# String.Format("{0:c}", double.Parse(Eval("UnitPrice").ToString()) * int.Parse(Eval("Quantity").ToString())) %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
          
          <asp:TemplateField>
            <HeaderStyle Width="70"></HeaderStyle>
            <ItemStyle HorizontalAlign="Right"></ItemStyle>
            <ItemTemplate>
              <asp:ImageButton Runat="server" ID="ImageButtonRemove" ImageUrl="~/Images/Site/Remove.jpg" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'></asp:ImageButton>
            </ItemTemplate>
          </asp:TemplateField>
          
        </Columns>
    </asp:GridView>
    
    <br />
    
    <hr />
    
    <!-- Shopping cart subtotal, shipping, and totals -->
   
    <table border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="290" align="left">
             <asp:LinkButton ID="LinkbuttonRecalculate" Runat="server" onclick="LinkbuttonRecalculate_Click" Text="Recalculate">
            </asp:LinkButton></td>
        <td width="100" align="right"><b>SubTotal:</b></td>
        <td width="100" align="right"><%# String.Format("{0:c}", SubTotal())%></td>
        <td width="70"><asp:Image ID="ImageSpacer1" runat="server" ImageUrl="~/Images/Site/spacer.gif" Width="70" height="1" BorderWidth="0" /></td>
      </tr>
      <tr>
        <td align="right"> Ship via:
           <asp:DropDownList runat="server" ID="DropDownListShipping" 
             AutoPostBack="true" Width="80" OnSelectedIndexChanged="DropDownListShipping_SelectedIndexChanged">
              <asp:ListItem Selected="True"  Value="1" Text="Fedex"></asp:ListItem>
              <asp:ListItem Selected="False" Value="2" Text="UPS"></asp:ListItem>
              <asp:ListItem Selected="False" Value="3" Text="USPS"></asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
        <td align="right"><b>Shipping:</b></td>
        <td align="right"><%# String.Format("{0:c}", Shipping() ) %></td>
        <td ><asp:Image ID="ImageSpacer2" runat="server" ImageUrl="~/Images/Site/spacer.gif" Width="70" height="1" BorderWidth="0" /></td>
      </tr>
       
      <tr><td colspan="4"><hr /></td></tr>
      <tr>
        <td width="290" align="left">&nbsp;</td>
        <td width="100" align="right"><b>Total:</b></td>
        <td width="100" align="right"><font color="red"><u><b><%# String.Format("{0:c}", Total() ) %></b></u></font></td>
        <td width="70"><asp:Image ID="ImageSpacer3" runat="server" ImageUrl="~/Images/Site/spacer.gif" Width="70" height="1" BorderWidth="0" /></td>
      </tr>
      <tr>
        <td height="36" colspan="3" align="left" valign="middle"><asp:HyperLink ID="HyperLinkCheckoutBottom" runat="Server" 
         CssClass="cartlink" Text="Checkout" NavigateUrl="~/WebShop/Checkout.aspx" /></td>
      </tr>
     </table>
    </ContentTemplate>
   </asp:UpdatePanel>
   
   
   <hr />
   <br /><br />
   
</asp:Content>
