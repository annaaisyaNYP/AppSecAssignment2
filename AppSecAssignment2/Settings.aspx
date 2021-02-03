<%@ Page Title="Settings" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="AppSecAssignment2.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Settings</h2>
    <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
    </br>
    <table class="nav-justified">
        <caption>
            Change Password&nbsp; </br>
                <asp:Label ID="lbMsg" runat="server" ForeColor="Red"></asp:Label>
                <asp:Label ID="lbSuccessMsg" runat="server" ForeColor="Lime"></asp:Label>
            </caption>
        <tr>
            <td style="width: 30%; height: 30px;">
                <asp:TextBox ID="tbCurrPass" runat="server" placeholder="Current Password" TextMode="Password"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%; height: 30px;">
                <asp:TextBox ID="tbNewPass" runat="server" placeholder="New Password" TextMode="Password"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%; height: 30px;">
                Requirements: minimum 8 characters, at least one of each: lowercase, uppercase, number, and special character ( !, *, @, #, $, %, ^, &amp;, +, = )</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 30%; height: 30px;">
                <asp:Button ID="btnChaPass" runat="server" Text="Save" OnClick="btnChaPass_Click" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
