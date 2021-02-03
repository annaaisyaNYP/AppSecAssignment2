<%@ Page Title="Settings" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="AppSecAssignment2.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Settings</h2>
    <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
    <table class="nav-justified">
        <caption>
            Change Password</caption>
        <tr>
            <td style="width: 203px">
                &nbsp;</td>
            <td>
                <asp:Label ID="lbMsg" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">
                Current Password</td>
            <td>
                <asp:TextBox ID="tbCurrPass" runat="server" placeholder="Current Password"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">
                New Password</td>
            <td>
                <asp:TextBox ID="tbNewPass" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">
                Confirm Password</td>
            <td>
                <asp:TextBox ID="tbConPass" runat="server"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 203px">
                <asp:Button ID="btnChaPass" runat="server" Text="Save" />
            </td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
