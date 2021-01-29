<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AppSecAssignment2._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Login</h2>
    <table style="width:100%;">
        <tr style="height: 30px">
            <td style="width: 200px">Email</td>
            <td>
                <asp:TextBox ID="tbEmail" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="height: 30px">
            <td style="width: 200px">Password</td>
            <td>
                <asp:TextBox ID="tbPass" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="height: 30px">
            <td style="width: 200px">&nbsp;</td>
            <td>
                <asp:Button ID="btnLogin" runat="server" Text="Login" />
            </td>
            <td>&nbsp;</td>
        </tr>
      </table>

</asp:Content>
