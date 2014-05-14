<%@ Page Language="C#" MasterPageFile="~/SiteMasterPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPNETWebApplication.Default" Title="ASP.NET Application -- Patterns-in-Action 3.5" %>
<%@ MasterType TypeName="ASPNETWebApplication.SiteMasterPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <h1>Welcome</h1>
    
    <!-- image -->
    
    <div class="floatright">
      <asp:Image ID="ImageDefault" runat="server" ImageUrl="~/Images/Site/default.jpg" Width="162" Height="232" BorderWidth="0" />
    </div>
        
	<p>
	Thank you for purchasing the Design Pattern Framework.
	You are currently running the <strong>Patterns-in-Action 3.5</strong> ASP.NET Web Application 
	which demonstrates when, where, and how design patterns are 
	most effectively used in a modern 3-tier e-commerce web application. 
	</p>
	
	<p>
	This application has been built around the most frequently used 
	Gang of Four (GoF) design patterns and associated best practice architectures. 
	Also included are numerous Enterprise Patterns 
	as documented in Martin Fowler's book titled: 
     <%--<a href="JavaScript:openwindow('http://www.martinfowler.com/books.html#eaa','mf0','790','460');">--%>
     "Patterns of Enterprise Application Architecture"<%--</a>--%>.
    Finally, SOA and Messaging Patterns are found in the WCF Services layer which this application consumes.
    
	</p> 
	<br />
    <b>Getting Started</b>
    <br /><br />
	<p>
    As a first step, we recommend that you familiarize yourself with the 
    functionality of this application. Select the menu items on the left and 
    explore the options.  Secondly, we suggest you analyze the .NET Solution 
    and Project structure. This will provide a glimpse at the 3-tier architecture 
    and some of the major pattern used to build this reference application. Once you 
    have an understanding of the overall design and architecture you'll want to explore  
    the details of the numerous design patterns used throughout the application.  
    Next, you can explore the Service and Hosting layers (Cloud Facade pattern). 
    Finally, we suggest you examine the other clients (Windows Forms Application 
    and WPF Application) and their use of the same WCF Service and Hosting layers .
    </p>
    
    <p>
    <br />
    <b>Where To Find Documentation</b>
    <br /><br />
    
    Setup, functionality, design, architecture, and design patterns are discussed in
    the accompanying document named: <b>PatternsInAction.pdf</b>. &nbsp;
    In addition, in the .NET Solution under a folder named \Solution Items\Documention\ you'll find 
    a reference guide, named 
    <b>Documentation.chm</b>, of all types
    (classes, methods, interfaces, enums, etc) used in the application. 
    The C# and VB.NET source code itself is well commented. Finally, all of the 21 projects 
    come with their own class diagram located in folders named \_UML Diagram\.
    </p>
    
    <br />
    <b>A Great Learning Experience</b>
    <br /><br />
    <p>
    We are confident that the <strong>Patterns-in-Action 3.5</strong> reference application will be a great 
    learning experience on the use of design patterns in the real world. 
    </p>
	<br /><br />


</asp:Content>
